using System.Web;
using System.Web.Mvc;

namespace MyKitchen_ServerSide1
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
