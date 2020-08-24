using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Totallydays.Models
{
    public class Comment
    {
        [Key]
        public int Comment_id { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public int Rating { get; set; }

        [Required]
        public DateTime Created_at { get; set; }

        [Required]
        public virtual AppUser User_emmiter { get; set; }

        public virtual AppUser User_receiver { get; set; }

        public virtual Hosting Hosting_id { get; set; }
    }
}
