using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Database.Models;

public partial class Ruoli
{
    [Key]
    public int id { get; set; }

    public string nome { get; set; } = null!;
}
