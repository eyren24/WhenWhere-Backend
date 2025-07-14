using Database.Data;
using Database.Models;
using DTO.Auth;
using Microsoft.EntityFrameworkCore;

namespace Repository.services.auth;

public class AuthRepository(AppDbContext _context)
{
    public async Task<TokenInfoDTO> RegisterAsync(ReqRegisterUser newUser)
    {
        var cercaUtente = await _context.Utente.AnyAsync(p => p.email == newUser.email);
        if (cercaUtente)
            throw new Exception("Esiste già un utente con questa email.");
        try {
            newUser.password = BCrypt.Net.BCrypt.HashPassword(newUser.password);
            var modello = new Utente()
            {

            };

            await _context.AddAsync(modello);
            await _context.SaveChangesAsync();
            
            return new TokenInfoDTO
                { nomeCompleto = modello.nome + modello.cognome, utenteId = modello.id, ruolo = ERuolo.Utente };
        }
        catch (Exception e) {
            throw new Exception(e.Message);
        }
    }
}