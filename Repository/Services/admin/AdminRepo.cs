using Auth.dto;
using Auth.Interfaces;
using AutoMapper;
using Database.Data;
using DTO.admin;
using Microsoft.EntityFrameworkCore;
using Repository.interfaces;

namespace Repository.Services.admin;

public class AdminRepo(AppDbContext _context, ITokenService _token, IMapper _mapper) : IAdminRepo
{
       // === Sezione Dashboard generale ===
    public async Task<ResAdminStatsDTO> GetDashboardStatsAsync()
    {
        var tokenInfo = _token.GetInfoToken();
        if (tokenInfo == null)
            throw new Exception("Token non valido");

        if (tokenInfo.ruolo != ERuolo.Amministratore)
            throw new Exception("Accesso consentito solo agli amministratori");

        var now = DateTime.UtcNow;
        var sevenDaysAgo = now.AddDays(-7);

        var totalUtenti = await _context.Utente.CountAsync();
        var utentiAttivi = await _context.Utente.CountAsync(u => u.statoAccount);
        var utentiDisabilitati = await _context.Utente.CountAsync(u => !u.statoAccount);
        var nuoviUtentiSettimana = await _context.Utente
            .CountAsync(u => u.dataCreazione >= sevenDaysAgo);

        var ultimoLoginMedio = await _context.Utente
            .Where(u => u.lastLogin != null)
            .AverageAsync(u => EF.Functions.DateDiffDay(u.lastLogin, now));

        return new ResAdminStatsDTO
        {
            TotaleUtenti = totalUtenti,
            UtentiAttivi = utentiAttivi,
            UtentiDisabilitati = utentiDisabilitati,
            NuoviUtentiUltimi7Giorni = nuoviUtentiSettimana,
            UltimoLoginMedioGiorni = Math.Round(ultimoLoginMedio, 1)
        };
    }

    // === Sezione Agende & Likes ===
    public async Task<ResAdminAgendeStatsDTO> GetAgendeStatsAsync()
    {
        var tokenInfo = _token.GetInfoToken();
        if (tokenInfo == null)
            throw new Exception("Token non valido");

        if (tokenInfo.ruolo != ERuolo.Amministratore)
            throw new Exception("Accesso consentito solo agli amministratori");

        var totaleAgende = await _context.Agenda.CountAsync();
        var agendePubbliche = await _context.Agenda.CountAsync(a => !a.isprivate);
        var agendePrivate = await _context.Agenda.CountAsync(a => a.isprivate);

        var totaleLikes = await _context.Likes.CountAsync();

        var topAgende = await _context.Agenda
            .Include(a => a.utente)
            .Include(a => a.Likes)
            .OrderByDescending(a => a.Likes.Count)
            .Take(10)
            .Select(a => new ResAdminTopAgendaDTO
            {
                Id = a.id,
                NomeAgenda = a.nomeAgenda,
                Utente = a.utente.username,
                LikesCount = a.Likes.Count,
                IsPrivate = a.isprivate,
                Tema = a.tema
            })
            .ToListAsync();

        return new ResAdminAgendeStatsDTO
        {
            TotaleAgende = totaleAgende,
            AgendePubbliche = agendePubbliche,
            AgendePrivate = agendePrivate,
            TotaleLikes = totaleLikes,
            TopAgende = topAgende
        };
    }
}