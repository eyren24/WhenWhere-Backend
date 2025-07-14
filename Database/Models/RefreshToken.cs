using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Database.Models;

public partial class RefreshToken
{
    [Key]
    public int id { get; set; }

    [Unicode(false)]
    public string token { get; set; } = null!;

    public DateTime dataScadenza { get; set; }

    public int utenteId { get; set; }

    [ForeignKey("utenteId")]
    [InverseProperty("RefreshToken")]
    public virtual Utente utente { get; set; } = null!;
}
