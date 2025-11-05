using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DTO.Utente;

public class ReqUpdateUtenteDTO
{
    [StringLength(50)] [Required] public string nome { get; set; } = null!;

    [StringLength(50)] [Required] public string cognome { get; set; } = null!;
    [Required] public bool preferenzeNotifiche { get; set; } = true;
    [Required] public string genere { get; set; }
    [Required] public DateTime dataNascita { get; set; }
}