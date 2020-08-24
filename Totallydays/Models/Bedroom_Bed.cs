using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Totallydays.Models
{
    public class Bedroom_Bed
    {
        [Key]
        public int Bedroom_Bed_id { get; set; }

        [Required]
        public virtual Bed Bed { get; set; }

        [Required]
        public virtual Bedroom Bedroom { get; set; }


        [Required]
        public int Number { get; set; }
    }
}
