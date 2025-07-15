using System.ComponentModel.DataAnnotations;

namespace DTO.Agenda;

public class FiltriAgendaDTO
{
    [MaxLength(50)] public string? titolo { get; set; } = null;
    public int? tagId { get; set; } = null;
}