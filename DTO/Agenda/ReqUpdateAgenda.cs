namespace DTO.Agenda;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class ReqUpdateAgenda
{
    [Required] public string nomeAgenda { get; set; } = null!;

    [Column(TypeName = "text")] public string? descrizione { get; set; } = null;

    [StringLength(50)] public string tema { get; set; } = null!;
    public bool isprivate { get; set; } = false;
}