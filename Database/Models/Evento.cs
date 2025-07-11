using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Database.Models;

public partial class Evento
{
    [Key]
    public int id { get; set; }

    public int agendaId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime dataInizio { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? dataFine { get; set; }

    [Column(TypeName = "text")]
    public string? descrizione { get; set; }

    [Column(TypeName = "decimal(1, 1)")]
    public decimal? rating { get; set; }

    [StringLength(50)]
    public string luogo { get; set; } = null!;

    [StringLength(50)]
    public string stato { get; set; } = null!;

    public bool notifica { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime dataCreazione { get; set; }

    [StringLength(50)]
    public string titolo { get; set; } = null!;

    public int tagId { get; set; }

    [ForeignKey("agendaId")]
    [InverseProperty("Evento")]
    public virtual Agenda agenda { get; set; } = null!;

    [ForeignKey("tagId")]
    [InverseProperty("Evento")]
    public virtual Tag tag { get; set; } = null!;
}
