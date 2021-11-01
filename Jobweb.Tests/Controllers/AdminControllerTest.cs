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

namespace Jobweb.Tests.Controllers
{
    [TestClass]
    public class AdminControllerTest
    {
        [TestMethod]
        public void Dashboard()
        {
            var controller = new AdminController();
            var result = controller.Dashboard() as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task UsersAsync()
        {
            var controller = new AdminController();
            var result = await controller.Users() as ViewResult;
            var model = (List<Usuario>)result.Model;
            Assert.IsNotNull(result);
            Assert.IsTrue(model.Count > 0);
        }
        [TestMethod]
        public async Task EditUsersAsync()
        {
            var controller = new AdminController();
            var result = await controller.EditUser(1) as ViewResult;
            var model = (Usuario)result.Model;
            Assert.IsNotNull(result);
            Assert.IsTrue(model.id > 0);
        }
        [TestMethod]
        public async Task CategoriesAsync()
        {
            var controller = new AdminController();
            var result = await controller.Categories() as ViewResult;
            var model = (List<Categoria>)result.Model;
            Assert.IsNotNull(result);
            Assert.IsTrue(model.Count > 0);
        }
        [TestMethod]
        public async Task EditCategoriesAsync()
        {
            var controller = new AdminController();
            var result = await controller.EditCategories(1) as ViewResult;
            var model = (Categoria)result.Model;
            Assert.IsNotNull(result);
            Assert.IsTrue(model.id > 0);
            Assert.IsTrue(model.categoria != null);
            
        }
        [TestMethod]
        public async Task ListingAsync()
        {
            var controller = new AdminController();
            var result = await controller.Listings() as ViewResult;
            var model = (List<PuestoTrabajo>)result.Model;
            Assert.IsNotNull(result);
            Assert.IsTrue(model.Count > 0);
        }
        [TestMethod]
        public async Task EditListingAsync()
        {
            var controller = new AdminController();
            var result = await controller.EditListing(1) as ViewResult;
            var model = (PuestoTrabajo)result.Model;
            Assert.IsNotNull(result);
            Assert.IsTrue(model.id > 0);
        }
        [TestMethod]
        public async Task SettingsAsync()
        {
            var controller = new AdminController();
            var result = await controller.Settings() as ViewResult;
            var model = (List<Config>)result.Model;
            Assert.IsNotNull(result);
            Assert.IsTrue(model.Count > 0);
        }
        [TestMethod]
        public async Task EditSettingsAsync()
        {
            var controller = new AdminController();
            var result = await controller.EditSettings(6) as ViewResult;
            var model = (Config)result.Model;
            Assert.IsNotNull(result);
            Assert.IsTrue(Int32.Parse(model.valor) > 0);
        }
    }
}
