using Database.Data;
using DTO.Auth;
using Microsoft.EntityFrameworkCore;

namespace Repository.Services.Auth;

public class AuthRepository (AppDbContext _context)
{
    public async Task RegisterAsync(ReqRegisterUser newUser)
    {
        var cercaUtente = await _context.Utente.AnyAsync(p => p.email == newUser.email);
        if (cercaUtente)
            throw new Exception("Esiste già un utente con questa email.");
        newUser.password = BCrypt.Net.BCrypt.HashPassword(newUser.password);
    }
}