using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DTO.Nota;

public class ReqUpdateNotaDTO
{
    [StringLength(50)] public string titolo { get; set; } = null!;
    [Column(TypeName = "text")] [Required] public string? descrizione { get; set; }
    [StringLength(50)] public string tema { get; set; } = null!;
    [Required] public int tagId { get; set; }
}