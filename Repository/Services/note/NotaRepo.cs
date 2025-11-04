using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Database.Data;
using Database.Models;
using DTO.Agenda;
using DTO.Nota;
using Microsoft.EntityFrameworkCore;
using Repository.interfaces;

namespace Repository.services.note;

public class NotaRepo(
    AppDbContext _context,
    IMapper _mapper) : INotaRepo
{
    public async Task<int> AddAsync(ReqNotaDTO nota)
    {
        var check = await _context.Nota.AnyAsync(p => p.titolo == nota.titolo && p.dataCreazione == nota.dataCreazione);
        if (check)
        {
            throw new Exception("Una nota con combinazione di titolo e data e' gia esistente");
        }
        var modello = _mapper.Map<Nota>(nota);
        await _context.Nota.AddAsync(modello);
        await _context.SaveChangesAsync();
        return modello.id;
    }
    
    public async Task UpdateAsync(int id, ReqUpdateNotaDTO notaUpdt)
    {
        var modello = await _context.Nota.FirstOrDefaultAsync(p => p.id == id);
        if (modello == null)
            throw new Exception($"Nota non trovata con ID: {id}");
        

        modello.titolo = notaUpdt.titolo;
        modello.descrizione = notaUpdt.descrizione;
        modello.tema = notaUpdt.tema;
        modello.tagId = notaUpdt.tagId;

        await _context.SaveChangesAsync();
        
    }
    
    public async Task RemoveAsync(int notaId)
    {
        var nota = await _context.Nota.FindAsync(notaId);
        if (nota == null) 
            throw new Exception($"Nota non trovata con ID: {notaId}");
        _context.Nota.Remove(nota);
        await _context.SaveChangesAsync();
    }
    
    public async Task<List<ResNotaDTO>> GetListAsync([Required] int agendaId, FiltriAgendaDTO filtri)
    {
        var query = _context.Nota.Where(n => n.agendaId == agendaId).AsQueryable();
        
        if (!string.IsNullOrWhiteSpace(filtri.titolo))
        {
            query = query.Where(n => n.titolo.Contains(filtri.titolo));
        }

        if (filtri.tagId.HasValue)
        {
            query = query.Where(n => n.tagId == filtri.tagId.Value);
        }

        return await query
            .Select(a => _mapper.Map<ResNotaDTO>(a))
            .Where(p=>p.agendaId == agendaId)
            .ToListAsync();
    }
}