using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Totallydays.ViewsModel
{
    public class FormResetPasswordToken
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required(AllowEmptyStrings =false)]
        [Display(Name ="Mot de passe")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(AllowEmptyStrings =false)]
        [DataType(DataType.Password)]
        [Display(Name ="Confirmé le mot de passe")]
        [Compare("Password", ErrorMessage = "Les Mot de passe doivent etre identique")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string Token { get; set; }
    }
}
