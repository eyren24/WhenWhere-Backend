using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Database.Models;

public partial class Agenda
{
    [Key]
    public int id { get; set; }

    public int utenteId { get; set; }

    public string nomeAgenda { get; set; } = null!;

    [Column(TypeName = "text")]
    public string? descrizione { get; set; }

    [StringLength(50)]
    public string tema { get; set; } = null!;

    [InverseProperty("agenda")]
    public virtual ICollection<Evento> Evento { get; set; } = new List<Evento>();

    [InverseProperty("agenda")]
    public virtual ICollection<Nota> Nota { get; set; } = new List<Nota>();

    [ForeignKey("utenteId")]
    [InverseProperty("Agenda")]
    public virtual Utente utente { get; set; } = null!;
}
