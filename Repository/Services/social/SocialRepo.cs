using AutoMapper;
using Database.Data;
using DTO.Agenda;
using DTO.social;
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
}