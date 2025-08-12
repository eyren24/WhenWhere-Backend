using System.ComponentModel.DataAnnotations;
using Auth.dto;
using DTO.Agenda;
using Microsoft.AspNetCore.Mvc;
using Repository.interfaces;
using WhenWhereBackend.DecoratoriCustom;

namespace WhenWhereBackend.controllers;

public class AgendaController(IAgendaRepo _agendaRepo) : CustomController
{
    [HttpPost]
    [AuthorizeRole(ERuolo.Utente)]
    public async Task<ActionResult> AddAgenda([Required] ReqAgendaDTO agenda)
    {
        try
        {
            await _agendaRepo.AddAsync(agenda);
            return Ok("Agenda creata con successo.");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet]
    [AuthorizeRole(ERuolo.Utente)]
    public async Task<ActionResult<List<ResAgendaDTO>>> GetAllAsync()
    {
        try
        {
            return Ok(await _agendaRepo.GetListAsync());
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete]
    [AuthorizeRole(ERuolo.Utente)]
    public async Task<ActionResult> RemoveAsync([Required] int agendaId)
    {
        try
        {
            await _agendaRepo.RemoveAsync(agendaId);
            return Ok("Agenda rimossa con successo");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut]
    [AuthorizeRole(ERuolo.Utente)]
    public async Task<ActionResult> UpdateAsync([Required] int agendaId, ReqUpdateAgenda agenda)
    {
        try
        {
            await _agendaRepo.UpdateAsync(agendaId, agenda);
            return Ok("Agenda rimossa con successo");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpGet]
    [AuthorizeRole(ERuolo.Utente)]
    public async Task<ActionResult<ResAgendaDTO>> GetByIdAsync([Required] int agendaId)
    {
        try
        {
            return Ok(await _agendaRepo.GetById(agendaId));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}