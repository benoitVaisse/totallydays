
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Totallydays.Models
{
    public class Bed
    {
        [Key]
        public int Bed_id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Image { get; set; }

        public virtual IEnumerable<Bedroom_Bed> Bedroom_Beds { get; set; }

    }
}
