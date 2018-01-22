using System;
using System.Collections.Generic;
using System.Linq;
using Sky.SkyUserData.Entities;
using Sky.SkyUserData.DA;
using Sky.ViewModels;
using System.Linq.Expressions;
using Sky.BusinessLy;

namespace Sky.BusinessLy
{
    public class FunctionBL
    {
        private SkyFunctionData _smd;

        public FunctionBL()
        {
            _smd = new SkyFunctionData();
        }

        public List<SkyFunction> GetSelectList(Expression<Func<SkyFunction, bool>> where)
        {
            return _smd.GetTbList(where);
        }

        public List<FunctionViewModel> GetPaging(int pageIndex, int pageSize, out int totalCount, int FuncID, string StateFlag, string FuncName)
        {
            IList<SkyFunction> sull = _smd.GetListPage(pageIndex, pageSize, out totalCount, FuncID, StateFlag, FuncName);

            List<FunctionViewModel> rl = new List<FunctionViewModel>();
            foreach (SkyFunction su in sull)
            {
                FunctionViewModel uv = new FunctionViewModel(su);

                rl.Add(uv);
            }

            return rl;
        }

        public SkyFunction GetById(int id)
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
        public int Add(FunctionViewModel auvm)
        {
            SkyFunction su = new SkyFunction() {  FuncCode=auvm.FuncCode, FuncName=auvm.FuncName, Iconic=auvm.Iconic, StateFlag=auvm.StateFlag, System = auvm.System };
            RoleBL rbl = new RoleBL();
            MenuBL mbl = new MenuBL();

            su.Roles = new List<SkyRole>();
            foreach(int i in auvm.Roles)
            {
                SkyRole sr = rbl.GetById(i);
                su.Roles.Add(sr);
            }

            su.Menus = new List<SkyMenu>();
            foreach(int i in auvm.Menus)
            {
                SkyMenu sm = mbl.GetById(i);
                su.Menus.Add(sm);
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

        public FunctionViewModel GetViewById(int id)
        {
            SkyFunction su = _smd.GetById(id);
            if (su == null)
            {
                return null;
            }
            FunctionViewModel uv = new FunctionViewModel(su);
            return uv;
        }
        public FunctionViewModel GetViewByName(string name)
        {
            SkyFunction su = _smd.GetByName(name);
            if (su == null)
            {
                return null;
            }
            FunctionViewModel uv = new FunctionViewModel(su);

            return uv;
        }
        public int Edit(FunctionViewModel auvm)
        {
            SkyFunction su = new SkyFunction() { FuncCode=auvm.FuncCode, FuncName=auvm.FuncName, FuncID=auvm.FuncID, Iconic=auvm.Iconic, StateFlag=auvm.StateFlag, System=auvm.System };
            RoleBL rbl = new RoleBL();
            MenuBL mbl = new MenuBL();

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
            su.Menus = new List<SkyMenu>();
            foreach (int rls in auvm.Menus)
            {
                SkyMenu sm = mbl.GetById(rls);
                if (sm != null)
                {
                    su.Menus.Add(sm);
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