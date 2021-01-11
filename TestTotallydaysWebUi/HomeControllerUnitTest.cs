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

 
        /// <summary>
        /// test function Index of homeController
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public void TestIndexIsOk()
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


        /// <summary>
        /// test function Privacy of homeController
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public void Privacy__ReturnsAViewResult()
        {
            this.getVariable();
            var Ilogger = (new Mock<ILogger<HomeController>>(MockBehavior.Strict)).Object;

            HomeController HomeController = new HomeController(Ilogger, this._userRepo.Object);

            var result = HomeController.Privacy();

            Assert.IsInstanceOfType(result, typeof(ViewResult));

        }
    }
}
