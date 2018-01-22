using System;
using System.Collections.Generic;
using Sky.SkyUserData.Entities;

namespace Sky.ViewModels
{
    public class OperateViewModel
    {
        public  int OperID { get; set; }
        public  string OperCode { get; set; }
        public  string OperName { get; set; }
        public  string EventName { get; set; }
        public  string Status { get; set; }
        public  string Iconic { get; set; }
        public string StatusStr { get; set; }

        public List<int> Menus { get; set; }
        public string MenusStr { get; set; }
        public List<int> Roles { get; set; }
        public string RolesStr { get; set; }
        public OperateViewModel() { }
        public OperateViewModel(SkyOperate so)
        {
            this.EventName = so.EventName;
            this.Iconic = so.Iconic;
            this.OperCode = so.OperCode;
            this.OperID = so.OperID;
            this.OperName = so.OperName;
            this.Status = so.Status;
            this.StatusStr = so.Status == "1" ? "激活" : "未激活";
            this.Menus = new List<int>();
            this.MenusStr = string.Empty;
            foreach(SkyMenu sm in so.Menus)
            {
                this.Menus.Add(sm.MenuID);
                this.MenusStr += sm.Name + "; ";
            }

            this.Roles = new List<int>();
            this.RolesStr = string.Empty;
            foreach (SkyRole sm in so.Roles)
            {
                this.Roles.Add(sm.RoldID);
                this.RolesStr += sm.RoleName + "; ";
            }
        }

        public List<string> Roles2Str()
        {
            List<string> rl = new List<string>();
            foreach (int i in this.Roles)
            {
                rl.Add(i.ToString());
            }
            return rl;
        }

        public List<string> Menus2Str()
        {
            List<string> rl = new List<string>();
            foreach (int i in this.Menus)
            {
                rl.Add(i.ToString());
            }
            return rl;
        }
    }
}