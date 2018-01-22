using System;
using System.Collections.Generic;
using System.Linq;
using Sky.SkyUserData.Entities;
using Sky.SkyUserData.DA;
using Sky.ViewModels;
using System.Linq.Expressions;

namespace Sky.BusinessLy
{
    public class OperateBL
    {
                private SkyOperData _smd;

                public OperateBL()
        {
            _smd = new SkyOperData();
        }

        public List<SkyOperate> GetSelectList(Expression<Func<SkyOperate, bool>> where)
        {
            return _smd.GetTbList(where);
        }

        public SkyOperate GetById(int id)
        {
            return _smd.GetById(id);
        }

        public OperateViewModel GetViewByName(string name)
        {
            SkyOperate su = _smd.GetByName(name);
            if (su == null)
            {
                return null;
            }
            OperateViewModel uv = new OperateViewModel(su);

            return uv;
        }
        public List<OperateViewModel> GetPaging(int pageIndex, int pageSize, out int totalCount, int MenuID, string StateFlag)
        {
            IList<SkyOperate> sull = _smd.GetListPage(pageIndex, pageSize, out totalCount, MenuID, StateFlag);

            List<OperateViewModel> mvl = new List<OperateViewModel>();
            foreach(SkyOperate sm in sull)
            {
                mvl.Add(new OperateViewModel(sm));
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
        public int Add(OperateViewModel auvm)
        {
            SkyOperate su = new SkyOperate() {  Iconic=auvm.Iconic,  Status=auvm.Status, OperName=auvm.OperName, OperID=auvm.OperID, OperCode=auvm.OperCode, EventName=auvm.EventName };
            RoleBL rbl = new RoleBL();
            MenuBL mbl = new MenuBL();

            su.Roles = new List<SkyRole>();
            foreach (int i in auvm.Roles)
            {
                SkyRole sr = rbl.GetById(i);
                if (sr != null)
                {
                    su.Roles.Add(sr);
                }
            }

            su.Menus = new List<SkyMenu>();
            foreach (int i in auvm.Menus)
            {
                SkyMenu sr = mbl.GetById(i);
                if (sr != null)
                {
                    su.Menus.Add(sr);
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

        public OperateViewModel GetViewById(int id)
        {
            SkyOperate su = _smd.GetById(id);
            if (su == null)
            {
                return null;
            }
            OperateViewModel uv = new OperateViewModel(su);
            return uv;
        }

        public int Edit(OperateViewModel auvm)
        {
            SkyOperate su = new SkyOperate() { Iconic = auvm.Iconic, Status = auvm.Status, OperName = auvm.OperName, OperID = auvm.OperID, OperCode = auvm.OperCode, EventName = auvm.EventName };
            RoleBL rbl = new RoleBL();
            MenuBL mbl = new MenuBL();

            su.Roles = new List<SkyRole>();
            foreach (int i in auvm.Roles)
            {
                SkyRole sr = rbl.GetById(i);
                if (sr != null)
                {
                    su.Roles.Add(sr);
                }
            }

            su.Menus = new List<SkyMenu>();
            foreach (int i in auvm.Menus)
            {
                SkyMenu sr = mbl.GetById(i);
                if (sr != null)
                {
                    su.Menus.Add(sr);
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