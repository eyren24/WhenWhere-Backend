using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Tag
{
    public class ReqTagDTO
    {
        [StringLength(50)]
        [Required]
        public string nome { get; set; }

    }
}
