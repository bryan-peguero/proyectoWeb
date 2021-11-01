using System.Web;
using System.Web.Mvc;

namespace Jobweb
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new Filtros.Filtro());
        }
    }
}
