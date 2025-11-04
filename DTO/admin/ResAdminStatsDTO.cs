using System.ComponentModel.DataAnnotations;

namespace DTO.admin;

public class ResAdminStatsDTO
{
    [Required] public int TotaleUtenti { get; set; }
    [Required] public int UtentiAttivi { get; set; }
    [Required] public int UtentiDisabilitati { get; set; }
    [Required] public int NuoviUtentiUltimi7Giorni { get; set; }
    [Required] public double UltimoLoginMedioGiorni { get; set; }
}