using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Totallydays.Models
{
    public class Image
    {
        [Key]
        public int Image_id { get; set; }

        [Required]
        public virtual Hosting Hosting { get; set; }

        [Required]
        public string File { get; set; }

        

    }
}
