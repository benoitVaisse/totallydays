using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Totallydays.Models
{
    public class Booking
    {
        [Key]
        public int Booking_id { get; set; }

        [Required]
        public virtual Hosting Hosting { get; set; }

        [Required]
        public virtual AppUser User { get; set; }

        [Required]
        public DateTime Start_date { get; set; }

        [Required]
        public DateTime End_date { get; set; }

        [Required]
        public float Amount { get; set; }

        [Required]
        public DateTime Created_at { get; set; }

        public string Comment { get; set; }

        public bool Validated { get; set; }

       


    }
}
