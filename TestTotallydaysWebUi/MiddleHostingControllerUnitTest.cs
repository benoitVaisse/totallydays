using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Security.Principal;
using System.Threading.Tasks;
using Totallydays.Controllers;
using Totallydays.Controllers.MiddleController;
using Totallydays.Data;
using Totallydays.Models;
using Totallydays.Repositories;


namespace TestTotallydaysWebUi
{
    [TestClass]
    public class MiddleHostingControllerUnitTest : TestVariable
    {

        [TestMethod]
        public async Task MyHosting_ReturnsAViewResult()
        {
            this.getVariable();

            var HomeController = new MiddleHostingController(this._hostingRepository.Object, _userManagerMock.Object, _hostingTypeRepo.Object, _equipementRepository.Object, _hostingService.Object, _imageRepository.Object, _uploadService.Object, _bookingRepository.Object, _bookingService.Object, _googleMapService.Object, _bedRepository.Object, _bedrommService.Object, _bedRoomRepository.Object, _mailService.Object, _controllerExtenstionServiceRazor.Object);

            var result = await HomeController.MyHosting();

            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
    }
}
