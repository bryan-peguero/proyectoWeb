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
    public class SearchControllerTest
    {
        [TestMethod]
        public async Task IndexAsync()
        {
            var controller = new SearchController();
            var result = await controller.Index() as ViewResult;
            var model = (List<Listing>)result.Model;
            Assert.IsNotNull(result);
            Assert.IsNotNull(model);
            Assert.IsTrue(model.Count > 0);
        }
    }
}
