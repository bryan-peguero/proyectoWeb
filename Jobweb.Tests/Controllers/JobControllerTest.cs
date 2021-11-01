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
    public class JobControllerTest
    {
        [TestMethod]
        public async Task IndexAsync()
        {
            var controller = new JobController();
            var result = await controller.Index(1) as ViewResult;
            var model = (Listing)result.Model;
            Assert.IsNotNull(result);
            Assert.IsNotNull(model);            
        }
    }
}
