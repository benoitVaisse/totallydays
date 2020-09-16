using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Totallydays.Models;
using Totallydays.ViewsModel;

namespace Totallydays.Services
{
    public class UploadService
    {
        private readonly IWebHostEnvironment _host;

        public UploadService(IWebHostEnvironment host)
        {
            this._host = host;
        }

        /// <summary>
        /// sauvegarde et upload l'image de couverture d'une annonce
        /// </summary>
        /// <param name="Post"></param>
        /// <param name="Model"></param>
        /// <param name="Path"></param>
        /// <returns></returns>
        public Hosting UploadHostingCoverImage(Hosting Post, FormHostingViewModel Model, string Path)
        {
            string UniqueName = null;
            if (Model.Cover_image != null)
            {
                UniqueName = this.SaveFile(Path, Model.Cover_image);
                Post.Cover_image = UniqueName;
            }
            return Post;
        }

        public Image UploadImageHosting(Image Image, FormImageViewModel Model, string Path)
        {
            string UniqueName = null;
            if (Model.Image != null)
            {
                UniqueName = this.SaveFile(Path, Model.Image);
                Image.File = UniqueName;
            }
            return Image;
        }

        private string SaveFile(string PathString, IFormFile File)
        {
            string[] Folder = new string[] { this._host.WebRootPath, PathString };
            string PathFolder = Path.Combine(Folder);
            string UniqueName = Guid.NewGuid().ToString() + "_" + File.FileName;
            string PathFile = Path.Combine(PathFolder, UniqueName);
            if (!Directory.Exists(PathFolder))
            {
                Directory.CreateDirectory(PathFolder);
            }
            File.CopyTo(new FileStream(PathFile, FileMode.Create));

            return UniqueName;
        }
    }
}
