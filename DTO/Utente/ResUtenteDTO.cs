
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DTO.Utente
{
    public class ResUtenteDTO
    {
        [Key]
        public int id { get; set; }

        [StringLength(50)]
        public string nome { get; set; } = null!;

        [StringLength(50)]
        public string cognome { get; set; } = null!;

        [StringLength(50)]
        public string email { get; set; } = null!;
        
        public string username { get; set; } = null!;

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

        public bool preferenzeNotifiche { get; set; } = true;

        public bool statoAccount { get; set; } = true;

        public int ruoloId { get; set; } = 2;
    }
}
