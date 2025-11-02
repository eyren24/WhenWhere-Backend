using AutoMapper;
using Database.Data;
using DTO.Agenda;
using DTO.social;
using DTO.Utente;
using Microsoft.EntityFrameworkCore;

namespace Repository.services.social;

public class SocialRepo(AppDbContext _context, IMapper _mapper) : ISocialRepo
{
    // GET
    public async Task<List<ResSocialDTO>> ListTopAgendeAsync()
    {
        var top10 = await _context.Agenda
            .Where(p => !p.isprivate)
            .Include(a => a.utente) // join esplicito
            .Select(a => new
            {
                Agenda = a,
                LikesCount = _context.Likes.Count(l => l.agendaid == a.id)
            })
            .OrderByDescending(x => x.LikesCount)
            .Take(10)
            .ToListAsync();

        return top10.Select(x =>
        {
            var dto = _mapper.Map<ResSocialDTO>(x.Agenda);
            dto.likesCount = x.LikesCount;
            return dto;
        }).ToList();
    }
    
    public async Task<List<ResSocialDTO>> GetUtenteByUsernameAsync(string username)
    {
        var utente = await _context.Utente
            .Include(u => u.Agenda)
            .FirstOrDefaultAsync(u => u.username == username);

        if (utente == null)
            throw new Exception($"Utente {username} non trovato");

        // Mappiamo solo le agende NON private
        var agendePubbliche = utente.Agenda
            .Where(a => !a.isprivate)
            .Select(agenda =>
            {
                var dto = _mapper.Map<ResSocialDTO>(agenda);
                dto.likesCount = _context.Likes.Count(l => l.agendaid == agenda.id);
                dto.utente = _mapper.Map<ResUtenteDTO>(utente);
                return dto;
            })
            .ToList();

        return agendePubbliche;
    }
 
    public async Task<List<ResSocialDTO>> GetByOwner(string username)
    {
        var agenda = _context.Agenda.Include(t => t.utente)
            .Where(p => p.utente.username == username && !p.isprivate);
        if (agenda == null)
        {
            throw new Exception
                ($"Agenda con proprietario {username} non trovata");
        }

        return await agenda.Select(p=> _mapper.Map<ResSocialDTO>(p)).ToListAsync();
    }
    
}