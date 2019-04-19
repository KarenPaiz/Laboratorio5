using System.Web;
using System.Web.Mvc;

namespace Laboratorio_5_diccionarios
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
