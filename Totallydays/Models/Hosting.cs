using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Totallydays.Models
{
    public class Hosting
    {
        public Hosting()
        {
            this.Average = 0;
        }
        [Key]
        public int Hosting_id { get; set; }

        [Required]
        public virtual Hosting_type Hosting_type { get; set; }

        public int UserId { get; set; }
        [Required]
        public virtual AppUser User { get; set; }
        
        [Required]
        public string Title { get; set; }

        [Required]
        public string Resume { get; set; }
        public string ResumeShow()
        {
            return System.Web.HttpUtility.HtmlEncode(this.Resume).Replace(Environment.NewLine, "<br />");
        }

        [Required]
        public string Description { get; set; }
        public string DescriptionShow()
        {
            return System.Web.HttpUtility.HtmlEncode(this.Description).Replace(Environment.NewLine, "<br />");
        }

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

        public string Lng { get; set; }

        public string Lat { get; set; }
        public bool Published { get; set; }

        public bool Active { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; }

        [DefaultValue(true)]
        public bool Modified { get; set; }

        public string Slug { get; set; }

        public virtual IEnumerable<Image> Images { get; set; }

        public virtual IEnumerable<Booking> Bookings { get; set; }
        public virtual IEnumerable<Unavailable_date> Unavailables_date { get; set; }

        public virtual IEnumerable<Hosting_Equipment> Hosting_Equipment { get; set; }

        public virtual IEnumerable<Bedroom> Bedrooms { get; set; }

        [NotMapped]
        public float Average { get; set; }

        /// <summary>
        /// return average of hosting
        /// </summary>
        /// <returns></returns>
        public float GetAverage()
        {
            float total = 0;
            int comment = 0;
            if (this.Bookings.Count() == 0)
                return 0;

            foreach (Booking b in this.Bookings)
            {
                if(b.Rating != null)
                {
                    total += b.Rating.Rating;
                    comment++;
                }
            }

            return (float)(total / comment);
        }

        public int getNumberComment()
        {
            int comment = 0;
            foreach (Booking b in this.Bookings)
            {
                if(b.Rating != null)
                {
                    comment++;
                }
            }

            return comment;
        }


        /// <summary>
        /// list all booking Date of hosting
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DateTime> getUnavailableDays()
        {
            List<DateTime> NotAvailabkesDays = new List<DateTime>();
            foreach (Booking b in this.getFuturBookingDayUnavailable())
            {
                for(DateTime date = b.Start_date; date<= b.End_date; date = date.AddDays(1))
                {
                    NotAvailabkesDays.Add(date);
                }
            };

            foreach(Unavailable_date d in this.GetMyNextUnavailableDate())
            {
                for (DateTime date = d.Start_date; date <= d.End_date; date = date.AddDays(1))
                {
                    NotAvailabkesDays.Add(date);
                }
            }

            return NotAvailabkesDays;
        }


        /// <summary>
        /// return futur booking of this hosting not cancelled
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Booking> getFuturBookingDayUnavailable()
        {
            return this.Bookings.Where(b=>b.Start_date > DateTime.Now).Where(b => b.Validated != Booking.CANCELLED);
        }

        /// <summary>
        /// retourne les futur dates indisponibles pour un hébergement
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Unavailable_date> GetMyNextUnavailableDate()
        {
            return this.Unavailables_date.Where(u => u.HostingHosting_id == this.Hosting_id).Where(u => u.Start_date > DateTime.Now);
        }

        /// <summary>
        /// retourne les futur dates indisponibles pour un hébergement sous forme de chaine de caractere dans un tableau 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> getUnavailableDaysToArray()
        {
            return this.getUnavailableDays().Select(d=>d.ToString("dd/MM/yyyy")).ToList();
        }


    }
}
