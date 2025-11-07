using System.ComponentModel.DataAnnotations;

namespace DTO.Evento;

public class ReqUpdateEventoDTO
{
    [Required] [StringLength(50)] public string titolo { get; set; } = null!;
    [Required] [StringLength(50)] public string stato { get; set; } = null!; 
    [StringLength(50)] public string? descrizione { get; set; } = null;
    [Required] public int tagId { get; set; }
}