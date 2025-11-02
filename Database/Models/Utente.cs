using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Database.Models;

[Index("email", Name = "IX_Utente", IsUnique = true)]
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

    [Column(TypeName = "datetime")]
    public DateTime lastLogin { get; set; }

    public bool preferenzeNotifiche { get; set; }

    public bool statoAccount { get; set; }

    public int ruoloId { get; set; }

    [StringLength(50)]
    public string username { get; set; } = null!;

    [InverseProperty("utente")]
    public virtual ICollection<Agenda> Agenda { get; set; } = new List<Agenda>();

    [InverseProperty("utente")]
    public virtual ICollection<Likes> Likes { get; set; } = new List<Likes>();

    [InverseProperty("utente")]
    public virtual ICollection<RefreshToken> RefreshToken { get; set; } = new List<RefreshToken>();
}
