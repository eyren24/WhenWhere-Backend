using System.ComponentModel.DataAnnotations;

namespace DTO.admin;

public class ResAdminAgendeStatsDTO
{
    [Required]
    public int TotaleAgende { get; set; }
    [Required]
    public int AgendePubbliche { get; set; }
    [Required]
    public int AgendePrivate { get; set; }
    [Required]
    public int TotaleLikes { get; set; }
    [Required]
    public List<ResAdminTopAgendaDTO> TopAgende { get; set; } = new();
}