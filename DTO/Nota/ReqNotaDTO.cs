using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DTO.Nota;

public class ReqNotaDTO
{
    [Required] public int agendaId { get; set; }
    [StringLength(50)] public string titolo { get; set; } = null!;
    [Column(TypeName = "text")] [Required] public string? descrizione { get; set; }
    [Column(TypeName = "datetime")]
    [Required]
    public DateTime dataCreazione { get; set; }
    [StringLength(50)] public string tema { get; set; } = null!;
    [Required] public int tagId { get; set; }
}