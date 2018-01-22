using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sky.ViewModels;
using Sky.Common;
using Newtonsoft.Json;
using Sky.Filter;

namespace Sky.Controllers
{
    [MyAuthorize]
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            AccountViewModel account = GetCurrentAccount();
            if (account == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                return PartialView();
            }
        }

        public PartialViewResult Bottom()
        {

            return PartialView("Bottom", GetSS());
        }

        public PartialViewResult TopShow()
        {
            AccountViewModel account = GetCurrentAccount();
            if (account == null)
            {
                return PartialView("TopShow", null);
            }
            else
            {
                return PartialView("TopShow", account);
            }
            
        }

        public PartialViewResult UserMenu()
        {
            AccountViewModel account = GetCurrentAccount();
            if (account == null)
            {
                return PartialView("UserMenu", null);
            }
            else
            {
                //Console.Out.WriteLine(ViewBag.menu);
                return PartialView("UserMenu", account.um);
            }
        }

        public JsonResult GetOperMenu()
        {
            AccountViewModel account = GetCurrentAccount();
            if (account == null)
            {
                return null;
            }
            else
            {
                //Console.Out.WriteLine(ViewBag.menu);
                return Json("success");
            }
        }

        public ActionResult Error()
        {
            return PartialView();
        }
    }
}