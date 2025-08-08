using Auth.dto;
using Auth.Interfaces;
using AutoMapper;
using Database.Data;
using Database.Models;
using DTO.Utente;
using Microsoft.EntityFrameworkCore;
using Repository.interfaces;

namespace Repository.services.utente;

public class UtenteRepo( AppDbContext _context, IMapper _mapper, ITokenService _token) : IUtenteRepo
{
    public async Task UpdateAsync(int id, ReqUpdateUtenteDTO utenteUpdate)
    {
        var modello = await _context.Utente.FindAsync(id);
        if (modello == null)
        {
            throw new Exception($"Utente con ID: {id} non trovato");
        }

        modello.nome = utenteUpdate.nome;
        modello.cognome = utenteUpdate.cognome;
        modello.fotoProfilo = utenteUpdate.fotoProfilo;
        modello.preferenzeNotifiche = utenteUpdate.preferenzeNotifiche;
        _context.Utente.Update(modello);
        await _context.SaveChangesAsync();
    }

    public async Task ToggleStatusAsync(int id)
    {
        var tokenInfo = _token.GetInfoToken();
        if (tokenInfo == null)
        {
            throw new Exception("Token non valido");
        }
        
        if (tokenInfo.ruolo != ERuolo.Amministratore && tokenInfo.utenteId != id)
        {
            throw new Exception("Non sei autorizzato a modificare lo stato di questo account");
        }

        var utente = await _context.Utente.FirstOrDefaultAsync(p => p.id == id);
        if (utente == null)
        {
            throw new Exception($"Utente con ID: {id} non trovato");
        }

        utente.statoAccount = !utente.statoAccount;
        await _context.SaveChangesAsync();
    }

    public async Task<List<ResUtenteDTO>> GetListAsync(FiltriUtenteDTO filtri)
    {
        var tokenInfo = _token.GetInfoToken();
        if (tokenInfo == null)
        {
            throw new Exception("Token non valido");
        }

        if (tokenInfo.ruolo != ERuolo.Amministratore)
        {
            throw new Exception("Accesso  autorizzato solo agli amministratori");
        }
        //filtri
        var query = _context.Utente.AsQueryable();

        if (!string.IsNullOrWhiteSpace(filtri.nome))
        {
            query = query.Where(u => u.nome.Contains(filtri.nome));
        }

        if (!string.IsNullOrWhiteSpace(filtri.cognome))
        {
            query = query.Where(u => u.cognome.Contains(filtri.cognome));
        }

        if (!string.IsNullOrWhiteSpace(filtri.email))
        {
            query = query.Where(u => u.email.Contains(filtri.email));
        }

        query = query.Where(u => u.statoAccount == filtri.statoAccount);
        //lista filtrata
        var utenti = await query.Select((ute) => _mapper.Map<ResUtenteDTO>(ute)).ToListAsync();
        return utenti;
    }
}