using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DTO.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Http;

namespace Repository.Services.Auth;

public class TokenService : ITokenService
{
    private readonly string _authKey;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public TokenService(IHttpContextAccessor httpContextAccessor, IConfiguration config)
    {
        _httpContextAccessor = httpContextAccessor;
        _authKey = config["AuthKey"]!;
        if (_authKey == null) throw new Exception("[AuthKey] non trovata.");
    }

    public string CreateToken(string nomeCompleto, int utenteId, ERuolo ruolo)
    {
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authKey));

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, nomeCompleto),
            new(ClaimTypes.PrimarySid, utenteId.ToString()),
            new(ClaimTypes.Role, ruolo.ToString())
        };

        var token = new JwtSecurityToken(
            expires: DateTime.Now.AddHours(8),
            claims: claims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public TokenInfoDTO? GetInfoToken()
    {
        if (_httpContextAccessor.HttpContext == null)
            return null;

        var userClaims = _httpContextAccessor.HttpContext.User.Claims;
        if (!userClaims.Any()) return null;

        var username = userClaims.FirstOrDefault(p => p.Type == ClaimTypes.NameIdentifier);
        var utenteId = userClaims.FirstOrDefault(p => p.Type == ClaimTypes.PrimarySid);
        var ruolo = userClaims.FirstOrDefault(p => p.Type == ClaimTypes.Role);

        if (username == null || utenteId == null || ruolo == null) return null;

        return new TokenInfoDTO
        {
            NomeCompleto = username.Value,
            UtenteId = int.Parse(utenteId.Value),
            Ruolo = Enum.Parse<ERuolo>(ruolo.Value)
        };
    }
}