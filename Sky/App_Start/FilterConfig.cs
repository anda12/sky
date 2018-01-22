using System.Web;
using System.Web.Mvc;
using Sky.Filter;

namespace Sky
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            //filters.Add(new MyExceptionAttribute());
            //filters.Add(new MyAuthorizeAttribute());
        }
    }
}
