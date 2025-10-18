using System.ComponentModel.DataAnnotations;

namespace DTO.Likes;

public class ResLikesDTO
{
    [Required] public int id { get; set; }
    [Required] public int utenteid { get; set; }

    [Required] public int agendaid { get; set; }
}