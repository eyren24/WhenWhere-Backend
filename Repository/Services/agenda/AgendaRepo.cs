using Database.Data;
using Database.Models;
using DTO.Agenda;
using Microsoft.EntityFrameworkCore;

namespace Repository.Services.agenda;

public class AgendaRepo(AppDbContext _context)
{
    public async Task<int> AddAsync(ReqAgendaDTO agenda)
    {
        var modello = new Agenda()
        {
            utenteId = agenda.utenteId,
            nomeAgenda = agenda.nomeAgenda,
            descrizione = agenda.descrizione,
            tema = agenda.tema,
        };
        await _context.Agenda.AddAsync(modello);
        await _context.SaveChangesAsync();
        return modello.id;
    }
    
    public async Task<bool> UpdateAsync(int id, ReqUpdateAgenda agendaUpdt)
    {
        var modello = await _context.Agenda.FirstOrDefaultAsync(p => p.id == id);
        if (modello == null)
        {
            return false;
        }
        
        modello.nomeAgenda = agendaUpdt.nomeAgenda;
        modello.descrizione = agendaUpdt.descrizione;
        modello.tema = agendaUpdt.tema;

        await _context.SaveChangesAsync();
        return true;
    }
    
}