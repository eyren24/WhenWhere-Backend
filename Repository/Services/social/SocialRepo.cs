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

}