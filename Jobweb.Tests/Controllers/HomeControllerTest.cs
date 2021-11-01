using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Jobweb;
using Jobweb.Controllers;
using System.Threading.Tasks;
using Jobweb.Models;
using System.Dynamic;

namespace Jobweb.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public async Task IndexAsync()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            var result = await controller.Index() as ViewResult;
            dynamic model = (ExpandoObject) result.ViewData.Model;
            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(model);
            Assert.IsTrue(Int32.Parse(model.Config.valor) > 0);
        }
        [TestMethod]
        public void Log()
        {
            var controller = new HomeController();
            var result = controller.Log() as ViewResult;
            Assert.IsNotNull(result);
        
        }

        [TestMethod]
        public void Signup()
        {
            var controller = new HomeController();
            var result = controller.Signup() as ViewResult;
            Assert.IsNotNull(result);

        }

        [TestMethod]
        public void Error()
        {
            var controller = new HomeController();
            var result = controller.Error() as ViewResult;
            Assert.IsNotNull(result);

        }
    }
}
