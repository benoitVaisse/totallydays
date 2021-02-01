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
        [Required]
        public string Firstname { get; set; }

        [Required]
        public string Lastname { get; set; }

        public string Description { get; set; }

        public string DescriptionShow()
        {
            return System.Web.HttpUtility.HtmlEncode(this.Description).Replace(Environment.NewLine, "<br />");
        }

        public string Picture { get; set; }

        public bool Active { get; set; }

        public virtual IEnumerable<Booking> Bookings { get; set; }

        public virtual IEnumerable<Hosting> Hostings { get; set; }

        public virtual IEnumerable<Comment> Comments_emmit { get; set; }

        public virtual IEnumerable<Comment> Comments_receive { get; set; }

        [NotMapped]
        public string FullName { get { return this.Firstname + " " + this.Lastname; } }

        [NotMapped]
        public float TotalAverageHosting { get {

                if (!this.Hostings.Any())
                {
                    return 0;
                }
                return (float)this.Hostings.Where(h => h.Note != 0).Select(h => h.Note).Average();
                
                
            } }

        [NotMapped]
        public int TotalCommentCountHosting { get {

                return this.Hostings.Select(h => h.NbComment).Count();
            } }


        /// <summary>
        /// retorune le nombre d'hebergement qui on des reservation en attente
        /// </summary>
        /// <returns></returns>
        public int GetNumberHostingWithBookingPending()
        {
            if(this.Hostings.Count() > 0)
            {
                return this.Hostings.Where(h => h.getNumberBookingPending() > 0).Count();
            }
            else
            {
                return 0;
            }
        }
    }
}
