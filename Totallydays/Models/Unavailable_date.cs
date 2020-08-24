using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Totallydays.Models
{
    public class Unavailable_date
    {
        [Key]
        public int Unavailable_date_id { get; set; }

        [Required]
        public virtual Hosting Hosting { get; set; }

        [Required]
        public DateTime Start_date { get; set; }

        [Required]
        public DateTime End_date { get; set; }
    }
}
