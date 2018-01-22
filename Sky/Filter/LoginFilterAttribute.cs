using Sky.Common;
using System.Web.Mvc;
using Sky.ViewModels;

namespace Sky.Filter
{
    /// <summary>
    /// 登录验证过滤器
    /// </summary>
    public class LoginFilterAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// 当Action中标注了[LoginFilter]的时候会执行
        /// </summary>
        /// <param name="filterContext">请求上下文</param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //if (filterContext.HttpContext.Session["account"] == null)
            //{
            //    filterContext.HttpContext.Response.Write(" <script type='text/javascript'> window.top.location='Login'; </script>");
            //    filterContext.Result = new EmptyResult();
            //    return;
            //}
            base.OnActionExecuting(filterContext);
            bool result = false;
            if (filterContext.HttpContext.Request.Cookies["sessionId"] != null)
            {
                string sessionId = filterContext.HttpContext.Request.Cookies["sessionId"].Value;//接收从Cookie中传递过来的Memcache的key
                object obj = MemcacheHelper.Get(sessionId);//根据key从Memcache中获取用户的信息
                if (obj != null)
                {
                    var account = SerializerHelper.DeserializeToObject<AccountViewModel>(obj.ToString());
                    if (account == null)
                    {
                        result = true;
                    }
                }
                else
                {
                    result = true;
                }
            }
            else
            {
                result = true;
            }
            if(result)
            {
                //filterContext.HttpContext.Response.Write(" <script type='text/javascript'> window.top.location='Login'; </script>");
                filterContext.HttpContext.Response.Redirect("/Login");
                filterContext.Result = new EmptyResult();
                return;
            }
        }
    }
}