using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Database.Models;

public partial class Tag
{
    [Key]
    public int id { get; set; }

    [StringLength(50)]
    public string nome { get; set; } = null!;

    [InverseProperty("tag")]
    public virtual ICollection<Evento> Evento { get; set; } = new List<Evento>();

    [InverseProperty("tag")]
    public virtual ICollection<Nota> Nota { get; set; } = new List<Nota>();
}
