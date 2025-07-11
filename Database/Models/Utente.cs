using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Database.Models;

public partial class Utente
{
    [Key]
    public int id { get; set; }

    [StringLength(50)]
    public string nome { get; set; } = null!;

    [StringLength(50)]
    public string cognome { get; set; } = null!;

    [StringLength(50)]
    public string email { get; set; } = null!;

    public string password { get; set; } = null!;

    [StringLength(5)]
    public string? token { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? tokenExpiration { get; set; }

    public DateOnly dataNascita { get; set; }

    [StringLength(50)]
    public string genere { get; set; } = null!;

    public string fotoProfilo { get; set; } = null!;

    public bool isAdmin { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime lastLogin { get; set; }

    public bool preferenzeNotifiche { get; set; }

    public bool statoAccount { get; set; }

    [InverseProperty("utente")]
    public virtual ICollection<Agenda> Agenda { get; set; } = new List<Agenda>();
}
