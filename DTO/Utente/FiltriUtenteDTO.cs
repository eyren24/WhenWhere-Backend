using System.ComponentModel.DataAnnotations;

namespace DTO.Utente;

public class FiltriUtenteDTO
{
    [StringLength(50)]
    public string nome { get; set; } = null!;

    [StringLength(50)]
    public string cognome { get; set; } = null!;

    [StringLength(50)]
    public string email { get; set; } = null!;
    
    public bool statoAccount { get; set; } = true;

}