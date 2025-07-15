using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Agenda
{
    public class ReqAgendaDTO
    {
        [Required]
        public string nomeAgenda { get; set; } = null!;

        [Column(TypeName = "text")]
        public string? descrizione { get; set; } = null;

        [StringLength(50)]
        public string tema { get; set; } = null!;
    }
}
