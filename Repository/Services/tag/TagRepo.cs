using AutoMapper;
using Database.Data;
using Database.Models;
using DTO.Tag;
using Microsoft.EntityFrameworkCore;
using Repository.interfaces;

namespace Repository.Services.tag;



public class TagRepo(
    AppDbContext _context,
    IMapper _mapper) : ITagRepo
{
    public async Task<int> AddAsync(ReqTagDTO tag)
    {
        var modello = _mapper.Map<Tag>(tag);
        await _context.Tag.AddAsync(modello);
        await _context.SaveChangesAsync();
        return modello.id;
    }

    public async Task UpdateAsync(int id, ReqUpdateTagDTO tagUpdt)
    {
        var modello = await _context.Tag.FirstOrDefaultAsync(p => p.id == id);
        if (modello == null)
            throw new Exception($"Tag non trovato con ID: {id}");
        if (string.IsNullOrWhiteSpace(tagUpdt.nome))
            throw new Exception("Il nome del tag è obbligatorio");

        modello.nome = tagUpdt.nome;

        await _context.SaveChangesAsync();
    }

    public async Task RemoveAsync(int tagId)
    {
        var tag = await _context.Tag.FindAsync(tagId);
        if (tag == null)
            throw new Exception($"Tag non trovato con ID: {tagId}");

        _context.Tag.Remove(tag);
        await _context.SaveChangesAsync();
    }

    public async Task<List<ResTagDTO>> GetListAsync()
    {
        var query = _context.Tag.AsQueryable();
        return await query
            .Select(t => _mapper.Map<ResTagDTO>(t))
            .ToListAsync();
    }
}