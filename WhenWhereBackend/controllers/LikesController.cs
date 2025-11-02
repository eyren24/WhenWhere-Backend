using System.ComponentModel.DataAnnotations;
using Auth.dto;
using DTO.Likes;
using Microsoft.AspNetCore.Mvc;
using Repository.interfaces;
using WhenWhereBackend.DecoratoriCustom;

namespace WhenWhereBackend.controllers;

public class LikesController(ILikesRepo _likesRepo) : CustomController
{
    [HttpPost]
    [AuthorizeRole(ERuolo.Utente)]
    public async Task<ActionResult> AddLike([Required] ReqLikesDTO like)
    {
        try
        {
            await _likesRepo.AddLikeAsync(like);
            return Ok("Like aggiunto con successo.");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet]
    [AuthorizeRole(ERuolo.Utente)]
    public async Task<ActionResult<ResLikesDTO>> GetLikeByIdAsync(int id)
    {
        try
        {
            return Ok(await _likesRepo.GetLikeByIdAsync(id));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet]
    [AuthorizeRole(ERuolo.Utente)]
    public async Task<ActionResult<List<ResLikesDTO>>> GetLikeByAgendaIdAsync([Required] int id)
    {
        try
        {
            return Ok(await _likesRepo.GetLikeByAgendaIdAsync(id));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete]
    [AuthorizeRole(ERuolo.Utente)]
    public async Task<ActionResult> RemoveAsync([Required] int id)
    {
        try
        {
            await _likesRepo.RemoveLikeAsync(id);
            return Ok("Like rimosso");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet]
    [AuthorizeRole(ERuolo.Utente)]
    public async Task<ActionResult<List<ResLikesDTO>>> GetListByUserIdAsync([Required] int id)
    {
        try
        {
            return Ok(await _likesRepo.GetListByUserIdAsync(id));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet]
    [AuthorizeRole(ERuolo.Utente)]
    public async Task<ActionResult<bool>> GetIfUserLikeAgenda(int agendaId)
    {
        try
        {
            return Ok(await _likesRepo.GetIfUserLikeAgenda(agendaId));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}