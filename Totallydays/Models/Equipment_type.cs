using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Totallydays.Models
{
    public class Equipment_type
    {
        [Key]
        public int Equipment_type_id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual IEnumerable<Equipment> Equipments { get; set; }
    }
}
