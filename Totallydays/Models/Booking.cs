using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Totallydays.Models
{
    public class Booking
    {
        [NotMapped]
        public const int PENDING = 0;
        [NotMapped]
        public const int VALIDATED = 1;
        [NotMapped]
        public const int CANCELLED = 2;


        [Key]
        public int Booking_id { get; set; }

        public int HostingHosting_id { get; set; }
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

        public byte Validated { get; set; }

        public virtual Comment Rating { get; set; }


        /// <summary>
        /// return list of string begin by start date en finich by end date
        /// </summary>
        /// <returns></returns>
        public List<string> GetDate()
        {
            List<string> days = new List<string>();
            for (DateTime date = this.Start_date; date <= this.End_date; date = date.AddDays(1))
            {
                days.Add(date.ToString("yyyy-MM-dd"));
            }

            return days;
        }

        /// <summary>
        /// llok if date of booking is bookingable before add booking in datebase
        /// </summary>
        /// <returns></returns>
       public bool IsBookingableDate()
       {
            var UnavailableDate = this.Hosting.getUnavailableDays();
            IEnumerable<string> notAvailable = UnavailableDate.Select(u => u.ToString("yyyy-MM-dd"));
            IEnumerable<string> Days = this.GetDate();

            foreach (string d in Days)
            {
                if (notAvailable.Contains(d))
                {
                    return false;
                }
            }

            return true;
       }


    }
}
