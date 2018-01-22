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
    public class RoleController : BaseController
    {
        // GET: Role
        public ActionResult Index(int id)
        {
            ViewBag.RoleOpers = GetOperate(id).Values;
            return View();
        }
        public JsonResult GetData()
        {
            RoleBL ab = new RoleBL();
            int page = int.Parse(Request["page"]);
            int rows = int.Parse(Request["rows"]);
            List<RoleViewModel> lulvm = new List<RoleViewModel>();
            int total;
            int RoldID = -1;
            string StateFlag = null;
            int CreateUser = -1;
            string RoleName = null;
            try
            {
                if (!string.IsNullOrWhiteSpace(Request["RoldID"]))
                {
                    RoldID = int.Parse(Request["RoldID"]);
                }

                if (!string.IsNullOrWhiteSpace(Request["CreateUser"]))
                {
                    CreateUser = int.Parse(Request["CreateUser"]);
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


            if (!string.IsNullOrWhiteSpace(Request["RoleName"]))
            {
                RoleName = Request["RoleName"];
            }

            lulvm = ab.GetPaging(page, rows, out total, RoldID, StateFlag, CreateUser, RoleName);
            var data = new
            {
                total,
                rows = lulvm
            };

            return Json(data, JsonRequestBehavior.AllowGet);

        }

        public ActionResult Create()
        {
            MenuBL mbl =new MenuBL();
            ViewBag.Menus = new SelectList(mbl.GetSelectList(x => x.MenuID > 0), "MenuID", "Name");

            FunctionBL fbl = new FunctionBL();
            ViewBag.Funcs = new SelectList(fbl.GetSelectList(x=>x.FuncID>0), "FuncID", "FuncName");

            OperateBL obl = new OperateBL();
            ViewBag.Opers = new SelectList(obl.GetSelectList(m => m.OperID > 0), "OperID", "OperName");

            AccountBL abl = new AccountBL();
            ViewBag.Users = new SelectList(abl.GetSelectList(m => m.UserID > 0), "UserID", "UserName");

            return View();
        }
        [HttpPost]
        public ActionResult Create(RoleViewModel auvm)
        {
            AccountViewModel av = GetCurrentAccount();
            if (ModelState.IsValid && auvm != null && av!=null)
            {
                RoleBL rbl = new RoleBL();
                string returnValue = string.Empty;

                if (rbl.CheckExist(auvm.RoleName))
                {
                    returnValue = "角色名已存在！";
                    return Json(Suggestion.InsertFail + returnValue, JsonRequestBehavior.AllowGet);
                }

                auvm.Menus = auvm.Menus.Distinct<int>().ToList<int>();
                auvm.Funcs = auvm.Funcs.Distinct<int>().ToList<int>();
                auvm.Opers = auvm.Opers.Distinct<int>().ToList<int>();
                auvm.Users = auvm.Users.Distinct<int>().ToList<int>();

                auvm.CreateUser = av.UserID;
                UserLogModel _log = new UserLogModel { ClassName = this.GetType().ToString() + ": Create", FuncCode = "1001", ImportanceDegree = "1", OperationType = "N", UserID = av.UserID };

                int rlt = rbl.Add(auvm);
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

            RoleViewModel uvm = new RoleBL().GetViewById(id);
            //ViewBag.Genders = SelectListClass.GetGenders(true);

            MenuBL mbl = new MenuBL();
            ViewBag.Menus = new SelectList(mbl.GetSelectList(x => x.MenuID > 0), "MenuID", "Name");

            FunctionBL fbl = new FunctionBL();
            ViewBag.Funcs = new SelectList(fbl.GetSelectList(x => x.FuncID > 0), "FuncID", "FuncName");

            OperateBL obl = new OperateBL();
            ViewBag.Opers = new SelectList(obl.GetSelectList(m => m.OperID > 0), "OperID", "OperName");

            AccountBL abl = new AccountBL();
            ViewBag.Users = new SelectList(abl.GetSelectList(m => m.UserID > 0), "UserID", "UserName");
            return View(uvm);
        }
        [HttpPost]
        public ActionResult Edit(int id, RoleViewModel auvm)
        {
            AccountViewModel av = GetCurrentAccount();
            if (auvm != null && ModelState.IsValid&&av!=null)
            {

                auvm.UpdateUser = av.UserID;
                string returnValue = string.Empty;
                UserLogModel _log = new UserLogModel { ClassName = this.GetType().ToString() + ": Edit", FuncCode = "1001", ImportanceDegree = "1", OperationType = "U", UserID = auvm.UpdateUser };
                auvm.Funcs = auvm.Funcs.Distinct<int>().ToList<int>();
                auvm.Menus = auvm.Menus.Distinct<int>().ToList<int>();
                auvm.Opers = auvm.Opers.Distinct<int>().ToList<int>();
                auvm.Users = auvm.Users.Distinct<int>().ToList<int>();
                RoleBL abl = new RoleBL();

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
                RoleBL abl = new RoleBL();

                if (abl.Delete(id) == 0)
                {
                    _log.Remark = "用户:" + av.UserName + "删除角色" + id.ToString() + Suggestion.DeleteSucceed;
                    UserLogBL.RecordUserLog(_log);
                    return Json("OK", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    _log.Remark = "用户:" + av.UserName + "删除角色" + id.ToString() + Suggestion.DeleteFail;
                    UserLogBL.RecordUserLog(_log);
                }
                returnValue = _log.Remark;
            }

            return Json(returnValue, JsonRequestBehavior.AllowGet);
        }
    }
}