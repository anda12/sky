using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sky.ViewModels;
using Sky.BusinessLy;

namespace Sky.Controllers
{
    public class UserLogController : BaseController
    {

        // GET: UserLog
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult GetData()
        {
            List<UserLogViewModel> lulvm = new List<UserLogViewModel>();
            int page= int.Parse(Request["page"]);
            int rows = int.Parse(Request["rows"]);
            string syscode;
            int UserID=-1;
            DateTime st;
            DateTime et;
            string oper; 
            if (Request["OperationType"] == "all")
            {
                oper = null;
            }
            else
            {
                oper = Request["OperationType"];
            }

            if(string.IsNullOrWhiteSpace(Request["SystemCode"]))
            {
                syscode = null;
            }
            else
            {
                syscode = Request["SystemCode"];
            }

            try
            {
                if (!string.IsNullOrWhiteSpace(Request["UserID"]))
                {
                     UserID = int.Parse(Request["UserID"]);
                }
                if (!string.IsNullOrWhiteSpace(Request["StartTime"]))
                {
                     st = DateTime.Parse(Request["StartTime"]);
                }
                else
                {
                    st = DateTime.Now.AddYears(-10);
                }
                if (!string.IsNullOrWhiteSpace(Request["EndTime"]))
                {
                     et = DateTime.Parse(Request["EndTime"]);
                }
                else
                {
                    et = DateTime.Now;
                }
            }
            catch (Exception err)
            {
                throw err;
            }
            int start = page*rows;
            UserLogBL ubl = new UserLogBL();

            
            int total;

            lulvm=ubl.GetUserLogPaging(page, rows, out total, st, et, UserID, oper, syscode);
            var data = new
            {
                total,
                rows = lulvm
            };
            return Json(data, JsonRequestBehavior.AllowGet);
        }


    }
}