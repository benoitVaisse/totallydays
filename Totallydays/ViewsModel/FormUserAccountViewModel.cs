using Castle.Components.DictionaryAdapter;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Totallydays.ViewsModel
{
    public class FormUserAccountViewModel
    {

        [Required(AllowEmptyStrings =false, ErrorMessage ="firstname must not be null or empty")]
        public string firstname { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Lastname must not be null or empty")]
        public string lastname { get; set; }

        [Phone]
        public string phone { get; set; }
        
        [StringLength(255)]
        [Display(Name ="Description (max 255 lettres)", Prompt = "Mettez une petite description de vous, pour vous mettre en valeur")]
        public string description { get; set; }

    }
}
