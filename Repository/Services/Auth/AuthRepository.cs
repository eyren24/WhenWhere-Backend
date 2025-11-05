using System.Security.Cryptography;
using Auth.dto;
using AutoMapper;
using Database.Data;
using Database.Models;
using DTO.Auth;
using Microsoft.EntityFrameworkCore;
using Repository.interfaces;

namespace Repository.services.auth;

public class AuthRepository(AppDbContext _context, IMapper _mapper, IMailTrapRepository _mailer) : IAuthRepository
{
    private readonly Random random = new();
    private readonly string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

    public async Task<TokenInfoDTO> RegisterAsync(ReqRegisterUser newUser)
    {
        var cercaUtente = await _context.Utente.AnyAsync(p => p.email == newUser.email);
        if (cercaUtente)
            throw new Exception("Esiste già un utente con questa email.");

        if (newUser.password != newUser.confermaPassword)
            throw new Exception("Le due password non corrispondono.");

        newUser.password = BCrypt.Net.BCrypt.HashPassword(newUser.password);
        var modello = _mapper.Map<Utente>(newUser);
        modello.lastLogin = DateTime.Now;
        modello.token = GeneraCodiceVerifica();
        modello.tokenExpiration = DateTime.Now.AddMinutes(5);
        await _context.AddAsync(modello);
        await _context.SaveChangesAsync();
        await _mailer.SendWelcomeMailAsync(modello.email, modello.nome + modello.cognome, modello.token);
        return new TokenInfoDTO
            { nomeCompleto = $"{modello.nome} {modello.cognome}", utenteId = modello.id, ruolo = ERuolo.Utente };
    }

    public async Task<TokenInfoDTO> LoginAsync(ReqLoginUser request)
    {
        var utente = await _context.Utente.SingleOrDefaultAsync(p => p.email == request.email);
        if (utente == null || !BCrypt.Net.BCrypt.Verify(request.password, utente.password))
            throw new Exception("Email o Password errata.");
        utente.lastLogin = DateTime.Now;
        _context.Update(utente);
        await _context.SaveChangesAsync();

        return new TokenInfoDTO
            { nomeCompleto = $"{utente.nome} {utente.cognome}", utenteId = utente.id, ruolo = (ERuolo)utente.ruoloId };
    }

    public async Task<string> GenerateRefreshTokenAsync(int utenteId, int length = 255)
    {
        await RemoveExpiredRefreshTokens();

        var stringChars = new char[length];
        var check = true;
        do
        {
            for (var i = 0; i < length; i++) stringChars[i] = chars[random.Next(chars.Length)];
            check = await _context.RefreshToken.AnyAsync(r => r.token == new string(stringChars));
        } while (check);

        await _context.AddAsync(new RefreshToken
            { token = new string(stringChars), utenteId = utenteId, dataScadenza = DateTime.Now.AddHours(8) });
        await _context.SaveChangesAsync();

        return new string(stringChars);
    }

    public async Task<(TokenInfoDTO? tokenInfo, string? newRefreshToken)> RefreshTokenAsync(string oldToken)
    {
        var refreshTokenDb = await _context.RefreshToken.FirstOrDefaultAsync(r => r.token == oldToken);
        if (refreshTokenDb == null)
            //_logger.LogInformation("RefreshToken non trovato.");
            return (null, null);

        var dataScadenza = refreshTokenDb.dataScadenza;
        var userId = refreshTokenDb.utenteId;

        var utente = await _context.Utente.FindAsync(userId);

        if (utente == null) throw new Exception("L'utente associato al token non esiste.");

        if (dataScadenza < DateTime.Now) return (null, null);

        var newRefreshToken = await GenerateRefreshTokenAsync(userId);

        return (
            new TokenInfoDTO
                { nomeCompleto = $"{utente.nome} {utente.cognome}", utenteId = userId, ruolo = (ERuolo)utente.ruoloId },
            newRefreshToken);
    }

    public async Task<string> VerifyTokenAsync(string email, string token)
    {
        var utente = await _context.Utente.SingleOrDefaultAsync(p => p.email == email && p.token == token);
        if (utente == null)
        {
            throw new Exception("Token non valido.");
        }

        if (utente.tokenExpiration < DateTime.Now)
        {
            throw new Exception("Token scaduto.");
        }

        utente.token = null;
        utente.tokenExpiration = null;
        await _context.SaveChangesAsync();
        return "Token validato! Il tuo account è ora verificato!";
    }

    private async Task RemoveExpiredRefreshTokens()
    {
        List<RefreshToken> toRemove =
            await _context.RefreshToken.Where(r => r.dataScadenza < DateTime.Now).ToListAsync();
        _context.RemoveRange(toRemove);
    }

    private static string GeneraCodiceVerifica()
    {
        var bytes = new byte[3];
        RandomNumberGenerator.Fill(bytes);
        return BitConverter.ToString(bytes).Replace("-", "").ToUpper();
    }
}