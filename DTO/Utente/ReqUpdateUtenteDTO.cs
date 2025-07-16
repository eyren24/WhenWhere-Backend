using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DTO.Utente;

public class ReqUpdateUtenteDTO
{

    [StringLength(50)]
    public string nome { get; set; } = null!;

    [StringLength(50)]
    public string cognome { get; set; } = null!;

    public string fotoProfilo { get; set; } = null!;

    public bool preferenzeNotifiche { get; set; } = true;
}