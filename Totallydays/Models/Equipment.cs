using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Totallydays.Models
{
    public class Equipment
    {
        [Key]
        public int Equipment_id { get; set; }

        [Required]
        public virtual Equipment_type Equipement_type { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual IEnumerable<Hosting_Equipment> Hosting_Equipment { get; set; }
    }
}
