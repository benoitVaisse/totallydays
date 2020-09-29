using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Totallydays.Utilities;

namespace Totallydays.ViewsModel
{
    public class FormBookingViewModel
    {
        [Required]
        public int hosting_id { get; set; }


        [Required]
        [DataType(DataType.Date)]
        [CompareDate("End_date", "<", ErrorMessage = "La date de début de séjour dois être inférieur à la date de fin de séjour")]
        [DateLessGreaterThan(compare: ">",ErrorMessage ="La Date de début de séjour dois etre superieur a celle de aujourdhui")]
        public DateTime Start_date { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [CompareDate("Start_date", ">", ErrorMessage = "La date de fin de séjour dois être supérieur à la date de début de séjour")]
        public DateTime End_date { get; set; }


        [Display(Name ="Laisser un petit commentaire pour votre arrivé si vous le souhaitez")]
        public string Comment { get; set; }
    }
}
