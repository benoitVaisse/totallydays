using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Totallydays.Utilities;

namespace Totallydays.ViewsModel
{
    public class FormSearchHostingViewModel
    {

        [Required(AllowEmptyStrings = false)]
        [RegularExpression("(^[a-zA-Z,éèà ]+$)")]
        public string City { get; set; }

        [CompareDate("End_Date", "<", ErrorMessage = "La date de début de séjour doit être inferieur à celle de fin")]
        [DateLessGreaterThan(compare: ">", ErrorMessage = "La Date de début de séjour dois etre superieur à celle de aujourdhui")]
        public DateTime Start_Date { get; set; }

        [CompareDate("Start_Date", ">", ErrorMessage = "La date de fin de séjour doit être supéreur à celle de début")]
        [DateLessGreaterThan(compare: ">", ErrorMessage = "La Date de fin de séjour dois etre superieur à celle de aujourdhui")]
        public DateTime End_Date { get; set; }

        [RegularExpression("([0-9]+)", ErrorMessage = "le nombre de personne n'est pas correct")]
        public int Number_personn { get; set; }
    }
}
