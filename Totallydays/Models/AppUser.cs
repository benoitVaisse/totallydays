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
        public AppUser()
        {
            //var Average = rating.First();
            //this.TotalAverageHosting = (float)Average;
        }
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
        public float TotalAverageHosting { get; set; }

        [NotMapped]
        public int TotalCommentCountHosting { get; set; }
    }
}
