using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;
using Totallydays.Controllers;
using Totallydays.Data;
using Totallydays.Models;
using Totallydays.Repositories;

namespace TestTotallydaysWebUi
{
    [TestClass]
    public class HomeControllerUnitTest : TestVariable
    {

 

        [TestMethod]
        public async Task TestIndexIsOk()
        {
            this.getVariable();
            var Ilogger = (new Mock<ILogger<HomeController>>(MockBehavior.Strict)).Object;

            HomeController HomeController = new HomeController(Ilogger, this._userRepo.Object);

            var result = HomeController.Index();

            Assert.IsInstanceOfType(result, typeof(ViewResult));

            ViewResult view = result as ViewResult;


            Assert.IsNotNull(view.ViewData["title"]);

            Assert.IsTrue(view.ViewData["title"].ToString() == "Bienvenue");
        }

        [TestMethod]
        public async Task Privacy__ReturnsAViewResult()
        {
            this.getVariable();
            var Ilogger = (new Mock<ILogger<HomeController>>(MockBehavior.Strict)).Object;

            HomeController HomeController = new HomeController(Ilogger, this._userRepo.Object);

            var result = HomeController.Privacy();

            Assert.IsInstanceOfType(result, typeof(ViewResult));

        }
    }
}
