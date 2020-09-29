using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Totallydays.Utilities;

namespace Totallydays.ViewsModel
{
    public class FormUserPictureModelView
    {

        [FilesExtensionsCustom(".jpg,.png", ErrorMessage = "please select ")]
        public IFormFile picture { get; set; }
    }
}
