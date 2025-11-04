using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DTO.Evento;

public class ReqEventoDTO
{
    [Required] public int agendaId { get; set; }

    [Required]
    [Column(TypeName = "datetime")]
    public DateTime dataInizio { get; set; }

    [Required]
    [Column(TypeName = "datetime")]
    public DateTime? dataFine { get; set; } = null;

    [Required] [Column(TypeName = "text")] public string? descrizione { get; set; } = null;

    [Required]
    [Column(TypeName = "decimal(1, 1)")]
    public decimal? rating { get; set; } = null;

    [Required] [StringLength(50)] public string luogo { get; set; } = null!;

    [Required] [StringLength(50)] public string stato { get; set; } = null!;

    [Required] public bool notifica { get; set; }

    [Required] [StringLength(50)] public string titolo { get; set; } = null!;

    [Required] public int tagId { get; set; }
}