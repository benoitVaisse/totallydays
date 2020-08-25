using Castle.DynamicProxy;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Totallydays.Models
{
    public class AppUser : IdentityUser<int>
    {
        [Required]
        public string Firstname { get; set; }

        [Required]
        public string Lastname { get; set; }

        public string Description { get; set; }

        public string Picture { get; set; }

        public bool Active { get; set; }

        public virtual IEnumerable<Booking> Bookings { get; set; }

        public virtual IEnumerable<Hosting> Hostings { get; set; }

        public virtual IEnumerable<Comment> Comments_emmit { get; set; }

        public virtual IEnumerable<Comment> Comments_receive { get; set; }
    }
}
