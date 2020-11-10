using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Totallydays.Models;

namespace Totallydays.ViewsModel
{
    public class FormHostingBookingValidationModel
    {

        public Booking Booking { get; set; }
        public string Comment { get; set; }
    }
}
