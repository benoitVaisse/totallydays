using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Totallydays.Models;
using Totallydays.Utilities;

namespace Totallydays.ViewsModel
{
    public class FormHostingViewModel
    {
        public int Hosting_id { get; set; }

        [Required]
        public int Hosting_type_id { get; set; }

        [UniqueTitleValue("Hosting_id",ErrorMessage ="aleady exist hosting with this title")]
        [MaxLength(50, ErrorMessage = "Votre Titre ne doit pas dépasser les 50 caractères")]
        [RegularExpression(@"^[0-9a-zA-Z,.:!\s- ]+")]
        [Required(AllowEmptyStrings = false)]
        public string Title { get; set; }

        [MaxLength(100, ErrorMessage = "Votre Résumé ne doit pas dépasser les 100 caractères")]
        [RegularExpression(@"^[0-9a-zA-Z,.:!\s- ]+")]
        [Required(AllowEmptyStrings = false)]
        public string Resume { get; set; }

        [Required(AllowEmptyStrings = false)]
        [RegularExpression(@"^[0-9a-zA-Z,.:!\s- ]+")]
        public string Description { get; set; }

        [Required]
        [Range(1, 10000, ErrorMessage = "Le prix doit être compris en {0} et {1}")]
        public float Price { get; set; }

        [FilesExtensionsCustom(".jpg,.png")]
        public IFormFile Cover_image { get; set; }

        [Required(AllowEmptyStrings = false)]
        [RegularExpression(@"^[0-9a-zA-Zéè- ]+")]
        [MaxLength(100, ErrorMessage = "Votre Adresse ne doit pas dépasser les 100 caractères")]
        public string Address { get; set; }

        [RegularExpression(@"^[0-9]{5}$")]
        [Required(AllowEmptyStrings = false)]
        public string Post_code { get; set; }

        [Required(AllowEmptyStrings = false)]
        [RegularExpression(@"^[0-9a-zA-Zéè- ]+")]
        [MaxLength(150, ErrorMessage = "Votre ville ne doit pas dépasser les 150 caractères")]
        public string City { get; set; }
    }
}
