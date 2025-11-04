using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DTO.Utente;
using DTO.Evento;
using DTO.Likes;
using DTO.Nota;

namespace DTO.Agenda;

public class ResAgendaDTO
{
    [Required] public int id { get; set; }

    [Required] public int utenteId { get; set; }

    [Required] public string nomeAgenda { get; set; } = null!;

    [Column(TypeName = "text")] public string? descrizione { get; set; }

    [StringLength(50)] [Required] public string tema { get; set; } = "#03ace4";

    [Required] public bool isprivate { get; set; } = true;

    // --- Relazioni ---
    [Required] public ResUtenteDTO utente { get; set; } = null!;

    public List<ResEventoDTO> Evento { get; set; } = new();

    public List<ResNotaDTO> Nota { get; set; } = new();

    public List<ResLikesDTO> Likes { get; set; } = new();
    public int likesCount { get; set; }

    public bool hasLiked { get; set; }
}