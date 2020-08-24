using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Totallydays.Models
{
    public class Hosting_type
    {
        [Key]
        public int Hosting_type_id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual IEnumerable<Hosting> Hostings { get; set; }
    }
}
