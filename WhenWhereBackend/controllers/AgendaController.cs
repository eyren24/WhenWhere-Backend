using System.ComponentModel.DataAnnotations;
using Auth.dto;
using DTO.Agenda;
using Microsoft.AspNetCore.Mvc;
using Repository.interfaces;
using WhenWhereBackend.DecoratoriCustom;

namespace WhenWhereBackend.controllers;

[Route("api/[controller]")]
[ApiController]
public class AgendaController(IAgendaRepo _agendaRepo) : CustomController
{
    [HttpPost("AddAgenda")]
    [AuthorizeRole(ERuolo.Utente)]
    public async Task<ActionResult> AddAgenda([FromBody][Required] ReqAgendaDTO agenda)
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

    [HttpPut("UpdateAgenda")]
    [AuthorizeRole(ERuolo.Utente)]
    public async Task<ActionResult> UpdateAgenda(
        [FromQuery][Required] int agendaId,
        [FromBody][Required] ReqUpdateAgenda agenda)
    {
        try
        {
            await _agendaRepo.UpdateAsync(agendaId, agenda);
            return Ok(new { message = "Agenda aggiornata" });
        }
        catch (Exception e)
        {
            return BadRequest(new { error = e.Message });
        }
    }

    [HttpDelete("RemoveAgenda")]
    [AuthorizeRole(ERuolo.Utente)]
    public async Task<ActionResult> RemoveAgenda([FromQuery][Required] int agendaId)
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

    [HttpGet("GetPersonalAgenda")]
    [AuthorizeRole(ERuolo.Utente)]
    public async Task<ActionResult<List<ResAgendaDTO>>> GetPersonalAgenda()
    {
        try
        {
            return Ok(await _agendaRepo.GetPersonalAgenda());
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("GetAllAgende")]
    [AuthorizeRole(ERuolo.Amministratore, ERuolo.Utente)]
    public async Task<ActionResult<List<ResAgendaDTO>>> GetAllAgende()
    {
        try
        {
            return Ok(await _agendaRepo.GetAll());
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("ListTopAgende")]
    public async Task<ActionResult<List<ResAgendaDTO>>> ListTopAgende()
    {
        try
        {
            var res = await _agendaRepo.ListTopAgendeAsync();
            return Ok(res);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
        /*todo:
         
    Task<List<ResAgendaDTO>> GetUserLikes();
    
    
    byAgendaId: (ganedaId: number) => Promise<{ success: boolean, likes?: ResLikesDTO[], error?: string }>;
         
         
         */
        
        
    [HttpGet("GetById")]
    [AuthorizeRole(ERuolo.Utente)]
    public async Task<ActionResult<ResAgendaDTO>> GetById([FromQuery][Required] int agendaId)
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

    [HttpGet("GetByOwner")]
    [AuthorizeRole(ERuolo.Utente)]
    public async Task<ActionResult<List<ResAgendaDTO>>> GetByOwner([FromQuery][Required] string username)
    {
        try
        {
            return Ok(await _agendaRepo.GetByOwner(username));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
