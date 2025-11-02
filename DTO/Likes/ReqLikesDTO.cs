using System.ComponentModel.DataAnnotations;

namespace DTO.Likes;

public class ReqLikesDTO
{
    [Required] public int agendaid { get; set; }
}