using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Agenda
{
    public class ResAgendaDTO
    {
        [Required]
        public int id { get; set; }

        [Required]
        public int utenteId { get; set; }
        [Required]
        public string nomeAgenda { get; set; } = null!;

        [Required]
        [Column(TypeName = "text")]
        public string? descrizione { get; set; } = null;

        [StringLength(50)]
        [Required]
        public string tema { get; set; } = null!;
    }
}
