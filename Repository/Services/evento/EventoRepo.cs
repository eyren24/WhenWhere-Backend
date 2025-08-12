using AutoMapper;
using Database.Data;
using Database.Models;
using DTO.Agenda;
using DTO.Evento;
using Microsoft.EntityFrameworkCore;
using Repository.interfaces;

namespace Repository.services.evento;

public class EventoRepo(AppDbContext _context, IMapper _mapper) : IEventoRepo
{
    public async Task<List<ResEventoDTO>> GetAllAsync(int agendaId, FiltriAgendaDTO filtri)
    {
        var query = _context.Evento.Where(n => n.agendaId == agendaId).AsQueryable();
        if (!string.IsNullOrWhiteSpace(filtri.titolo))
        {
            query = query.Where(n => n.titolo.Contains(filtri.titolo));
        }

        if (filtri.tagId != null)
        {
            query = query.Where(n => n.tagId == filtri.tagId);
        }

        return await query.Select(p => _mapper.Map<ResEventoDTO>(p)).ToListAsync();
    }

    public async Task<int> AddAsync(ReqEventoDTO evento)
    {
        var modello = _mapper.Map<Evento>(evento);
        await _context.Evento.AddAsync(modello);
        await _context.SaveChangesAsync();
        return modello.id;
    }

    public async Task RemoveAsync(int eventoId)
    {
        var evento = await _context.Evento.FindAsync(eventoId);
        if (evento == null)
            throw new Exception($"Evento non trovato con ID: {eventoId}");
        _context.Evento.Remove(evento);
        await _context.SaveChangesAsync();
    }


    public async Task UpdateAsync(int id, ReqUpdateEventoDTO eventoDto)
    {
        var modello = await _context.Evento.FindAsync(id);
        if (modello == null)
        {
            throw new Exception($"Evento con ID: {id} non trovato");
        }

        modello.titolo = eventoDto.titolo;
        modello.stato = eventoDto.stato;
        modello.descrizione = eventoDto.descrizione;
        modello.tagId = eventoDto.tagId;
        _context.Evento.Update(modello);
        await _context.SaveChangesAsync();
    }
    
    public async Task<ResEventoDTO> GetByTitle(int agendaId, string titolo)
    {
        var evento = await _context.Evento.Where((p) => p.titolo == titolo).FirstOrDefaultAsync();
        if (evento ==  null)
        {
            throw new Exception($"Evento con titolo: {titolo} non trovato");
        }

        return _mapper.Map<ResEventoDTO>(evento);
    }
}