using Castle.DynamicProxy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Totallydays.Data;
using Totallydays.Repositories;

namespace Totallydays.Models
{
    public class AppUser : IdentityUser<int>
    {
        private readonly TotallydaysContext dbContext;
        public AppUser(TotallydaysContext dbContext)
        {
            this.dbContext = dbContext;
            var rating = this.dbContext.Comments.Where(c => c.Hosting != null).Where(c => c.User_receiver == this).GroupBy(c => c.User_receiver)
                       .Select(c => new { average = c.Sum(t => t.Rating / c.Count()) });
            //var Average = rating.First();
            //this.TotalAverageHosting = (float)Average;
        }
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

        [NotMapped]
        public string FullName { get { return this.Firstname + " " + this.Lastname; } }

        [NotMapped]
        public float TotalAverageHosting { 
            get 
            {
                var total = 0;
                if (this.TotalCommentCountHosting == 0)
                    return 0;

                foreach (var h in this.Hostings)
                {
                    foreach (var c in h.Comments)
                    {
                        total += c.Rating;
                    }
                }
                return total / this.TotalCommentCountHosting;
            } 
        }

        public int TotalCommentCountHosting
        {
            get
            {
                int total = 0;
                foreach (var h in this.Hostings)
                {
                    total += h.Comments.Count();
                }

                return total;
            }
        }
    }
}
