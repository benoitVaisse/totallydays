using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Totallydays.Models;
using Totallydays.Repositories;
using Totallydays.Services;
using Totallydays.ViewsModel;

namespace Totallydays.Controllers.FrontController
{
    public class HostingController : Controller
    {
        private readonly HostingRepository _hostingRepository;
        private readonly UserManager<AppUser> _usermanager;
        private readonly HostingTypeRepository _hostingTypeRepo;
        private readonly EquipmentRepository _equipementRepository;
        private readonly HostingService _hostingService;
        private readonly ImageRepository _imageRepository;
        private readonly UploadService _uploadService;

        public HostingController(HostingRepository hostrepo,
            UserManager<AppUser> usermanager,
            HostingTypeRepository hostingTypeRepo,
            EquipmentRepository equipementRepository,
            HostingService hostingService,
            ImageRepository imagerepo,
            UploadService uploadServ
         )
        {
            this._hostingRepository = hostrepo;
            this._usermanager = usermanager;
            this._hostingTypeRepo = hostingTypeRepo;
            this._equipementRepository = equipementRepository;
            this._hostingService = hostingService;
            this._imageRepository = imagerepo;
            this._uploadService = uploadServ;
        }


        /// <summary>
        /// fonction qui liste mes hébergements
        /// </summary>
        /// <returns></returns>
        [HttpGet("mes-hebergements", Name = "my_hostings")]
        [Authorize]
        public async Task<IActionResult> MyHosting()
        {
            AppUser User = await this._usermanager.GetUserAsync(this.User);
            IEnumerable<Hosting> Hostings = await this._hostingRepository.FindByUser(User);
            return View(Hostings);
        }


        /// <summary>
        /// fonction qui med'afficher la page de creation ou modification d'un hebergements
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("creer-un-hebergement", Name = "hosting_create")]
        [HttpGet("modifier-un-hebergement/{id:int}", Name = "hosting_modified")]
        [Authorize]
        public async Task<IActionResult> CreateHosting(int id = 0)
        {
            AppUser User = await this._usermanager.GetUserAsync(this.User);
            Hosting Hosting;
            if (id != 0)
            {
                // je regarde si j'annonce existe et si elle appartient bien a l'utilisateur
                Hosting = this._hostingRepository.Find(id);
                if (Hosting == null || Hosting.User != User)
                {
                    return BadRequest();
                }
            }
            else
            {
                Hosting = new Hosting();
            }

            this.ViewBag.hosting_type = await this._hostingTypeRepo.FindAll();
            FormHostingViewModel model = this._hostingService.HostingToFormModel(Hosting);
            return View(model);
        }


        /// <summary>
        /// creer ou met a jour un hebergement
        /// </summary>
        /// <param name="Model"></param>
        /// <returns></returns>
        [HttpPost("creer-un-hebergement-submit", Name = "hosting_create_post")]
        [Authorize]
        public async Task<IActionResult> CreateHosting(FormHostingViewModel Model)
        {
            Hosting Hosting;
            AppUser User = await this._usermanager.GetUserAsync(this.User);

            if (ModelState.IsValid)
            {
                string Slug = this._hostingService.Replace(Model.Title);
                if (Model.Hosting_id != 0)
                {
                    Hosting = this._hostingRepository.Find(Model.Hosting_id);
                    if (Hosting == null || Hosting.User != User)
                    {
                        return BadRequest();
                    }
                    Hosting = this._hostingService.FormModelToHosting(Hosting, Model, User);
                    Hosting = this._hostingService.SetSlug(Hosting, Slug);
                    this._hostingRepository.Update(Hosting);

                }
                else
                {
                    Hosting = new Hosting();
                    Hosting = this._hostingService.FormModelToHosting(Hosting, Model, User);

                    Hosting = this._hostingService.SetSlug(Hosting, Slug);
                    this._hostingRepository.Create(Hosting);
                }

                return RedirectToRoute("hosting_images", new { id = Hosting.Hosting_id });

            }


            this.ViewBag.hosting_type = await this._hostingTypeRepo.FindAll();
            return View(Model);

        }

