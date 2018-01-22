using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sky.ViewModels;
using Sky.BusinessLy;
using Sky.Common;
using Sky.Model;


namespace Sky.Controllers
{
    public class FunctionController : BaseController
    {
        // GET: Function
        public ActionResult Index(int id)
        {
            ViewBag.FuncOpers = GetOperate(id).Values;
            return View();
        }


        public JsonResult GetData()
        {
            FunctionBL ab = new FunctionBL();
            int page = int.Parse(Request["page"]);
            int rows = int.Parse(Request["rows"]);
            List<FunctionViewModel> lulvm = new List<FunctionViewModel>();
            int total;
            int FuncID = -1;
            string StateFlag = null;
            string FuncName = null;
            try
            {
                if (!string.IsNullOrWhiteSpace(Request["FuncID"]))
                {
                    FuncID = int.Parse(Request["FuncID"]);
                }

            }
            catch (Exception err)
            {
                throw (err);
            }

            if (!string.IsNullOrWhiteSpace(Request["StateFlag"]))
            {
                if (Request["StateFlag"] == "1")
                {
                    StateFlag = "1";
                }
                else
                {
                    StateFlag = "2";
                }
            }


            if (!string.IsNullOrWhiteSpace(Request["FuncName"]))
            {
                FuncName = Request["FuncName"];
            }

            lulvm = ab.GetPaging(page, rows, out total, FuncID, StateFlag, FuncName);
            var data = new
            {
                total,
                rows = lulvm
            };

            return Json(data, JsonRequestBehavior.AllowGet);

        }

        public ActionResult Create()
        {
            MenuBL mbl = new MenuBL();
            ViewBag.Menus = new SelectList(mbl.GetSelectList(x => x.MenuID > 0), "MenuID", "Name");

            RoleBL fbl = new RoleBL();
            ViewBag.Roles = new SelectList(fbl.GetSelectList(x => x.RoldID > 0), "RoldID", "RoleName");

            return View();
        }
        [HttpPost]
        public ActionResult Create(FunctionViewModel auvm)
        {
            AccountViewModel av = GetCurrentAccount();
            if (ModelState.IsValid && auvm != null && av!=null)
            {
                FunctionBL fbl = new FunctionBL();
                string returnValue = string.Empty;

                if (fbl.CheckExist(auvm.FuncName))
                {
                    returnValue = "子系统名已存在！";
                    return Json(Suggestion.InsertFail + returnValue + " " + auvm.FuncName, JsonRequestBehavior.AllowGet);
                }

                auvm.Menus = auvm.Menus.Distinct<int>().ToList<int>();
                auvm.Roles = auvm.Roles.Distinct<int>().ToList<int>();

                UserLogModel _log = new UserLogModel { ClassName = this.GetType().ToString() + ": Create", FuncCode = "1001", ImportanceDegree = "1", OperationType = "N", UserID = av.UserID };

                int rlt = fbl.Add(auvm);
                if (rlt == 0)
                {
                    _log.Remark = "用户:" + av.UserName + Suggestion.InsertSucceed;
                    UserLogBL.RecordUserLog(_log);

                    return Json(Suggestion.InsertSucceed, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    _log.Remark = "用户:" + av.UserName + Suggestion.InsertFail + "原因：" + rlt.ToString();
                    UserLogBL.RecordUserLog(_log);

                    return Json(Suggestion.InsertFail + returnValue, JsonRequestBehavior.AllowGet);
                }

            }
            return Json(Suggestion.InsertFail + "请核对输入的数据的格式:" + ModeStateError.GetModeStateError(ModelState), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit(int id)
        {

            FunctionViewModel uvm = new FunctionBL().GetViewById(id);
            //ViewBag.Genders = SelectListClass.GetGenders(true);

            MenuBL mbl = new MenuBL();
            ViewBag.Menus = new SelectList(mbl.GetSelectList(x => x.MenuID > 0), "MenuID", "Name");

            RoleBL fbl = new RoleBL();
            ViewBag.Roles = new SelectList(fbl.GetSelectList(x => x.RoldID > 0), "RoldID", "RoleName");

            return View(uvm);
        }
        [HttpPost]
        public ActionResult Edit(int id, FunctionViewModel auvm)
        {
            AccountViewModel av = GetCurrentAccount();
            if (auvm != null && ModelState.IsValid && av!=null)
            {
                string returnValue = string.Empty;
                UserLogModel _log = new UserLogModel { ClassName = this.GetType().ToString() + ": Edit", FuncCode = "1001", ImportanceDegree = "1", OperationType = "U", UserID = av.UserID };
                auvm.Menus = auvm.Menus.Distinct<int>().ToList<int>();
                auvm.Roles = auvm.Roles.Distinct<int>().ToList<int>();
                FunctionBL abl = new FunctionBL();

                if (abl.Edit(auvm) == 0)
                {
                    _log.Remark = "用户:" + av.UserName + Suggestion.UpdateSucceed;
                    UserLogBL.RecordUserLog(_log);
                    return Json(Suggestion.UpdateSucceed, JsonRequestBehavior.AllowGet); //提示更新成功 
                }
                else
                {
                    _log.Remark = "用户:" + av.UserName + Suggestion.UpdateFail;
                    UserLogBL.RecordUserLog(_log);
                    return Json(Suggestion.UpdateFail + returnValue, JsonRequestBehavior.AllowGet); //提示更新失败
                }
            }
            return Json(Suggestion.UpdateFail + "请核对输入的数据的格式", JsonRequestBehavior.AllowGet);

        }

        public ActionResult Delete(int id)
        {
            string returnValue = string.Empty;

            AccountViewModel av = GetCurrentAccount();
            if (id > 0 && av!=null)
            {
                UserLogModel _log = new UserLogModel { OperationType = "D", FuncCode = "1001", UserID = Convert.ToInt32(av.UserID), ClassName = this.GetType().ToString() + ": Delete" };
                FunctionBL abl = new FunctionBL();

                if (abl.Delete(id) == 0)
                {
                    _log.Remark = "用户:" + av.UserName + "删除子系统" + id.ToString() + Suggestion.DeleteSucceed;
                    UserLogBL.RecordUserLog(_log);
                    return Json("OK", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    _log.Remark = "用户:" + av.UserName + "删除子系统" + id.ToString() + Suggestion.DeleteFail;
                    UserLogBL.RecordUserLog(_log);
                }
                returnValue = _log.Remark;
            }

            return Json(returnValue, JsonRequestBehavior.AllowGet);
        }
    }
}