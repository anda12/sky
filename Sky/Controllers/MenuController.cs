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
    public class MenuController : BaseController
    {
        // GET: Menu
        public ActionResult Index(int id)
        {
            ViewBag.MenuOpers = GetOperate(id).Values;
            return View();
        }

        public JsonResult GetData()
        {
            MenuBL ab = new MenuBL();
            int page = int.Parse(Request["page"]);
            int rows = int.Parse(Request["rows"]);
            List<MenuViewModel> lulvm = new List<MenuViewModel>();
            int total;
            int MenuID = -1;
            string StateFlag = null;
            int FuncId = -1;
            int RoleId = -1;
            try
            {
                if (!string.IsNullOrWhiteSpace(Request["MenuID"]))
                {
                    MenuID = int.Parse(Request["MenuID"]);
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
                RoleBL rbl = new RoleBL();
                RoleViewModel rv = rbl.GetViewByName(Request["RoleName"]);
                if(rv!=null)
                {
                    RoleId = rv.RoldID;
                }
            }

            if (!string.IsNullOrWhiteSpace(Request["FuncName"]))
            {
                FunctionBL fbl = new FunctionBL();
                FunctionViewModel fv = fbl.GetViewByName(Request["FuncName"]);
                if(fv!=null)
                {
                    FuncId = fv.FuncID;
                }
            }

            lulvm = ab.GetPaging(page, rows, out total, MenuID, StateFlag, FuncId, RoleId);
            var data = new
            {
                total,
                rows = lulvm
            };

            return Json(data, JsonRequestBehavior.AllowGet);

        }

        public JsonResult GetJson()
        {
            MenuBL mbl = new MenuBL();
            SelectList s = new SelectList(mbl.GetSelectList(m => m.IsLeaf == "0"), "MenuID", "Name");
            return Json(s, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Create()
        {
            FunctionBL fbl = new FunctionBL();
            ViewBag.Funcs = new SelectList(fbl.GetSelectList(x => x.FuncID > 0), "FuncID", "FuncName");

            RoleBL rbl = new RoleBL();
            ViewBag.Roles = new SelectList(rbl.GetSelectList(x => x.RoldID > 0), "RoldID", "RoleName");

            MenuBL mbl = new MenuBL();
            ViewBag.Parents= new SelectList( mbl.GetSelectList(m => m.IsLeaf == "0"), "MenuID", "Name");

            OperateBL obl = new OperateBL();
            ViewBag.Opers = new SelectList(obl.GetSelectList(m => m.OperID > 0), "OperID", "OperName");
            return View();
        }
        [HttpPost]
        public ActionResult Create(MenuViewModel auvm)
        {
            AccountViewModel av = GetCurrentAccount();
            if (ModelState.IsValid && auvm != null && av != null)
            {
                MenuBL mbl = new MenuBL();
                string returnValue = string.Empty;

                if (mbl.CheckExist(auvm.Name))
                {
                    returnValue = "菜单名已存在！";
                    return Json(Suggestion.InsertFail + returnValue + " " + auvm.Name, JsonRequestBehavior.AllowGet);
                }

                auvm.Function = auvm.Function.Distinct<int>().ToList<int>();
                auvm.Roles = auvm.Roles.Distinct<int>().ToList<int>();
                auvm.Opers = auvm.Opers.Distinct<int>().ToList<int>();

                UserLogModel _log = new UserLogModel { ClassName = this.GetType().ToString() + ": Create", FuncCode = "1001", ImportanceDegree = "1", OperationType = "N", UserID = av.UserID };

                int rlt = mbl.Add(auvm);
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

            MenuViewModel uvm = new MenuBL().GetViewById(id);
            //ViewBag.Genders = SelectListClass.GetGenders(true);

            FunctionBL fbl = new FunctionBL();
            ViewBag.Funcs = new SelectList(fbl.GetSelectList(x => x.FuncID > 0), "FuncID", "FuncName");

            RoleBL rbl = new RoleBL();
            ViewBag.Roles = new SelectList(rbl.GetSelectList(x => x.RoldID > 0), "RoldID", "RoleName");

            MenuBL mbl = new MenuBL();
            ViewBag.Parents = new SelectList(mbl.GetSelectList(m => m.IsLeaf == "0"), "MenuID", "Name");

            OperateBL obl = new OperateBL();
            ViewBag.Opers = new SelectList(obl.GetSelectList(m => m.OperID > 0), "OperID", "OperName");
            return View(uvm);
        }
        [HttpPost]
        public ActionResult Edit(int id, MenuViewModel auvm)
        {
            AccountViewModel av = GetCurrentAccount();
            if (auvm != null && ModelState.IsValid && av != null)
            {
                string returnValue = string.Empty;
                UserLogModel _log = new UserLogModel { ClassName = this.GetType().ToString() + ": Edit", FuncCode = "1001", ImportanceDegree = "1", OperationType = "U", UserID = av.UserID };
                auvm.Function = auvm.Function.Distinct<int>().ToList<int>();
                auvm.Roles = auvm.Roles.Distinct<int>().ToList<int>();
                auvm.Opers = auvm.Opers.Distinct<int>().ToList<int>();
                MenuBL abl = new MenuBL();

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
            if (id > 0 && av != null)
            {
                UserLogModel _log = new UserLogModel { OperationType = "D", FuncCode = "1001", UserID = Convert.ToInt32(av.UserID), ClassName = this.GetType().ToString() + ": Delete" };
                MenuBL abl = new MenuBL();

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