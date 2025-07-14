using System.ComponentModel.DataAnnotations;

namespace DTO.Auth;

public class ReqRegisterUser
{
    [StringLength(50)]
    [Required]
    public string nome { get; set; } = null!;

    [StringLength(50)]
    [Required]
    public string cognome { get; set; } = null!;

    [StringLength(50)]
    [Required]
    public string email { get; set; } = null!;

    [Required]
    public string password { get; set; } = null!;
    [Required]
    public string confermaPassword { get; set; } = null!;

    [Required]
    public DateOnly dataNascita { get; set; }

    [StringLength(50)]
    [Required]
    public string genere { get; set; } = null!;
}