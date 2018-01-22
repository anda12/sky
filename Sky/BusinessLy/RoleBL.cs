using System;
using System.Collections.Generic;
using System.Linq;
using Sky.SkyUserData.Entities;
using Sky.SkyUserData.DA;
using Sky.ViewModels;
using System.Linq.Expressions;
using Sky.Model;

namespace Sky.BusinessLy
{
    public class RoleBL
    {
        private SkyRoleData _smd;

        public RoleBL()
        {
            _smd = new SkyRoleData();
        }

        public IList<SkyRole> GetSelectList(Expression<Func<SkyRole, bool>> where)
        {
            IList<SkyRole> srl = _smd.GetTbList(where);
            return srl;
        }

        public List<RoleViewModel> GetPaging(int pageIndex, int pageSize, out int totalCount, int UserID, string StateFlag, int CreateUser, string UsID)
        {
            IList<SkyRole> sull = _smd.GetListPage(pageIndex, pageSize, out totalCount, UserID, StateFlag, CreateUser, UsID);

            List<RoleViewModel> rl = new List<RoleViewModel>();
            foreach (SkyRole su in sull)
            {
                RoleViewModel uv = new RoleViewModel(su);

                rl.Add(uv);
            }

            return rl;
        }

        public SkyRole GetById(int id)
        {
            return _smd.GetById(id);
        }

        public bool CheckExist(string name)
        {
            return _smd.CheckExist(name);
        }

        public bool CheckExist(int id)
        {
            return _smd.CheckExist(id);
        }
        public int Add(RoleViewModel auvm)
        {
            SkyRole su = new SkyRole() { CreateTime=DateTime.Now, CreateUser=auvm.CreateUser,  Remark=auvm.Remark,  RoleName=auvm.RoleName, StateFlag = auvm.StateFlag, UpdateTime=DateTime.Now, UpdateUser=0, Users=null };
            FunctionBL rbl = new FunctionBL();
            MenuBL mbl = new MenuBL();
            OperateBL obl = new OperateBL();
            AccountBL abl = new AccountBL();

            su.Funcs = new List<SkyFunction>();
            foreach (int rls in auvm.Funcs)
            {
                SkyFunction sr = rbl.GetById(rls);
                if (sr != null)
                {
                    su.Funcs.Add(sr);
                }
            }

            su.Menus = new List<SkyMenu>();
            foreach(int rls in auvm.Menus)
            {
                SkyMenu sm = mbl.GetById(rls);
                if(sm!=null)
                {
                    su.Menus.Add(sm);
                }
            }

            su.Opers = new List<SkyOperate>();
            foreach(int i in auvm.Opers)
            {
                SkyOperate so = obl.GetById(i);
                if(so!=null)
                {
                    su.Opers.Add(so);
                }
            }

            su.Users = new List<SkyUser>();
            foreach (int i in auvm.Users)
            {
                SkyUser so = abl.GetById(i);
                if (so != null)
                {
                    su.Users.Add(so);
                }
            }

            if (_smd.Insert(su))
            {
                return 0;
            }
            else
            {
                return -1;
            }

        }

        public RoleViewModel GetViewById(int id)
        {
            SkyRole su = _smd.GetById(id);
            if(su==null)
            {
                return null;
            }
            RoleViewModel uv = new RoleViewModel(su);
            return uv;
        }

        public RoleViewModel GetViewByName(string name)
        {
            SkyRole su = _smd.GetByName(name);
            if (su == null)
            {
                return null;
            }
            RoleViewModel uv = new RoleViewModel(su);
            return uv;
        }

        public int Edit(RoleViewModel auvm)
        {
            SkyRole su = new SkyRole() { CreateTime = DateTime.Parse(auvm.CreateTime), CreateUser = auvm.CreateUser, StateFlag = auvm.StateFlag, UpdateTime = DateTime.Now, Remark=auvm.Remark,  RoleName=auvm.RoleName, UpdateUser=auvm.UpdateUser, RoldID=auvm.RoldID,  };
            FunctionBL rbl = new FunctionBL();
            MenuBL mbl = new MenuBL();
            OperateBL obl = new OperateBL();
            AccountBL abl = new AccountBL();

            //su.UPassword = auvm.UPassword;
            su.Funcs = new List<SkyFunction>();
            foreach (int rls in auvm.Funcs)
            {
                SkyFunction sr = rbl.GetById(rls);
                if (sr != null)
                {
                    su.Funcs.Add(sr);
                }
            }
            su.Menus = new List<SkyMenu>();
            foreach (int rls in auvm.Menus)
            {
                SkyMenu sm = mbl.GetById(rls);
                if (sm != null)
                {
                    su.Menus.Add(sm);
                }
            }

            su.Opers = new List<SkyOperate>();
            foreach(int i in auvm.Opers)
            {
                SkyOperate so = obl.GetById(i);
                if(so!=null)
                {
                    su.Opers.Add(so);
                }
            }

            su.Users = new List<SkyUser>();
            foreach(int i in auvm.Users)
            {
                SkyUser s = abl.GetById(i);
                if(s!=null)
                {
                    su.Users.Add(s);
                }
            }

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
            if (_smd.Delete(id))
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