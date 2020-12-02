using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Totallydays.ViewsModel
{
    public class FormRegisterViewModel
    {

        [Required(AllowEmptyStrings =false)]
        [Display(Name = "Prénom")]
        public string Firstname { get; set; }

        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Nom")]
        public string Lastname { get; set; }

        [Required(AllowEmptyStrings = false)]
        [EmailAddress]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        [Display(Name = "Mot de passe")]
        public string Password { get; set; }

        [Required(AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmer le mot de passe")]
        [Compare("Password", ErrorMessage = "Les mots de passe douvent être identique")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string TokenCaptcha { get; set; }

        public IList<AuthenticationScheme> ExternalLogings { get; set; }
    }
}
