using DTO.Auth;

namespace Repository.Interfaces;

public interface ITokenService
{
    string CreateToken(string nomeCompleto, int utenteId, ERuolo ruolo);
    TokenInfoDTO? GetInfoToken();
}