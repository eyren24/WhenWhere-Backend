using Auth.dto;

namespace Auth.Interfaces;

public interface ITokenService
{
    string CreateToken(string nomeCompleto, int utenteId, ERuolo ruolo);
    TokenInfoDTO? GetInfoToken();
}