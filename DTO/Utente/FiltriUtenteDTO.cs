using System.ComponentModel.DataAnnotations;

namespace DTO.Utente;

public class FiltriUtenteDTO
{
    [StringLength(50)]
    public string? nome { get; set; }

    [StringLength(50)]
    public string? cognome { get; set; }

    [StringLength(50)]
    public string? email { get; set; }
    
    public bool? statoAccount { get; set; }

}