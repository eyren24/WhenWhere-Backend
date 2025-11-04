using Auth.dto;
using DTO.admin;
using Microsoft.AspNetCore.Mvc;
using Repository.interfaces;
using WhenWhereBackend.DecoratoriCustom;

namespace WhenWhereBackend.controllers;

public class AdminController(IAdminRepo _adminRepo) : CustomController
{
    [HttpGet]
    [AuthorizeRole(ERuolo.Amministratore)]
    public async Task<ActionResult<ResAdminStatsDTO>> GetStatsAsync()
    {
        try
        {
            var stats = await _adminRepo.GetDashboardStatsAsync();
            return Ok(stats);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet]
    [AuthorizeRole(ERuolo.Amministratore)]
    public async Task<ActionResult<ResAdminAgendeStatsDTO>> GetAgendeStatsAsync()
    {
        try
        {
            var stats = await _adminRepo.GetAgendeStatsAsync();
            return Ok(stats);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}