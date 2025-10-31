using System.ComponentModel.DataAnnotations;
using DTO.Agenda;
using DTO.social;
using DTO.Utente;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository.services.social;

namespace WhenWhereBackend.controllers;

public class SocialController(ISocialRepo _socialRepo) : CustomController
{
    [HttpGet]
    public async Task<ActionResult<List<ResSocialDTO>>> ListTopAgendeAsync()
    {
        var res = await _socialRepo.ListTopAgendeAsync();
        return Ok(res);
    }


    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<ResUtenteDTO>> GetUtenteByUsernameAsync([Required] string username)
    {
        try
        {
            var res = await _socialRepo.GetUtenteByUsernameAsync(username);
            return Ok(res);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}