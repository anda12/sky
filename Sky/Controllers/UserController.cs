using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sky.BusinessLy;
using Sky.ViewModels;
using Sky.Common;
using Sky.Model;
using Sky.SkyUserData.DA;
using Newtonsoft.Json;

namespace Sky.Controllers
{
    public class UserController : BaseController
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Index2(int id)
        {
            ViewBag.UserOpers = GetOperate(id).Values;
            return View();
        }



        public JsonResult GetData()
        {
            AccountBL ab = new AccountBL();
            int page = int.Parse(Request["page"]);
            int rows = int.Parse(Request["rows"]);
            List<UserViewModel> lulvm = new List<UserViewModel>();
            int total;
            int UserID = -1;
            string StateFlag = null;
            int CreateUser = -1;
            string UsID = null;
            try
            {
                if (!string.IsNullOrWhiteSpace(Request["UserID"]))
                {
                    UserID = int.Parse(Request["UserID"]);
                }

                if (!string.IsNullOrWhiteSpace(Request["CreateUser"]))
                {
                    CreateUser = int.Parse(Request["CreateUser"]);
                }
            }
            catch(Exception err)
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


            if (!string.IsNullOrWhiteSpace(Request["UsID"]))
            {
                UsID = Request["UsID"];
            }

            lulvm = ab.GetUserPaging(page, rows, out total, UserID, StateFlag, CreateUser, UsID);
            var data = new
            {
                total,
                rows = lulvm
            };

            return Json(data, JsonRequestBehavior.AllowGet);

        }

        public ActionResult Create()
        {
            ViewBag.Genders = SelectListClass.GetGenders(true);

            RoleBL rbl = new RoleBL();
            //ViewBag.Roles = new SelectList(rbl.GetRoleSelectList(x => x.RoldID > 1), "RoldID", "RoleName");
            ViewBag.Roles = new MultiSelectList(rbl.GetSelectList(x => x.RoldID > 1), "RoldID", "RoleName");

            return View();
        }
        [HttpPost]
        public ActionResult Create(UserViewModel auvm)
        {
            AccountViewModel av = GetCurrentAccount();
            if(ModelState.IsValid && av!=null)
            {
                AccountBL abl = new AccountBL();
                string returnValue = string.Empty;

                if(abl.CheckExist(auvm.UsID))
                {
                    returnValue = "登录名已存在！";
                    return Json(Suggestion.InsertFail + returnValue, JsonRequestBehavior.AllowGet);
                }

                auvm.UPassword = xEncrypt.EncryptText(auvm.UPassword);
                auvm.Role = auvm.Role.Distinct<int>().ToList<int>();

                UserLogModel _log = new UserLogModel { ClassName = this.GetType().ToString() + ": Create", FuncCode = "1001", ImportanceDegree = "1", OperationType = "N", UserID = av.UserID };

                int rlt = abl.AddAccount(auvm, av.UserID);
                if(rlt == 0)
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

            UserViewModel uvm = new AccountBL().GetViewById(id);
            //ViewBag.Genders = SelectListClass.GetGenders(true);
            RoleBL rbl = new RoleBL();
            
            ViewBag.Roles = new SelectList(rbl.GetSelectList(x => x.RoldID > 1), "RoldID", "RoleName");

            List<SelectListItem> sl = new List<SelectListItem>();

            foreach (var t in ViewBag.Roles)
            {
                bool select = false;
                foreach (int i in uvm.Role)
                {
                    if (t.Value == i.ToString())
                    {
                        sl.Add(new SelectListItem() { Text = t.Text, Selected = true, Value = t.Value });
                        select = true;
                    }

                }
                if (select == false)
                {
                    sl.Add(new SelectListItem() { Text = t.Text, Selected = false, Value = t.Value });
                }
            }
            //ViewBag.RolesSelect = new SelectList(rbl.GetRoleSelectList(x => uvm.Role.Contains(x.RoldID)), "RoldID", "RoleName", true);
            ViewBag.Roles = sl;
            return View(uvm);
        }
        [HttpPost]
        public ActionResult Edit(int id, UserViewModel auvm)
        {
            AccountViewModel av = GetCurrentAccount();
            if(auvm != null && ModelState.IsValid && av!=null)
            {
                auvm.UpdateUser = av.UserID;
                string returnValue = string.Empty;
                UserLogModel _log = new UserLogModel { ClassName = this.GetType().ToString() + ": Edit", FuncCode = "1001", ImportanceDegree = "1", OperationType = "U", UserID = auvm.UpdateUser };
                auvm.Role = auvm.Role.Distinct<int>().ToList<int>();
                AccountBL abl = new AccountBL();

                if (abl.Edit(auvm) == 0)
                {
                    _log.Remark = "用户:" + av.UsID + Suggestion.UpdateSucceed;
                    UserLogBL.RecordUserLog(_log);
                    return Json(Suggestion.UpdateSucceed, JsonRequestBehavior.AllowGet); //提示更新成功 
                }
                else
                {
                    _log.Remark = "用户:" + av.UsID + Suggestion.UpdateFail;
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
            if (id>0 && av!=null)
            {
                UserLogModel _log = new UserLogModel { OperationType = "D", FuncCode = "010000", UserID = Convert.ToInt32(av.UserID), ClassName = this.GetType().ToString() + ": Delete" };
                AccountBL abl = new AccountBL();

                if (abl.Delete(id) == 0)
                {
                    _log.Remark = "用户:" + av.UserName + Suggestion.DeleteSucceed;
                    UserLogBL.RecordUserLog(_log);
                    return Json("OK", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    _log.Remark = "用户:" + av.UserName + Suggestion.DeleteFail;
                    UserLogBL.RecordUserLog(_log);
                }
                returnValue = _log.Remark;
            }

            return Json(returnValue, JsonRequestBehavior.AllowGet);
        }
    }
}