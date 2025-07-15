using Auth.dto;
using DTO.Auth;

namespace Repository.interfaces;

public interface IAuthRepository
{
    Task<TokenInfoDTO> RegisterAsync(ReqRegisterUser newUser);
    Task<TokenInfoDTO> LoginAsync(ReqLoginUser request);
    Task<string> GenerateRefreshTokenAsync(int utenteId, int length = 255);
    Task<(TokenInfoDTO? tokenInfo, string? newRefreshToken)> RefreshTokenAsync(string oldToken);
}