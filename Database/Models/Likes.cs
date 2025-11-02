using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Database.Models;

public partial class Likes
{
    [Key]
    public int id { get; set; }

    public int utenteid { get; set; }

    public int agendaid { get; set; }

    [ForeignKey("agendaid")]
    [InverseProperty("Likes")]
    public virtual Agenda agenda { get; set; } = null!;

    [ForeignKey("utenteid")]
    [InverseProperty("Likes")]
    public virtual Utente utente { get; set; } = null!;
}
