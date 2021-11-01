using Jobweb.Controllers;
using Jobweb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Jobweb.Filtros
{

    public class Filtro:ActionFilterAttribute
    {
        private Usuario usr;
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                base.OnActionExecuting(filterContext);

                usr = (Usuario)HttpContext.Current.Session["User"];
                if (usr == null)
                {

                    if (filterContext.Controller is HomeController == false && filterContext.Controller is SearchController == false && filterContext.Controller is JobController == false)
                    {
                        filterContext.HttpContext.Response.Redirect("/Home/Log");
                    }
                }

            }
            catch (Exception)
            {
                filterContext.Result = new RedirectResult("~/Home/Index");
            }

        }
    }
}