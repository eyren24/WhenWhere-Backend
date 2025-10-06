using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Database.Models;

public partial class Nota
{
    [Key]
    public int id { get; set; }

    public int agendaId { get; set; }

    [StringLength(50)]
    public string titolo { get; set; } = null!;

    [Column(TypeName = "text")]
    public string? descrizione { get; set; }

    [Column(TypeName = "smalldatetime")]
    public DateTime dataCreazione { get; set; }

    [StringLength(50)]
    public string tema { get; set; } = null!;

    public int tagId { get; set; }

    [ForeignKey("agendaId")]
    [InverseProperty("Nota")]
    public virtual Agenda agenda { get; set; } = null!;

    [ForeignKey("tagId")]
    [InverseProperty("Nota")]
    public virtual Tag tag { get; set; } = null!;
}
