using DTO.Auth;

namespace Repository.Services.Auth;

public interface ITokenService
{
    string CreateToken(string nomeCompleto, int utenteId, ERuolo ruolo);
    TokenInfoDTO? GetInfoToken();
}