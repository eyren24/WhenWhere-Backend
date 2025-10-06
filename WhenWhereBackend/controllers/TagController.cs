using System.ComponentModel.DataAnnotations;
using Auth.dto;
using DTO.Tag;
using Microsoft.AspNetCore.Mvc;
using Repository.interfaces;
using WhenWhereBackend.DecoratoriCustom;

namespace WhenWhereBackend.controllers;

public class TagController(ITagRepo _tagRepo) : CustomController
{
    [HttpPost]
    [AuthorizeRole(ERuolo.Amministratore)]
    public async Task<IActionResult> AddAsync([Required] ReqTagDTO tag)
    {
        try
        {
            await _tagRepo.AddAsync(tag);
            return Ok($"Nota creata con successo!");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

    }

    [HttpPut]
    [AuthorizeRole(ERuolo.Utente)]
    public async Task<IActionResult> UpdateAsync(int id, [Required] ReqUpdateTagDTO tagUpdt)
    {
        try
        {
            await _tagRepo.UpdateAsync(id, tagUpdt);
            return Ok("Tag aggiornato con successo!");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete]
    [AuthorizeRole(ERuolo.Amministratore)]
    public async Task<IActionResult> RemoveAsync(int id)
    {
        try
        {
            await _tagRepo.RemoveAsync(id);
            return Ok("Tag eliminato con successo!");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet]
    [AuthorizeRole(ERuolo.Amministratore, ERuolo.Utente)] 
    public async Task<ActionResult<List<ResTagDTO>>> GetListAsync()
    {
        try
        {
            var tags = await _tagRepo.GetListAsync();
            return Ok(tags);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }



    }
}