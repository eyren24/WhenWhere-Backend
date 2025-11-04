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
    public async Task<ResLikesDTO> GetLikeByIdAsync(int id)
    {
        var like = await _context.Likes.FindAsync(id);
        return _mapper.Map<ResLikesDTO>(like);
    }

    public async Task<List<ResLikesDTO>> GetLikeByAgendaIdAsync(int id)
    {
        var likes = await _context.Likes
            .Where(p => p.agendaid == id)
            .Include(p => p.utente)
            .Include(p => p.agenda)
            .ToListAsync();

        return _mapper.Map<List<ResLikesDTO>>(likes);
    }

    public async Task<int> AddLikeAsync(ReqLikesDTO likes)
    {
        var tokeninfo = _token.GetInfoToken();
        if (tokeninfo == null)
            throw new Exception("Token non valido");

        // Verifica che l'agenda esista
        var agenda = await _context.Agenda
            .FirstOrDefaultAsync(a => a.id == likes.agendaid);
        if (agenda == null)
            throw new Exception("Agenda non trovata");

        // Impedisci di mettere like alla propria agenda
        if (agenda.utenteId == tokeninfo.utenteId)
            throw new Exception("Non puoi mettere like alla tua agenda");

        // Evita like duplicati dallo stesso utente
        var exists = await _context.Likes
            .AnyAsync(l => l.agendaid == likes.agendaid && l.utenteid == tokeninfo.utenteId);
        if (exists)
            throw new Exception("Hai già messo like a questa agenda");

        var modello = _mapper.Map<Likes>(likes);
        modello.utenteid = tokeninfo.utenteId;

        await _context.Likes.AddAsync(modello);
        await _context.SaveChangesAsync();

        return modello.id;
    }

    public async Task RemoveLikeAsync(int id)
    {
        var tokeninfo = _token.GetInfoToken();
        if (tokeninfo == null)
            throw new Exception("Token non valido");

        var like = await _context.Likes
            .FirstOrDefaultAsync(p => p.agendaid == id && p.utenteid == tokeninfo.utenteId);
        if (like == null)
            throw new Exception($"Like non trovato con ID: {id}");

        _context.Likes.Remove(like);
        await _context.SaveChangesAsync();
    }

    public async Task<List<ResLikesDTO>> GetListByUserIdAsync(int id)
    {
        var likes = await _context.Likes
            .Where(p => p.utenteid == id)
            .Include(l => l.utente)
            .Include(l => l.agenda)
            .ToListAsync();

        return _mapper.Map<List<ResLikesDTO>>(likes);
    }

    public async Task<bool> GetIfUserLikeAgenda(int agendaId)
    {
        var tokeninfo = _token.GetInfoToken();
        if (tokeninfo == null)
            throw new Exception("Token non valido");

        return await _context.Likes
            .AnyAsync(p => p.agendaid == agendaId && p.utenteid == tokeninfo.utenteId);
    }
}