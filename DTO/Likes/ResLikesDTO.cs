using System.ComponentModel.DataAnnotations;
using DTO.Agenda;
using DTO.Utente;

namespace DTO.Likes;

public class ResLikesDTO
{
    [Required] public int id { get; set; }
    [Required] public int utenteid { get; set; }
    [Required] public int agendaid { get; set; }
    [Required] public ResAgendaDTO agenda { get; set; } = null!;
    [Required] public ResUtenteDTO utente { get; set; } = null!;
}