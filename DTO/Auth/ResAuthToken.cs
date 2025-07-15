using System.ComponentModel.DataAnnotations;

namespace DTO.Auth;

public class ResAuthToken
{
    [Required] public string Token { get; set; } = null!;
    [Required] public string RefreshToken { get; set; } = null!;
}