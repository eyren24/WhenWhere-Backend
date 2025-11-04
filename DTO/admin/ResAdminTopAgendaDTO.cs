using System.ComponentModel.DataAnnotations;

namespace DTO.admin;

public class ResAdminTopAgendaDTO
{
    [Required]
    public int Id { get; set; }
    [Required]
    public string NomeAgenda { get; set; } = null!;
    [Required]
    public string Utente { get; set; } = null!;
    [Required]
    public int LikesCount { get; set; }
    [Required]
    public bool IsPrivate { get; set; }
    [Required]
    public string Tema { get; set; } = "#03ace4"; // default colore sito
}