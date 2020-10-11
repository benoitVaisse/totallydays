using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using Totallydays.Models;
using Totallydays.Repositories;
using Totallydays.ViewsModel;
using Totallydays.Services;

namespace Totallydays.Controllers.MiddleController
{
    [Route("my-account")]
    [Authorize]
    public class MiddleAccountController : MiddleController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly UserRepository _userRepository;
        private readonly UploadService _uploadService;

        public MiddleAccountController(
            UserManager<AppUser> userManager,
            UserRepository userRepo,
            UploadService uploadService
            )
        {
            this._userManager = userManager;
            this._userRepository = userRepo;
            this._uploadService = uploadService;
        }

        [HttpGet("", Name ="my_account")]
        public async Task<IActionResult> MyAccount()
        {
            this.setFlash();
            AppUser User = await this._userManager.GetUserAsync(this.User);
            this.ViewBag.average = this._userRepository.GetAverageAllHosting(User);
            return View(User);
        }

        [HttpGet("edit", Name = "my_account_edit")]
        public async Task<IActionResult> MyAccountEdit()
        {
            AppUser User = await this._userManager.GetUserAsync(this.User);
            FormUserAccountViewModel UserForm = new FormUserAccountViewModel()
            {
                firstname = User.Firstname,
                lastname = User.Lastname,
                phone = User.PhoneNumber,
                description = User.Description
            };
            this.ViewBag.user = User;
            return View(UserForm);
        }

        [HttpPost("edit", Name = "my_account_edit_post")]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> MyAccountEdit(FormUserAccountViewModel model)
        {
            AppUser User = await this._userManager.GetUserAsync(this.User);
            if (ModelState.IsValid)
            {
                User.Firstname = model.firstname;
                User.Lastname = model.lastname;
                User.PhoneNumber = model.phone;
                User.Description = model.description;

                var result = await this._userManager.UpdateAsync(User);
                List<string> message = new List<string>();
                message.Add("Votre profil a bien été mise a jour");
                TempData["success"] = message ;
                return RedirectToRoute("my_account");
            }
            this.ViewBag.user = User;
            return View(model);
        }


        /// <summary>
        /// save avatar from user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("change-picture", Name = "my_account_change_picture")]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> ChangePIcture(FormUserPictureModelView model)
        {
            AppUser User = await this._userManager.GetUserAsync(this.User);
            if (ModelState.IsValid)
            {
                User = this._uploadService.UploadImagePicture(User, model, "img/avatar/"+User.Id);
                var result = await this._userManager.UpdateAsync(User);
                string view = await ControllerExtenstionService.RenderViewToStringAsync(this, "~/Views/MiddleAccount/_partial/_account_picture.cshtml", User);
                return Json(new {status="success", message = "Votre photo de profile a bien été modifiée", view  });

            }
            
            return Json(new { status="error", message= "send your picture in format .jpg or .png"});
        }

    }
}