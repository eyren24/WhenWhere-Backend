using System.ComponentModel.DataAnnotations;

namespace DTO.Utente;

public class TaggedUsersDTO
{
    [Required] public string username { get; set; } = null!;
    [Required] public int userId { get; set; }
}