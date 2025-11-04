using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DTO.Utente;

namespace DTO.social;

public class ResSocialDTO
{
    [Required] public int id { get; set; }

    [Required] public int utenteId { get; set; }

    [Required] public string nomeAgenda { get; set; } = null!;

    [Column(TypeName = "text")]
    public string? descrizione { get; set; }

    [StringLength(50)]
    [Required] public string tema { get; set; } = null!;

    [Required] public bool isprivate { get; set; }

    [Required] public ResUtenteDTO utente { get; set; } = null!;

    public int likesCount { get; set; }
}