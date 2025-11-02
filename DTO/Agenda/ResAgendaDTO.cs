using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DTO.Evento;
using DTO.Likes;
using DTO.Nota;
using DTO.Utente;

namespace DTO.Agenda
{
    public class ResAgendaDTO
    {
        [Required] public int id { get; set; }

        [Required] public int utenteId { get; set; }
        [Required] public string nomeAgenda { get; set; } = null!;

        [Required] [Column(TypeName = "text")] public string? descrizione { get; set; } = null;

        [StringLength(50)] [Required] public string tema { get; set; } = null!;
        [Required] public bool isprivate { get; set; } = false;

        [Required] public List<ResLikesDTO> likes { get; set; } = null!;
        [Required] public ResUtenteDTO utente { get; set; } = null!;
        [Required] public List<ResEventoDTO> eventi { get; set; } = null!;
        [Required] public List<ResNotaDTO> note { get; set; } = null!;
    }
}