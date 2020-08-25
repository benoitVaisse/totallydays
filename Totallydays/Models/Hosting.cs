using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Totallydays.Models
{
    public class Hosting
    {
        [Key]
        public int Hosting_id { get; set; }

        [Required]
        public virtual Hosting_type Hosting_type { get; set; }

        [Required]
        public virtual AppUser User { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Resume { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public float Price { get; set; }

        [Required]
        public string Cover_image { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string Post_code { get; set; }

        [Required]
        public string City { get; set; }

        public float Lng { get; set; }

        public float Lat { get; set; }
        public bool Published { get; set; }

        public bool Active { get; set; }

        

        public virtual IEnumerable<Image> Images { get; set; }

        public virtual IEnumerable<Comment> Comments { get; set; }

        public virtual IEnumerable<Booking> Bookings { get; set; }
        public virtual IEnumerable<Unavailable_date> Unavailables_date { get; set; }

        public virtual IEnumerable<Hosting_Equipment> Hosting_Equipment { get; set; }

        public virtual IEnumerable<Bedroom> Bedrooms { get; set; }


    }
}
