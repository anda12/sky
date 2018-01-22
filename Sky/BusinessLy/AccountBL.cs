using System;
using System.Collections.Generic;
using System.Linq;
using Sky.SkyUserData.Entities;
using Sky.SkyUserData.DA;
using Sky.ViewModels;
using System.Linq.Expressions;

namespace Sky.BusinessLy
{
    public class AccountBL
    {
        private SkyUserDatas _smd;

        public AccountBL()
        {
            _smd = new SkyUserDatas();
        }

        public List<SkyUser> GetSelectList(Expression<Func<SkyUser, bool>> where)
        {
            return _smd.GetTbList(where);
        }

        protected SkySysViewModel GetSS()
        {
            SkySysViewModel foot = new SkySysViewModel();

            foot.producer = "中滦科技";
            foot.year = "2017";
            foot.owner = "中滦科技";

            return foot;
        }

        public List<UserViewModel> GetUserPaging(int pageIndex, int pageSize, out int totalCount, int UserID, string StateFlag, int CreateUser, string UsID)
        {
            IList<SkyUser> sull = _smd.GetListPage(pageIndex, pageSize, out totalCount, UserID, StateFlag, CreateUser, UsID);

            List<UserViewModel> rl = new List<UserViewModel>();
            foreach (SkyUser su in sull)
            {
                UserViewModel uv = new UserViewModel(su);
                rl.Add(uv);
            }

            return rl;
        }

