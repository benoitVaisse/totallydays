using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Totallydays.Utilities;

namespace Totallydays.ViewsModel
{
    public class FormImageViewModel
    {
        [FilesExtensionsCustom(".jpg,.png")]
        public IFormFile Image { get; set; }
    }
}
