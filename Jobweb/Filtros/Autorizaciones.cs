using Jobweb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Jobweb.Filtros
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class Autorizaciones:AuthorizeAttribute
    {
        private Usuario usr;
        private string nivelAcceso = "";
        public Autorizaciones(string nivel = "")
        {
            this.nivelAcceso = nivel;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {

            try
            {
                usr = (Usuario)HttpContext.Current.Session["User"];
//<<<<<<< HEAD
                //comprobando que existan usuario en sesion
                if(usr != null)
                {
                    //quitando cualquier diferencia posible como es alguna letra en mayuscula
                    usr.tipo = usr.tipo.ToLower();
                    nivelAcceso = nivelAcceso.ToLower();
                    //comparando si posee acceso

//>>>>>>> Estable-01
                    if (usr.tipo != nivelAcceso && usr.tipo != "administrador")
                    {
                        filterContext.Result = new RedirectResult("~/Home/Error");
                    }
                }
                else
                {
                    filterContext.Result = new RedirectResult("~/Home/Log");
                }
//<<<<<<< HEAD
                
            }      
            catch(Exception)
//>>>>>>> Estable-01
            {
                filterContext.Result = new RedirectResult("~/Home/Error");
            }
        }
    }
}