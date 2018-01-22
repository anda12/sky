using System;
using System.Collections.Generic;
using System.Linq;
using Sky.SkyUserData.Entities;
using Sky.SkyUserData.DA;
using Sky.ViewModels;
using System.Linq.Expressions;

namespace Sky.BusinessLy
{
    public class MenuBL
    {
        private SkyMenuData _smd;

        public MenuBL()
        {
            _smd = new SkyMenuData();
        }

        public List<SkyMenu> GetSelectList(Expression<Func<SkyMenu, bool>> where)
        {
            return _smd.GetTbList(where);
        }

        public SkyMenu GetById(int id)
        {
            return _smd.GetById(id);
        }

        public MenuViewModel GetViewByName(string name)
        {
            SkyMenu su = _smd.GetByName(name);
            if (su == null)
            {
                return null;
            }
            MenuViewModel uv = new MenuViewModel(su);

            return uv;
        }
        public List<MenuViewModel> GetPaging(int pageIndex, int pageSize, out int totalCount, int MenuID, string StateFlag, int FuncID, int RoldID)
        {
            IList<SkyMenu> sull = _smd.GetListPage(pageIndex, pageSize, out totalCount, MenuID, StateFlag, FuncID, RoldID);

            List<MenuViewModel> mvl = new List<MenuViewModel>();
            foreach(SkyMenu sm in sull)
            {
                mvl.Add(new MenuViewModel(sm));
            }
            return mvl;
        }

        public bool CheckExist(string name)
        {
            return _smd.CheckExist(name);
        }

        public bool CheckExist(int id)
        {
            return _smd.CheckExist(id);
        }
        public int Add(MenuViewModel auvm)
        {
            SkyMenu su = new SkyMenu() {  Iconic=auvm.Iconic,  IsLeaf=auvm.IsLeaf, MenuLevel=auvm.MenuLevel, Name=auvm.Name, ParentID=auvm.ParentID, Remark=auvm.Remark, Sort=auvm.Sort, StateFlag=auvm.StateFlag, URL=auvm.URL };
            RoleBL rbl = new RoleBL();
            FunctionBL fbl = new FunctionBL();
            OperateBL obl = new OperateBL();

            su.Roles = new List<SkyRole>();
            foreach (int i in auvm.Roles)
            {
                SkyRole sr = rbl.GetById(i);
                if (sr != null)
                {
                    su.Roles.Add(sr);
                }
            }

            su.Function = new List<SkyFunction>();
            foreach (int i in auvm.Function)
            {
                SkyFunction sm = fbl.GetById(i);
                if (sm != null)
                {
                    su.Function.Add(sm);
                }
            }

            su.Opers = new List<SkyOperate>();
            foreach(int i in auvm.Opers)
            {
                SkyOperate so = obl.GetById(i);
                if (so != null)
                {
                    su.Opers.Add(so);
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

        public MenuViewModel GetViewById(int id)
        {
            SkyMenu su = _smd.GetById(id);
            if (su == null)
            {
                return null;
            }
            MenuViewModel uv = new MenuViewModel(su);
            return uv;
        }

        public int Edit(MenuViewModel auvm)
        {
            SkyMenu su = new SkyMenu() { Iconic = auvm.Iconic, StateFlag = auvm.StateFlag, MenuID = auvm.MenuID, IsLeaf = auvm.IsLeaf, MenuLevel = auvm.MenuLevel, Name = auvm.Name, ParentID = auvm.ParentID, Remark = auvm.Remark, Sort = auvm.Sort, URL = auvm.URL };
            RoleBL rbl = new RoleBL();
            FunctionBL mbl = new FunctionBL();
            OperateBL obl = new OperateBL();

            //su.UPassword = auvm.UPassword;
            su.Roles = new List<SkyRole>();
            foreach (int rls in auvm.Roles)
            {
                SkyRole sr = rbl.GetById(rls);
                if (sr != null)
                {
                    su.Roles.Add(sr);
                }
            }
            su.Function = new List<SkyFunction>();
            foreach (int rls in auvm.Function)
            {
                SkyFunction sm = mbl.GetById(rls);
                if (sm != null)
                {
                    su.Function.Add(sm);
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