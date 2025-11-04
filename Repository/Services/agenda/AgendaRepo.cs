using System.ComponentModel.DataAnnotations;
using Auth.Interfaces;
using AutoMapper;
using Database.Data;
using Database.Models;
using DTO.Agenda;
using Microsoft.EntityFrameworkCore;
using Repository.interfaces;

namespace Repository.Services.agenda;

public class AgendaRepo(AppDbContext _context, IMapper _mapper, ITokenService _token) : IAgendaRepo
{
    // --- Query base: include tutte le relazioni ---
    private IQueryable<Agenda> BaseAgendaQuery()
    {
        return _context.Agenda
            .Include(a => a.utente)
            .Include(a => a.Likes)
            .Include(a => a.Evento)
            .Include(a => a.Nota);
    }

    // --- Crea nuova agenda ---
    public async Task<int> AddAsync(ReqAgendaDTO agenda)
    {
        var tokeninfo = _token.GetInfoToken();
        if (tokeninfo == null)
            throw new Exception("Token non valido");

        var modello = _mapper.Map<Agenda>(agenda);
        modello.utenteId = tokeninfo.utenteId;

        await _context.Agenda.AddAsync(modello);
        await _context.SaveChangesAsync();

        return modello.id;
    }

    // --- Aggiorna agenda esistente ---
    public async Task UpdateAsync(int id, ReqUpdateAgenda agendaUpdt)
    {
        var modello = await _context.Agenda.FirstOrDefaultAsync(p => p.id == id);
        if (modello == null)
            throw new Exception($"Agenda non trovata con ID: {id}");

        modello.nomeAgenda = agendaUpdt.nomeAgenda;
        modello.descrizione = agendaUpdt.descrizione;
        modello.tema = agendaUpdt.tema ?? "#03ace4";
        modello.isprivate = agendaUpdt.isprivate;

        await _context.SaveChangesAsync();
    }

    // --- Elimina agenda ---
    public async Task RemoveAsync(int agendaId)
    {
        var agenda = await _context.Agenda.FindAsync(agendaId);
        if (agenda == null)
            throw new Exception($"Agenda non trovata con ID: {agendaId}");

        _context.Agenda.Remove(agenda);
        await _context.SaveChangesAsync();
    }

    // --- Agende personali dell'utente loggato ---
    public async Task<List<ResAgendaDTO>> GetPersonalAgenda()
    {
        var tokeninfo = _token.GetInfoToken();
        if (tokeninfo == null)
            throw new Exception("Token non valido");

        var agende = await BaseAgendaQuery()
            .Where(a => a.utenteId == tokeninfo.utenteId)
            .ToListAsync();

        return agende.Select(a =>
        {
            var dto = _mapper.Map<ResAgendaDTO>(a);
            dto.likesCount = a.Likes.Count;
            dto.hasLiked = a.Likes.Any(l => l.utenteid == tokeninfo.utenteId);
            return dto;
        }).ToList();
    }

    // --- Tutte le agende pubbliche ---
    public async Task<List<ResAgendaDTO>> GetAll()
    {
        var tokeninfo = _token.GetInfoToken();

        var liste = await BaseAgendaQuery()
            .Where(a => !a.isprivate)
            .OrderByDescending(a => a.Likes.Count)
            .ToListAsync();

        return liste.Select(a =>
        {
            var dto = _mapper.Map<ResAgendaDTO>(a);
            dto.likesCount = a.Likes.Count;
            dto.hasLiked = tokeninfo != null && a.Likes.Any(l => l.utenteid == tokeninfo.utenteId);
            return dto;
        }).ToList();
    }

    // --- Tutte le agende che hai messo like ---
    public async Task<List<ResAgendaDTO>> GetAllLiked()
    {
        var tokeninfo = _token.GetInfoToken();
        if (tokeninfo == null)
            throw new Exception("Token non valido");

        var agendeLiked = await BaseAgendaQuery()
            .Where(a => a.Likes.Any(l => l.utenteid == tokeninfo.utenteId))
            .ToListAsync();

        return agendeLiked.Select(a =>
        {
            var dto = _mapper.Map<ResAgendaDTO>(a);
            dto.likesCount = a.Likes.Count;
            dto.hasLiked = true; // l’utente ha già messo like
            return dto;
        }).ToList();
    }

    // --- Top 10 agende per like ---
    public async Task<List<ResAgendaDTO>> ListTopAgendeAsync()
    {
        var tokeninfo = _token.GetInfoToken();

        var top10 = await BaseAgendaQuery()
            .Where(a => !a.isprivate)
            .OrderByDescending(a => a.Likes.Count)
            .Take(10)
            .ToListAsync();

        return top10.Select(a =>
        {
            var dto = _mapper.Map<ResAgendaDTO>(a);
            dto.likesCount = a.Likes.Count;
            dto.hasLiked = tokeninfo != null && a.Likes.Any(l => l.utenteid == tokeninfo.utenteId);
            return dto;
        }).ToList();
    }

    // --- Agenda per ID ---
    public async Task<ResAgendaDTO> GetById(int id)
    {
        var tokeninfo = _token.GetInfoToken();

        var agenda = await BaseAgendaQuery()
            .FirstOrDefaultAsync(a => a.id == id);

        if (agenda == null)
            throw new Exception($"Agenda con id {id} non trovata");

        var dto = _mapper.Map<ResAgendaDTO>(agenda);
        dto.likesCount = agenda.Likes.Count;
        dto.hasLiked = tokeninfo != null && agenda.Likes.Any(l => l.utenteid == tokeninfo.utenteId);

        return dto;
    }

    // --- Agende pubbliche di un utente per username ---
    public async Task<List<ResAgendaDTO>> GetByOwner(string username)
    {
        var tokeninfo = _token.GetInfoToken();

        var agende = await BaseAgendaQuery()
            .Where(a => a.utente.username == username && !a.isprivate)
            .ToListAsync();

        return agende.Select(a =>
        {
            var dto = _mapper.Map<ResAgendaDTO>(a);
            dto.likesCount = a.Likes.Count;
            dto.hasLiked = tokeninfo != null && a.Likes.Any(l => l.utenteid == tokeninfo.utenteId);
            return dto;
        }).ToList();
    }
}