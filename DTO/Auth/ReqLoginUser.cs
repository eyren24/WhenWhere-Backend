using System.ComponentModel.DataAnnotations;

namespace DTO.Auth;

public class ReqLoginUser
{
    [Required]
    public string email { get; set; }
    [Required]
    public string password { get; set; }
}