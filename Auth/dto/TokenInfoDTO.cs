using System.ComponentModel.DataAnnotations;

namespace Auth.dto;

public enum ERuolo {
    Amministratore = 1,
    Utente
}

public class TokenInfoDTO {
    [Required] public int utenteId { get; set; }
    [Required] public string nomeCompleto { get; set; } = null!;
    [Required] public ERuolo ruolo { get; set; }
}