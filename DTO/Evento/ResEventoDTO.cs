using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DTO.Evento;

public class ResEventoDTO
{
    
    [Key]
    [Required]
    public int id { get; set; }

    [Required]
    public int agendaId { get; set; }

    [Column(TypeName = "datetime")]
    [Required]
    public DateTime dataInizio { get; set; }

    [Column(TypeName = "datetime")]
    [Required]
    public DateTime? dataFine { get; set; } = null;

    [Column(TypeName = "text")] [Required] public string? descrizione { get; set; } = null;

    [Column(TypeName = "decimal(1, 1)")]
    [Required]
    public decimal? rating { get; set; } = null;

    [StringLength(50)]
    [Required]
    public string luogo { get; set; } = null!;

    [StringLength(50)]
    [Required]
    public string stato { get; set; } = null!;

    [Required]
    public bool notifica { get; set; }

    [Column(TypeName = "datetime")]
    [Required]
    public DateTime dataCreazione { get; set; }

    [StringLength(50)]
    [Required]
    public string titolo { get; set; } = null!;

    [Required]
    public int tagId { get; set; }
}