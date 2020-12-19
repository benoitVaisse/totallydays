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
        [Required(AllowEmptyStrings = false)]
        public string Title { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Resume { get; set; }

        [Required(AllowEmptyStrings = false)]
        [RegularExpression(@"^[0-9a-zA-Z;,.:!\s-]+")]
        public string Description { get; set; }

        [Required]
        [Range(1, 10000, ErrorMessage = "Le prix doit être compris en {0} et {1}")]
        public float Price { get; set; }

        [FilesExtensionsCustom(".jpg,.png")]
        public IFormFile Cover_image { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Address { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Post_code { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string City { get; set; }
    }
}