        /// <summary>
        /// affiche a page pour ajouter des image et des equipements a une annonce
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("modifier-un-hebergement/images/{id:int}", Name = "hosting_images")]
        [Authorize]
        public async Task<IActionResult> InsertImage(int id)
        {
            AppUser User = await this._usermanager.GetUserAsync(this.User);
            Hosting Hosting = this._hostingRepository.Find(id);
            if (Hosting.User != User)
            {
                return BadRequest();
            }

            this.ViewBag.equipments = this._equipementRepository.FindAll();
            this.ViewBag.images = await this._imageRepository.FindImageToHosting(Hosting);
            this.ViewBag.hosting = Hosting;
            List<int> listeE = new List<int>();
            foreach (Hosting_Equipment he in Hosting.Hosting_Equipment)
            {
                listeE.Add(he.EquipmentEquipment_id);
            }

            this.ViewBag.listeE = listeE;
            FormImageViewModel model = new FormImageViewModel();

            return View(model);
        }


        /// <summary>
        /// add image to hosting
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("modifier-un-hebergement/images-submit/{id:int}", Name = "hosting_images_post")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> InsertImage(int id, FormImageViewModel model)
        {
            Hosting Hosting = this._hostingRepository.Find(id);

            Image Image = new Image()
            {
                Hosting = Hosting
            };
            Image = this._uploadService.UploadImageHosting(Image, model, "img/hosting/img/" + Hosting.Hosting_id);
            this._imageRepository.Create(Image);
            IEnumerable<Image> Images = await this._imageRepository.FindImageToHosting(Hosting);
            this._hostingService.setModified(Hosting, true);
            return Json(new { view = await ControllerExtenstionService.RenderViewToStringAsync(this, "~/Views/Hosting/_partial/_liste_hosting_image.cshtml", Images) });
        }


        /// <summary>
        /// delete image to hosting
        /// </summary>
        /// <param name="id"></param>
        /// <param name="idImage"></param>
        /// <returns></returns>
        [HttpPost("modifier-un-hebergement/image-delete/{id:int}", Name = "hosting_images_delete_post")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> deleteImage(int id, int idImage)
        {
            AppUser User = await this._usermanager.GetUserAsync(this.User);
            Image image = await this._imageRepository.Find(idImage);
            Hosting Hosting = this._hostingRepository.Find(id);
            if (image.Hosting != Hosting || Hosting.User != User)
            {
                return BadRequest();
            }

            image = this._imageRepository.Delete(image);
            this._hostingService.setModified(Hosting, true);
            IEnumerable<Image> Images = await this._imageRepository.FindImageToHosting(Hosting);
            return Json(new { view = await ControllerExtenstionService.RenderViewToStringAsync(this, "~/Views/Hosting/_partial/_liste_hosting_image.cshtml", Images) });
        }

        /// <summary>
        /// link equipment to hosting
        /// </summary>
        /// <param name="id"></param>
        /// <param name="equipments"></param>
        /// <returns></returns>
        [HttpPost("modifier-un-hebergement/equipment/{id:int}", Name = "hosting_equipment_post")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LinkHostingEquipment(int id, string[] equipments)
        {
            AppUser User = await this._usermanager.GetUserAsync(this.User);
            Hosting Hosting = this._hostingRepository.Find(id);
            if (Hosting.User != User)
            {
                return BadRequest();
            }

            int idE;
            this._hostingRepository.DeleteHostingEquipmentByHosting(Hosting);
            foreach (string equipment_id in equipments)
            {
                if (int.TryParse(equipment_id, out idE))
                {
                    Equipment e = this._equipementRepository.FindOneById(idE);
                    if (e != null)
                    {
                        Hosting_Equipment HE = new Hosting_Equipment()
                        {
                            HostingHosting_id = Hosting.Hosting_id,
                            EquipmentEquipment_id = e.Equipment_id
                        };

                        this._hostingRepository.CreateHostingEquipment(HE);
                    }
                }
            }
            this._hostingService.setModified(Hosting, true);
            return Json(new { });
        }


        /// <summary>
        /// To see a hosting
        /// </summary>
        /// <param name="slug"></param>
        /// <returns></returns>
        [HttpGet("hebergement/{slug}", Name ="hosting_view")]
        public IActionResult SeeHosting(string slug)
        {
            Hosting Hosting = this._hostingRepository.FindBySlug(slug);
            if (Hosting == null)
                return BadRequest();

            return View(Hosting);
        }
    }
}