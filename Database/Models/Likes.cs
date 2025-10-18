using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Database.Models;

[Keyless]
public partial class Likes
{
    public int id { get; set; }

    public int utenteid { get; set; }

    public int agendaid { get; set; }

    [ForeignKey("agendaid")]
    public virtual Agenda agenda { get; set; } = null!;

    [ForeignKey("utenteid")]
    public virtual Utente utente { get; set; } = null!;
}
