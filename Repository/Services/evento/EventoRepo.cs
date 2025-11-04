using System.Text.RegularExpressions;
using AutoMapper;
using Database.Data;
using Database.Models;
using DTO.Agenda;
using DTO.Evento;
using DTO.Utente;
using Microsoft.EntityFrameworkCore;
using Repository.interfaces;

namespace Repository.services.evento;

public class EventoRepo(AppDbContext _context, IMapper _mapper) : IEventoRepo
{
    public async Task<List<ResEventoDTO>> GetAllAsync(int agendaId, FiltriAgendaDTO filtri)
    {
        var _mentionRegex = new Regex(@"@([A-Za-z0-9_]+)", RegexOptions.Compiled);
        var query = _context.Evento.Where(n => n.agendaId == agendaId).AsQueryable();
        if (!string.IsNullOrWhiteSpace(filtri.titolo))
        {
            query = query.Where(n => n.titolo.Contains(filtri.titolo));
        }

        if (filtri.tagId != null)
        {
            query = query.Where(n => n.tagId == filtri.tagId);
        }
        var eventi = await query.Select(e => _mapper.Map<ResEventoDTO>(e)).ToListAsync();

        // Raccogli tutti gli username (case-insensitive) in un unico set
        var all = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        foreach (var ev in eventi)
            if (!string.IsNullOrWhiteSpace(ev.descrizione))
                foreach (Match m in _mentionRegex.Matches(ev.descrizione))
                    all.Add(m.Groups[1].Value.Trim());

        // Nessuna menzione: ritorna subito
        if (all.Count == 0) return eventi;

        // Risolvi utenti in blocco e indicizzali per username (case-insensitive)
        var users = await _context.Utente
            .AsNoTracking()
            .Where(u => all.Contains(u.username))
            .Select(u => new { u.id, u.username })
            .ToListAsync();
        var byUsername = users.ToDictionary(x => x.username, x => x.id, StringComparer.OrdinalIgnoreCase);

        // Popola TaggedUsers per ogni evento
        foreach (var ev in eventi)
        {
            ev.taggedUsers.Clear();
            if (string.IsNullOrWhiteSpace(ev.descrizione)) continue;

            var seen = new HashSet<string>(StringComparer.OrdinalIgnoreCase); // evita duplicati per evento
            foreach (Match m in _mentionRegex.Matches(ev.descrizione))
            {
                var u = m.Groups[1].Value.Trim();
                if (seen.Add(u) && byUsername.TryGetValue(u, out var id))
                    ev.taggedUsers.Add(new TaggedUsersDTO { username = u, userId = id });
            }
        }

        return eventi;
    }

    public async Task<int> AddAsync(ReqEventoDTO evento)
    {
        evento.dataInizio = DateTime.SpecifyKind(evento.dataInizio, DateTimeKind.Utc);
        if (evento.dataFine.HasValue)
        {
            evento.dataFine = DateTime.SpecifyKind(evento.dataFine.Value, DateTimeKind.Utc);
        }
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
        if (evento == null)
        {
            throw new Exception($"Evento con titolo: {titolo} non trovato");
        }

        return _mapper.Map<ResEventoDTO>(evento);
    }
}