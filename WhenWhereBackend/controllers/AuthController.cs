using System.ComponentModel.DataAnnotations;
using Auth.dto;
using Auth.Interfaces;
using DTO.Auth;
using Microsoft.AspNetCore.Mvc;
using Repository.interfaces;
using WhenWhereBackend.DecoratoriCustom;

namespace WhenWhereBackend.controllers;

public class AuthController(IAuthRepository _authRepository, ITokenService tokenService) : CustomController
{
    [HttpPost]
    [ProducesResponseType(typeof(ResAuthToken), StatusCodes.Status200OK)]
    public async Task<IActionResult> Login([Required] ReqLoginUser req)
    {
        try
        {
            var res = await _authRepository.LoginAsync(req);
            return Ok(new ResAuthToken
            {
                Token = tokenService.CreateToken(res.nomeCompleto, res.utenteId, res.ruolo),
                RefreshToken = await _authRepository.GenerateRefreshTokenAsync(res.utenteId)
            });
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    [ProducesResponseType(typeof(ResAuthToken), StatusCodes.Status200OK)]
    public async Task<IActionResult> Register(ReqRegisterUser req)
    {
        try
        {
            var res = await _authRepository.RegisterAsync(req);

            return Ok(new ResAuthToken()
            {
                Token = tokenService.CreateToken(res.nomeCompleto, res.utenteId, res.ruolo),
                RefreshToken = await _authRepository.GenerateRefreshTokenAsync(res.utenteId)
            });
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    [ProducesResponseType(typeof(ResAuthToken), StatusCodes.Status200OK)]
    public async Task<IActionResult> RefreshToken(string oldRefresh)
    {
        var data = await _authRepository.RefreshTokenAsync(oldRefresh);
        if (data.tokenInfo == null || data.newRefreshToken == null)
            return BadRequest("RefreshToken scaduto.");

        return Ok(new ResAuthToken()
        {
            Token = tokenService.CreateToken(data.tokenInfo.nomeCompleto, data.tokenInfo.utenteId,
                data.tokenInfo.ruolo),
            RefreshToken = data.newRefreshToken
        });
    }

    [HttpGet]
    [AuthorizeRole(ERuolo.Utente)]
    [ProducesResponseType(typeof(TokenInfoDTO), StatusCodes.Status200OK)]
    public async Task<ActionResult<TokenInfoDTO>> GetUserInfo()
    {
        var infoToken = tokenService.GetInfoToken();
        if (infoToken == null)
            return Unauthorized("Token non valido o claims mancanti");

        return Ok(infoToken);
    }

}