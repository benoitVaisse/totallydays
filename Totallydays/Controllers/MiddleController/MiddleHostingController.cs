using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using Totallydays.Models;
using Totallydays.Repositories;
using Totallydays.Services;
using Totallydays.ViewsModel;

namespace Totallydays.Controllers.MiddleController
{
    [Route("mon-compte")]
    [Authorize]
    public class MiddleHostingController : MiddleController
    {
        private readonly HostingRepository _hostingRepository;
        private readonly UserManager<AppUser> _usermanager;
        private readonly HostingTypeRepository _hostingTypeRepo;
        private readonly EquipmentRepository _equipementRepository;
        private readonly HostingService _hostingService;
        private readonly ImageRepository _imageRepository;
        private readonly UploadService _uploadService;
        private readonly BookingRepository _bookingRepository;
        private readonly BookingService _bookingService;
        private readonly GoogleMapService _googleMapService;
        private readonly BedRepository _bedRepository;
        private readonly BedRoomService _bedrommService;
        private readonly BedRoomRepository _bedRoomRepository;
        private readonly SendMailService _mailService;
        private readonly ControllerExtenstionServiceRazor _controllerExtenstionServiceRazor;

        public MiddleHostingController(HostingRepository hostrepo,
            UserManager<AppUser> usermanager,
            HostingTypeRepository hostingTypeRepo,
            EquipmentRepository equipementRepository,
            HostingService hostingService,
            ImageRepository imagerepo,
            UploadService uploadServ,
            BookingRepository bookingRepo,
            BookingService bookingServ,
            GoogleMapService googleMapService,
            BedRepository bedRepo,
            BedRoomService bedrommService,
            BedRoomRepository bedRoomRepository,
            SendMailService mailService,
            ControllerExtenstionServiceRazor ControllerExtenstionServiceRazor
            ) : base()
        {
            this._hostingRepository = hostrepo;
            this._usermanager = usermanager;
            this._hostingTypeRepo = hostingTypeRepo;
            this._equipementRepository = equipementRepository;
            this._hostingService = hostingService;
            this._imageRepository = imagerepo;
            this._uploadService = uploadServ;
            this._bookingRepository = bookingRepo;
            this._bookingService = bookingServ;
            this._googleMapService = googleMapService;
            this._bedRepository = bedRepo;
            this._bedrommService = bedrommService;
            this._bedRoomRepository = bedRoomRepository;
            this._mailService = mailService;
            this._controllerExtenstionServiceRazor = ControllerExtenstionServiceRazor;
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
        /// M'affiche la page de création ou modification d'un hébergement
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
        [HttpPost("creer-un-hebergement", Name = "hosting_create_post")]
        [HttpPost("modifier-un-hebergement/{id:int}", Name = "hosting_modified")]
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
                    Hosting.CreatedAt = DateTime.Now;

                    Hosting = this._hostingService.SetSlug(Hosting, Slug);
                    this._hostingRepository.Create(Hosting);
                    await this._mailService.SendEmailNewHosting(Hosting, this);

                }

                Hosting = this._googleMapService.setLngLgt(Hosting);
                return RedirectToRoute("hosting_images", new { id = Hosting.Hosting_id });

            }


