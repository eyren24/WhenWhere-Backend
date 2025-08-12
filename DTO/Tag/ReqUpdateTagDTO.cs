using System.ComponentModel.DataAnnotations;

namespace DTO.Tag;

public class ReqUpdateTagDTO
{
    [Required]
    [StringLength(50)]
    public string nome { get; set; } = null;
}