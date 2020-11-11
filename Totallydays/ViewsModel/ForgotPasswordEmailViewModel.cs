using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Totallydays.Models;

namespace Totallydays.ViewsModel
{
    public class ForgotPasswordEmailViewModel
    {
        public AppUser User { get; set; }
        public string Url { get; set; }
    }
}
