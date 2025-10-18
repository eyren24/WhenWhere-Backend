using Auth.Interfaces;
using AutoMapper;
using Database.Data;
using Database.Models;
using DTO.Likes;
using Microsoft.EntityFrameworkCore;
using Repository.interfaces;

namespace Repository.services.likes;

public class LikesRepo(
    AppDbContext _context,
    IMapper _mapper,
    ITokenService _token) : ILikesRepo
{
    public async Task<List<ResLikesDTO>> GetallLikesasync(int id)
    {
        var query = _context.Likes.Where(e => e.id == id).AsQueryable();
        return await query
            .Select(a => _mapper.Map<ResLikesDTO>(a))
            .ToListAsync();
    }

    public async Task<int> AddLikeAsync(ReqLikesDTO likes)
    {
        var tokeninfo = _token.GetInfoToken();

        if (tokeninfo == null)
        {
            throw new Exception("Token non valido");
        }

        var modello = _mapper.Map<Likes>(likes);
        modello.utenteid = tokeninfo.utenteId;
        await _context.Likes.AddAsync(modello);
        await _context.SaveChangesAsync();
        return modello.id;
    }

    public async Task RemoveLikeAsync(int id)
    {
        var like = await _context.Likes.FindAsync(id);
        if (like == null)
            throw new Exception($"Like non trovato con ID: {id}");
        _context.Likes.Remove(like);
        await _context.SaveChangesAsync();
    }

    public async Task<List<ResLikesDTO>> GetListByUserIdAsync(int id)
    {
        var like = await _context.Likes.Where(p => p.utenteid == id)
            .Include(l => l.utente)
            .Include(l => l.agenda).ToListAsync();

        if (like == null)
        {
            throw new Exception("Like non trovato");
        }

        return like.Select(_mapper.Map<ResLikesDTO>).ToList();
    }
}