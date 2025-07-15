using System.ComponentModel.DataAnnotations;
using Auth.dto;
using DTO.Agenda;
using DTO.Nota;
using Microsoft.AspNetCore.Mvc;
using Repository.interfaces;
using WhenWhereBackend.DecoratoriCustom;

namespace WhenWhereBackend.controllers;

public class NotaController(INotaRepo _notaRepo) : CustomController
{
    [HttpPost]
    [AuthorizeRole(ERuolo.Utente)]
    public async Task<IActionResult> AddAsync([Required] ReqNotaDTO nota)
    {
        try
        {
            await _notaRepo.AddAsync(nota);
            return Ok($"Nota creata con successo!");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet] [AuthorizeRole(ERuolo.Utente)]
    public async Task<ActionResult<List<ResNotaDTO>>> GetAllAsync([Required] int agendaId, FiltriAgendaDTO nota)
    {
        try
        {
            return Ok(await _notaRepo.GetListAsync(agendaId, nota));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpDelete] [AuthorizeRole(ERuolo.Utente)]
    public async Task<ActionResult<List<ResNotaDTO>>> RemoveAsync([Required] int notaId)
    {
        try
        {
            await _notaRepo.RemoveAsync(notaId);
            return Ok("Nota eliminata con successo!");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpPut] [AuthorizeRole(ERuolo.Utente)]
    public async Task<ActionResult<List<ResNotaDTO>>> UpdateAsync([Required] int notaId, [Required] ReqNotaDTO nota)
    {
        try
        {
            await _notaRepo.UpdateAsync(notaId, nota);
            return Ok("Nota aggiornata con successo!");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}