using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Totallydays.ViewsModel
{
    public class FormCommentViewModel
    {
        [Required]
        [Range(0,5, ErrorMessage = "La note Dois être entre 0 et 5.")]
        [RegularExpression("([0-5])", ErrorMessage = "La note dois etre un chiffre compris entre 0 et 5")]
        [Display(Name = "Note")]
        public int Rating { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "le commentaire ne dois pas etre vide")]
        [MaxLength(250, ErrorMessage = "le message dois faire moin de 250 caracteres")]
        [Display(Name = "Laissez votre commentaire")]
        public string Comment { get; set; }

        [RegularExpression("([0-9]+)", ErrorMessage = "la réservation choisi n'existe pas.")]
        public int BookingId { get; set; }
    }
}
