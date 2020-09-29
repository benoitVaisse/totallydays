using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Totallydays.Models
{
    public class Bedroom
    {
        [Key]
        public int Bedroom_id { get; set; }

        [Column("Hosting_id")]
        public int HostingHosting_id { get; set; }
        [Required]
        public virtual Hosting Hosting { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual IEnumerable<Bedroom_Bed> Bedroom_Beds { get; set; }
    }
}
