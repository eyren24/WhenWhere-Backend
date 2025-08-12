using System.ComponentModel.DataAnnotations;
using Auth.dto;
using DTO.Agenda;
using DTO.Evento;
using Microsoft.AspNetCore.Mvc;
using Repository.interfaces;
using WhenWhereBackend.DecoratoriCustom;

namespace WhenWhereBackend.controllers;

public class EventoController(IEventoRepo _eventoRepo) : CustomController
{
    [HttpPost]
    [AuthorizeRole(ERuolo.Utente)]
    public async Task<IActionResult> AddAsync([Required] ReqEventoDTO evento)
    {
        try
        {
            await _eventoRepo.AddAsync(evento);
            return Ok("Evento creato con successo!");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    [AuthorizeRole(ERuolo.Utente)]
    public async Task<ActionResult<List<ResEventoDTO>>> GetAllAsync([Required] int agendaId, [Required] FiltriAgendaDTO filtri)
    {
        try
        {
            return Ok(await _eventoRepo.GetAllAsync(agendaId, filtri));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete]
    [AuthorizeRole(ERuolo.Utente)]
    public async Task<IActionResult> RemoveAsync([Required] int eventoId)
    {
        try
        {
            await _eventoRepo.RemoveAsync(eventoId);
            return Ok("Evento rimosso con successo!");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut]
    [AuthorizeRole(ERuolo.Utente)]
    public async Task<IActionResult> UpdateAsync([Required] int eventoId, [Required] ReqUpdateEventoDTO eveto)
    {
        try
        {
            await _eventoRepo.UpdateAsync(eventoId, eveto);
            return Ok("Evento aggiornato con successo!");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet]
    [AuthorizeRole(ERuolo.Utente)]
    public async Task<ActionResult<ResEventoDTO>> GetEventoByTitle(int agendaId, string titolo)
    {
        try
        {
            return Ok(await _eventoRepo.GetByTitle(agendaId, titolo));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}