using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Sky.ViewModels;
using Sky.Common;
using Newtonsoft.Json;
using Sky.BusinessLy;

namespace Sky.Controllers
{
    public class BaseController : Controller
    {
        // GET: Base

        protected SkySysViewModel GetSS()
        {
            SkySysViewModel foot = new SkySysViewModel();

            foot.producer = "中滦科技";
            foot.year = "2017";
            foot.owner = "中滦科技";

            return foot;
        }


        protected AccountViewModel ValidateUser(string username, string passwd)
        {
            AccountBL abl = new AccountBL();

            return abl.ValidateUser(username, passwd);
            //return GetAccount();
        }

        protected int ChangePass(string Accname, string oldpass, string newpass)
        {
            return 1;
        }

        protected AccountViewModel GetCurrentAccount()
        {
            if (Request.Cookies["sessionId"] != null)
            {
                string sessionId = Request.Cookies["sessionId"].Value;//接收从Cookie中传递过来的Memcache的key
                object obj = MemcacheHelper.Get(sessionId);//根据key从Memcache中获取用户的信息
                return obj == null ? null : SerializerHelper.DeserializeToObject<AccountViewModel>(obj.ToString()); ;
            }
            else
            {
                return null;
            }
        }

        public Dictionary<int, OperateViewModel> GetOperate(int menuid)
        {
            AccountViewModel avm = GetCurrentAccount();

            if (avm == null)
            {
                return null;
            }
            RoleBL rbl = new RoleBL();
            OperateBL obl = new OperateBL();

            Dictionary<int, OperateViewModel> ovmd = new Dictionary<int, OperateViewModel>();
            foreach (int i in avm.Roles)
            {
                RoleViewModel rvm = rbl.GetViewById(i);
                foreach (int j in rvm.Opers)
                {
                    if (!ovmd.ContainsKey(j))
                    {
                        ovmd.Add(j, obl.GetViewById(j));
                    }
                }
            }

            MenuBL mbl = new MenuBL();
            MenuViewModel mvm = mbl.GetViewById(menuid);
            foreach (KeyValuePair<int, OperateViewModel> kvp in ovmd)
            {
                if (!mvm.Opers.Contains(kvp.Key))
                {
                    ovmd.Remove(kvp.Key);
                }
            }

            return ovmd;
        }
    }
}