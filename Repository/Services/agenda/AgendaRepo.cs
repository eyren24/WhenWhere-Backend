using System.ComponentModel.DataAnnotations;
using Auth.Interfaces;
using AutoMapper;
using Database.Data;
using Database.Models;
using DTO.Agenda;
using Microsoft.EntityFrameworkCore;
using Repository.interfaces;

namespace Repository.Services.agenda;

public class AgendaRepo(
    AppDbContext _context,
    IMapper _mapper,
    ITokenService _token) : IAgendaRepo
{
    public async Task<int> AddAsync(ReqAgendaDTO agenda)
    {
        var tokeninfo = _token.GetInfoToken();
        
        if (tokeninfo == null) {
            throw new Exception("Token non valido");
        }
        var modello = _mapper.Map<Agenda>(agenda);
        modello.utenteId = tokeninfo.utenteId;
        await _context.Agenda.AddAsync(modello);
        await _context.SaveChangesAsync();
        return modello.id;
    }

    public async Task UpdateAsync(int id, ReqUpdateAgenda agendaUpdt)
    {
        var modello = await _context.Agenda.FirstOrDefaultAsync(p => p.id == id);
        if (modello == null)
            throw new Exception($"Agenda non trovata con ID: {id}");

        modello.nomeAgenda = agendaUpdt.nomeAgenda;
        modello.descrizione = agendaUpdt.descrizione;
        modello.tema = agendaUpdt.tema;

        await _context.SaveChangesAsync();
    }

    public async Task RemoveAsync(int agendaId)
    {
        var agenda = await _context.Agenda.FindAsync(agendaId);
        if (agenda == null)
            throw new Exception($"Agenda non trovata con ID: {agendaId}");
        _context.Agenda.Remove(agenda);
        await _context.SaveChangesAsync();
        
    }

    public async Task<List<ResAgendaDTO>> GetListAsync()
    {
        var query = _context.Agenda.AsQueryable();
        return await query
            .Select(a => _mapper.Map<ResAgendaDTO>(a))
            .ToListAsync();
    }
}