        private UserMenuViewModel FromUserGetUm(SkyUser su)
        {
            UserMenuViewModel um = new UserMenuViewModel();

            um.UserMenus = new List<FuncMenu>();

            IList<SkyRole> lsr = su.Roles;
            Dictionary<string, List<int>> funcd = new  Dictionary<string,List<int>>();
            foreach(SkyRole sr in lsr)
            {
                foreach (SkyFunction sf in sr.Funcs)
                {

                    if (funcd.ContainsKey(sf.FuncName))
                    {
                        #region exist
                        foreach (FuncMenu fm in um.UserMenus)
                        {
                            if(fm.FunctionName == "\""+sf.FuncName+"\"")
                            {
                                foreach (SkyMenu sm in sr.Menus)
                                {
                                    if (sm.IsLeaf == "0" && funcd[sf.FuncName].Contains(sm.MenuID))
                                    {
                                        bool find =false;
                                        foreach(MainMenu mm in fm.FunctionMenus)
                                        {
                                            if (mm.menuid == sm.MenuID)
                                            {
                                                find = true;
                                                break;
                                            }
                                        }

                                        if(find == false)
                                        {
                                            MainMenu mmt = new MainMenu();
                                            mmt.icon = sm.Iconic;
                                            mmt.menuid = sm.MenuID;
                                            mmt.menuname = sm.Name;
                                            mmt.menus = new List<LeafMenu>();
                                            fm.FunctionMenus.Add(mmt);
                                        }
                                    }
                                }

                                foreach(SkyMenu sm in sr.Menus)
                                {
                                    if(sm.IsLeaf == "1")
                                    {
                                        bool find = false;

                                        foreach(MainMenu mm in fm.FunctionMenus)
                                        {
                                            if(sm.ParentID == mm.menuid)
                                            {
                                                
                                                foreach(LeafMenu lm in mm.menus)
                                                {
                                                    if (sm.MenuID == lm.menuid)
                                                    {
                                                        find = true;
                                                        break;
                                                    }
                                                }

                                                    break;

                                            }
                                        }
                                        if (find == false)
                                        {
                                            foreach (MainMenu mm in fm.FunctionMenus)
                                            {
                                                if (sm.ParentID == mm.menuid)
                                                {

                                                    LeafMenu lm = new LeafMenu();

                                                    lm.icon = sm.Iconic;
                                                    lm.menuid = sm.MenuID;
                                                    lm.menuname = sm.Name;
                                                    lm.url = sm.URL+ "/"+ sm.MenuID.ToString();

                                                    mm.menus.Add(lm);
                                                }
                                            }
                                        }
                                    }
                                }

                                break;
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        List<int> ml = new List<int>();

                        foreach(SkyMenu t in sf.Menus)
                        {
                            ml.Add(t.MenuID);
                        }

                        funcd.Add(sf.FuncName, ml);

                        FuncMenu fmt = new FuncMenu();
                        fmt.FunctionName = "\""+ sf.FuncName+"\"";
                        fmt.FunctionMenus = new List<MainMenu>();
                        foreach (SkyMenu sm in sr.Menus)
                        {
                            if (sm.IsLeaf == "0" && ml.Contains(sm.MenuID))
                            {
                                MainMenu mmt = new MainMenu();
                                mmt.icon = sm.Iconic;
                                mmt.menuid = sm.MenuID;
                                mmt.menuname = sm.Name;
                                mmt.menus = new List<LeafMenu>();
                                fmt.FunctionMenus.Add(mmt);
                            }
                        }

                        foreach (MainMenu mm in fmt.FunctionMenus)
                        {
                            foreach (SkyMenu sm in sr.Menus)
                            {
                                if (sm.IsLeaf == "1" && sm.ParentID == mm.menuid)
                                {
                                    LeafMenu lmt = new LeafMenu();
                                    lmt.icon = sm.Iconic;
                                    lmt.menuid = sm.MenuID;
                                    lmt.menuname = sm.Name;
                                    lmt.url = sm.URL+"/"+sm.MenuID.ToString();

                                    mm.menus.Add(lmt);
                                }
                            }
                        }

                        um.UserMenus.Add(fmt);
                    }


                }
                
            }


            return um;
        }

        public AccountViewModel ValidateUser(string username, string passwd)
        {
            SkyUser su = _smd.GetByName(username);
            //SkyUser su = _smd.GetByRoleId(4);

            if(su == null)
            {
                return null;
            }

            if (su.UPassword == passwd)
            {
                AccountViewModel avm = new AccountViewModel();

                avm.UserID = su.UserID;
                avm.UserName = su.UserName;
                avm.UsID = username;
                avm.UsPassword = passwd;
                avm.funclist = new List<string>();
                avm.Roles = new List<int>();
                foreach(SkyRole sr in su.Roles)
                {
                    foreach(SkyFunction sf in sr.Funcs)
                    {
                        if(!avm.funclist.Contains(sf.FuncName))
                        {
                            avm.funclist.Add(sf.FuncName);
                        }
                    }

                    avm.Roles.Add(sr.RoldID);
                }

                avm.ss = GetSS();
                avm.um = FromUserGetUm(su);

                return avm;
            }
            else
            {
                return null;
            }
        }

        public bool CheckExist(string usname)
        {
            return _smd.CheckExist(usname);
        }

        public bool CheckExist(int id)
        {
            return _smd.CheckExist(id);
        }
        public int AddAccount(UserViewModel auvm, int creater)
        {
            SkyUser su = new SkyUser();
            RoleBL rbl = new RoleBL();

            su.CreateTime = DateTime.Now;
            su.CreateUser = creater;
            su.Gender = auvm.Gender[0];
            //su.Roles = new List<int> { auvm.Role };
            su.StateFlag = auvm.StateFlag == true ? "1":"2";
            su.UPassword = auvm.UPassword;
            if(string.IsNullOrWhiteSpace(auvm.UserName))
            {
                su.UserName = su.UsID;
            }
            else
            {
                su.UserName = auvm.UserName;
            }
            su.UsID = auvm.UsID;
            su.Roles = new List<SkyRole>();
            su.UpdateUser = 0;
            su.UpdateTime = DateTime.Now;
            foreach (int rls in auvm.Role)
            {
                SkyRole sr = rbl.GetById(rls);
                if (sr != null)
                {
                    su.Roles.Add(sr);
                }
            }


            if(_smd.Insert(su))
            {
                return 0;
            }
            else
            {
                return -1;
            }
            
        }

        public UserViewModel GetViewById(int id)
        {
            SkyUser su = _smd.GetById(id);
            if (su == null)
            {
                return null;
            }
            UserViewModel uv= new UserViewModel(su);

            return uv;
        }
        public UserViewModel GetViewByName(string name)
        {
            SkyUser su = _smd.GetByName(name);
            if (su == null)
            {
                return null;
            }
            UserViewModel uv = new UserViewModel(su);
            return uv;
        }
        public SkyUser GetById(int id)
        {
            return _smd.GetById(id);
        }

        public int Edit(UserViewModel auvm)
        {
            SkyUser su = new SkyUser() { CreateTime = DateTime.Parse(auvm.CreateTime), CreateUser = auvm.CreateUser, Gender = auvm.Gender[0], StateFlag = auvm.StateFlag == true ? "1" : "2", UPassword=auvm.UPassword, UpdateTime=DateTime.Now, UserID= int.Parse(auvm.UserID), UserName = auvm.UserName, UsID=auvm.UsID };
            RoleBL rbl = new RoleBL();

            //su.UPassword = auvm.UPassword;
            if (string.IsNullOrWhiteSpace(auvm.UserName))
            {
                su.UserName = su.UsID;
            }
            else
            {
                su.UserName = auvm.UserName;
            }
            su.Roles = new List<SkyRole>();
            foreach (int rls in auvm.Role)
            {
                SkyRole sr = rbl.GetById(rls);
                if (sr != null)
                {
                    su.Roles.Add(sr);
                }
            }
            su.UserID = int.Parse(auvm.UserID);

            if (_smd.Update(su))
            {
                return 0;
            }
            else
            {
                return -1;
            }
        }

        public int Delete(int id)
        {
            if(_smd.Delete(id))
            {
                return 0;
            }
            else
            {
                return -1;
            }
        }
    }
}