using System.ComponentModel.DataAnnotations;
using Auth.dto;
using DTO.Agenda;
using DTO.Nota;
using DTO.Utente;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository.interfaces;
using WhenWhereBackend.DecoratoriCustom;

namespace WhenWhereBackend.controllers;

public class UtenteController(IUtenteRepo _utenteRepo) : CustomController
{
    [HttpGet]
    [AuthorizeRole(ERuolo.Amministratore)]
    public async Task<ActionResult<List<ResUtenteDTO>>> GetAllAsync([Required] int agendaId, FiltriUtenteDTO filtri)
    {
        try
        {
            return Ok(await _utenteRepo.GetListAsync(filtri));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete]
    [AuthorizeRole(ERuolo.Utente)]
    public async Task<ActionResult<List<ResUtenteDTO>>> ToggleStatusAsync([Required] int utenteId)
    {
        try
        {
            await _utenteRepo.ToggleStatusAsync(utenteId);
            return Ok("Stato dell'account aggiornato con successo!");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut]
    [AuthorizeRole(ERuolo.Utente)]
    public async Task<ActionResult<List<ResUtenteDTO>>> UpdateAsync([Required] int utenteId,
        [Required] ReqUpdateUtenteDTO utente)
    {
        try
        {
            await _utenteRepo.UpdateAsync(utenteId, utente);
            return Ok("Utente aggiornato con successo!");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<ResUtenteDTO>> GetById([Required] int utenteId)
    {
        try
        {
            var res = await _utenteRepo.GetUtenteByIdAsync(utenteId);
            return Ok(res);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

}