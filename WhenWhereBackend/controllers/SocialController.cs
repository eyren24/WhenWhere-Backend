using DTO.Agenda;
using DTO.social;
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
}