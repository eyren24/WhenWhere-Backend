
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DTO.Utente
{
    public class ResUtenteDTO
    {
        [Key]
        [Required]
        public int id { get; set; }

        [StringLength(50)]
        [Required]
        public string nome { get; set; } = null!;

        [StringLength(50)]
        [Required]
        public string cognome { get; set; } = null!;

        [StringLength(50)]
        [Required]
        public string email { get; set; } = null!;
        
        [Required]
        public string username { get; set; } = null!;

        [Required]
        public DateOnly dataNascita { get; set; }

        [StringLength(50)]
        [Required]
        public string genere { get; set; } = null!;

        [Required]
        public string fotoProfilo { get; set; } = null!;

        [Column(TypeName = "datetime")]
        [Required]
        public DateTime lastLogin { get; set; }

        [Required]
        public bool preferenzeNotifiche { get; set; } = true;

        [Required]
        public bool statoAccount { get; set; } = true;

        [Required]
        public int ruoloId { get; set; } = 2;
    }
}
