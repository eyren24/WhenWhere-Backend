using System.ComponentModel.DataAnnotations;

namespace DTO.Auth;

public enum ERuolo {
    Amministratore = 1,
    Utente
}

public class TokenInfoDTO {
    [Required] public int UtenteId { get; set; }
    [Required] public string NomeCompleto { get; set; } = null!;
    [Required] public ERuolo Ruolo { get; set; }
}