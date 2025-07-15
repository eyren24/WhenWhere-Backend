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
            return Ok(await _agendaRepo.AddAsync(agenda));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpPost]
    [AuthorizeRole(ERuolo.Utente)]
    public async Task<ActionResult> GetAllAgenda()
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
}