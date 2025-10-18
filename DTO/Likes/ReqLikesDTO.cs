using System.ComponentModel.DataAnnotations;

namespace DTO.Likes;

public class ReqLikesDTO
{
    
    [Required] public int utenteid { get; set; }

    [Required] public int agendaid { get; set; }
}