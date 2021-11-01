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
    public class PosterControllerTest
    {
        [TestMethod]
        public async Task PostAJobAsync()
        {
            var controller = new PosterController();
            var result = await controller.PostAJob() as ViewResult;
            var model = (List<Categoria>)result.ViewBag.categorias;
            Assert.IsNotNull(result);
            Assert.IsNotNull(model);
            Assert.IsTrue(model.Count > 0);
        }
    }
}
