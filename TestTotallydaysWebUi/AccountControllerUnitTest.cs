using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Totallydays.Controllers;
using Totallydays.Models;
using Totallydays.Services;
using Totallydays.ViewsModel;

namespace TestTotallydaysWebUi
{
    [TestClass]
    public class AccountControllerUnitTest : TestVariable
    {

        /// <summary>
        /// test la fonction Resgiter en method GET
        /// si on a bien une instance de type Vieuw result avec un model de type FormRegisterViewModel
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task Register_ReturnsAViewResult()
        {
            this.getVariable();

            AccountController controller = new AccountController(
                this._userManagerMock.Object, 
                this._signInManagerMock.Object, 
                this._IWebHostEnvironment,  
                this._mailService.Object, 
                this._recaptchaService.Object);

            var result = await controller.Register();

            Assert.IsInstanceOfType(result, typeof(ViewResult));

            var View = result as ViewResult;

            Assert.IsInstanceOfType(View.ViewData.Model, typeof(FormRegisterViewModel));
        }

        /// <summary>
        /// test la fonction register de AccountController method POST
        /// verifie si on a bien une redirection si la création de compte ce passe bien
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task Register_ReturnsARedirection_isModelStateIsValid()
        {
            this.getVariable();

            FormRegisterViewModel model = new FormRegisterViewModel()
            {
                Firstname= "benoit",  Lastname = "vaisse", Email="benoit.toto@toto.fr", 
                Password="password",  ConfirmPassword="password",  TokenCaptcha="fdsfsdfsdfdsfdsf"
            };

            AppUser User = new AppUser()
            {
                UserName = model.Email,  Email = model.Email,
                Firstname = model.Firstname, Lastname = model.Lastname
            };

            _userManagerMock
                .Setup(x => x.CreateAsync(It.IsAny<AppUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            this._recaptchaService.
                Setup(s => s.VerifyResponse(model.TokenCaptcha))
                .Returns(Task.FromResult(true));

            string locationUrl = "http://location/";

            var mockUrlHelper = new Mock<IUrlHelper>();
            mockUrlHelper
                .Setup(x => x.RouteUrl(It.IsAny<UrlRouteContext>()))
                .Returns(locationUrl);

            AccountController controller = new AccountController(this._userManagerMock.Object,  this._signInManagerMock.Object, 
                this._IWebHostEnvironment,  this._mailService.Object, this._recaptchaService.Object);


            var result = await controller.Register(model);

            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

        /// <summary>
        /// test si la vue retournée est bien de type ViewResult
        /// avec un model  de type FormRegisterViewModel
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task Register_ReturnsAViewModel_isModelStateIsInValid()
        {
            // Arrange
            this.getVariable();

            FormRegisterViewModel model = new FormRegisterViewModel()
            {
                Firstname = "",
                Lastname = "vaisse",
                Email = "benoit.toto@toto.fr",
                Password = "password",
                ConfirmPassword = "password",
            };

            this._recaptchaService.
                Setup(s => s.VerifyResponse(model.TokenCaptcha))
                .Returns(Task.FromResult(true));

            AccountController controller = new AccountController(
                this._userManagerMock.Object,
                this._signInManagerMock.Object,
                this._IWebHostEnvironment,
                this._mailService.Object,
                this._recaptchaService.Object);

            controller.ModelState.AddModelError("fakeError", "fakeError");

            // Act
            var result = await controller.Register(model);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var View = result as ViewResult;

            Assert.IsInstanceOfType(View.ViewData.Model, typeof(FormRegisterViewModel));

        }

        /// <summary>
        /// si le recaptcha est invalide, test si on a une vue de type view result
        /// avec yb model de type FormRegisterViewModel
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task Register_ReturnsAViewResult_isRepcatchaInvalid()
        {
            this.getVariable();

            AccountController controller = new AccountController(
                this._userManagerMock.Object, 
                this._signInManagerMock.Object, 
                this._IWebHostEnvironment, 
                this._mailService.Object, 
                this._recaptchaService.Object);

            FormRegisterViewModel model = new FormRegisterViewModel()
            {
                Firstname = "benoit",
                Lastname = "vaisse",
                Email = "benoit.toto@toto.fr",
                Password = "password",
                ConfirmPassword = "password",
                TokenCaptcha = "fdsfsdfsdfdsfdsf"
            };

            this._recaptchaService
                .Setup(s => s.VerifyResponse(model.TokenCaptcha))
                .Returns(Task.FromResult(false));

            var result = await controller.Register(model);

            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var View = result as ViewResult;

            Assert.IsInstanceOfType(View.ViewData.Model, typeof(FormRegisterViewModel));
        }
    }
}
