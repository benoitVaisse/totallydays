using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Totallydays.ViewsModel
{
    public class FormHostingBookingValidation
    {

        [Required]
        [RegularExpression("/d+", ErrorMessage = "Un problème est survenue lors de la gestion de la reservation")]
        public string BookingId { get; set; }

        [Required]
        [RegularExpression("[1-2]", ErrorMessage = "Un problème est survenue lors de la gestion de la reservation")]
        public string Status { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Vous devez laissez un message")]
        [MaxLength(250, ErrorMessage ="Le message dois conteni moin de 250 caractères")]
        public string Comment { get; set; }
    }
}