            this.ViewBag.hosting_type = await this._hostingTypeRepo.FindAll();
            return View(Model);

        }

        /// <summary>
        /// affiche a page pour ajouter des images, chambres et des equipements a une annonce
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
            this.ViewBag.listBed = await this._bedRepository.FindAll();
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
            AppUser User = await this._usermanager.GetUserAsync(this.User);
            Hosting Hosting = this._hostingRepository.Find(id);

            if(Hosting.User != User || Hosting == null)
            {
                this._errorMessage.Add("Erreur lors de l'ajout de l'image");
                this._ajaxFlashessage.Add("error", this._errorMessage);
                return Json(new { status = "error", message = this._ajaxFlashessage });
            }

            if (!ModelState.IsValid)
            {
                this.SetErroMessageAjax();
                this._ajaxFlashessage.Add("error", this._errorMessage);
                return Json(new { status = "error", message = this._ajaxFlashessage });
            }

            Image Image = new Image()
            {
                Hosting = Hosting
            };
            Image = this._uploadService.UploadImageHosting(Image, model, "img/hosting/img/" + Hosting.Hosting_id);
            this._imageRepository.Create(Image);
            IEnumerable<Image> Images = await this._imageRepository.FindImageToHosting(Hosting);
            this._hostingService.setModified(Hosting, true);
            this._successMessage.Add("l'image a bien été ajoutée");
            this._ajaxFlashessage.Add("success", this._successMessage);
            return Json(new { view = await this._controllerExtenstionServiceRazor.RenderViewToStringAsync("~/Views/Hosting/_partial/_liste_hosting_image.cshtml", Images), status="success", message= this._ajaxFlashessage });
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
                this._errorMessage.Add("Erreur lors de la supression de l'image");
                this._ajaxFlashessage.Add("error", this._errorMessage);
                return Json(new { status = "error", message = this._ajaxFlashessage });
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
            if (Hosting.User != User || Hosting == null)
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
                        this._successMessage.Add($"L'equipment {e.Name} a bien été ajouté.");
                    }
                    else
                    {
                        this._errorMessage.Add($"L'equipment {equipment_id} n'a pas été ajouté, une erreur c'est produite lors de l'ajout.");
                    }
                }
                else
                {
                    this._errorMessage.Add($"L'equipment {equipment_id} n'a pas été ajouté, une erreur c'est produite lors de l'ajout.");
                }
            }
            this._hostingService.setModified(Hosting, true);
            this._ajaxFlashessage.Add("error", this._errorMessage);
            this._ajaxFlashessage.Add("success", this._successMessage);
            return Json(new { status ="success", message = this._ajaxFlashessage });
        }


        /// <summary>
        /// permet de publié ou pas un hébergement coté utilisateur
        /// </summary>
        /// <param name="id"></param>
        /// <param name="publish"></param>
        /// <returns></returns>
        [HttpPost("modifier-un-hebergement/publish", Name ="hosting_publish_post")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> PublishHosting(int id, bool publish)
        {
            AppUser User = await this._usermanager.GetUserAsync(this.User);
            Hosting Hosting = this._hostingRepository.Find(id);
            if(Hosting == null || Hosting.User != User)
            {
                this._errorMessage.Add("Erreur Lors de la modification de l'annonce");
                this._ajaxFlashessage.Add("error", this._errorMessage);
                return Json(new { status = "error", Message = this._ajaxFlashessage });
            }

            Hosting = this._hostingRepository.setPublish(Hosting, publish);
            this._successMessage.Add("le status de publication a bien été changé");
            this._ajaxFlashessage.Add("success", this._successMessage);
            return Json(new { status="success", Message = this._ajaxFlashessage});
        }

        [HttpPost("modifier-un-hebergement/bedroom/{id:int}", Name = "hosting_bed_post")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SetBedroom(int id, IEnumerable<Bedroom> bedrooms)
        {
            AppUser User = await this._usermanager.GetUserAsync(this.User);
            Hosting Hosting = this._hostingRepository.Find(id);

            if(Hosting == null || Hosting.User != User)
            {
                this._errorMessage.Add("Erreur lors de l'ajoue des chambres.");
                return Json(new { status ="error", message="Un problème est survenu lors de l'ajoue des chambre"});
            }
            // supression de tt les anciens lit
            this._bedrommService.RemoveBed(Hosting.Bedrooms);

            bedrooms =  this._bedrommService.CreateBedRoom(bedrooms, Hosting, ref this._errorMessage,  ref this._successMessage);

            this._ajaxFlashessage.Add("error", this._errorMessage);
            this._ajaxFlashessage.Add("success", this._successMessage);

            return Json(new { bedrooms, status="success" , message = this._ajaxFlashessage});
        }

    }
}