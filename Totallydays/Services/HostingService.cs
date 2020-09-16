using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Totallydays.Models;
using Totallydays.ViewsModel;
using Totallydays.Repositories;
using System.Text.RegularExpressions;

namespace Totallydays.Services
{
    public class HostingService
    {
        private readonly HostingRepository _hostingRepository;
        private readonly HostingTypeRepository _hostingTypeRepository;
        private readonly UploadService _uploadService;

        public HostingService(HostingRepository hostingRepo, HostingTypeRepository hostingTypeRepo, UploadService upServ)
        {
            this._hostingRepository = hostingRepo;
            this._hostingTypeRepository = hostingTypeRepo;
            this._uploadService = upServ;
        }

        public FormHostingViewModel HostingToFormModel(Hosting hosting)
        {
            FormHostingViewModel model = new FormHostingViewModel()
            {
                Hosting_id = hosting.Hosting_id,
                Hosting_type_id = hosting.Hosting_type!= null ? hosting.Hosting_type.Hosting_type_id:0,
                Title = hosting.Title,
                Resume = hosting.Resume,
                Description = hosting.Description,
                Price = hosting.Price,
                Address = hosting.Address,
                Post_code = hosting.Post_code,
                City = hosting.City
            };
            return model;
        }

        public Hosting FormModelToHosting(Hosting Hosting, FormHostingViewModel model, AppUser User)
        {
            Hosting.Title = model.Title;
            Hosting.Resume = model.Resume;
            Hosting.Description = model.Description;
            Hosting.Price = model.Price;
            Hosting.Address = model.Address;
            Hosting.Post_code = model.Post_code;
            Hosting.City = model.City;

            Hosting = this._uploadService.UploadHostingCoverImage(Hosting, model, "img/hosting/coverImage");
            Hosting.Hosting_type = this._hostingTypeRepository.Find(model.Hosting_type_id);
            Hosting.User = User;

            return Hosting;
        }

        /// <summary>
        /// set modified in hosting
        /// </summary>
        /// <param name="Hosting"></param>
        /// <param name="modified"></param>
        /// <returns></returns>
        public Hosting setModified(Hosting Hosting, bool modified)
        {
            Hosting.Modified = modified;
            this._hostingRepository.Update(Hosting);

            return Hosting;
        }

        /// <summary>
        /// set slug in hosting
        /// </summary>
        /// <param name="Hosting"></param>
        /// <param name="Slug"></param>
        /// <returns></returns>
        public Hosting SetSlug(Hosting Hosting, string Slug)
        {
            if(Hosting.Slug == null)
            {
                Hosting.Slug = Slug;
            }

            return Hosting;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="String"></param>
        /// <returns></returns>
        public string Replace(string String)
        {

            string newString = String.Trim();
            newString = newString.ToLower();
            newString = newString.Replace(' ', '-');

            newString = Regex.Replace(newString, @"[àâä]", "a");
            newString = Regex.Replace(newString, @"[éèëê]", "e");

            return newString;
        }
    }
}
