using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Sky.SkyUserData.Entities;

namespace Sky.ViewModels
{
    public class RoleViewModel
    {
        [Display(Name = "主键")]
        public  int RoldID { get; set; }
        [Required(ErrorMessage = "请填写角色名称")]
        [DisplayName("角色名")]
        [StringLength(18, ErrorMessage = "{0} 必须至少包含 {2} 个字符。", MinimumLength = 4)]
        public  string RoleName { get; set; }

        [DisplayName("创建时间")]
        [DataType(DataType.DateTime, ErrorMessage = "时间格式不正确")]
        public  string CreateTime { get; set; }
        [DisplayName("创建者ID")]
        public  int CreateUser { get; set; }
        [DisplayName("修改时间")]
        public  string UpdateTime { get; set; }
        [DisplayName("修改者ID")]
        public  int UpdateUser { get; set; }

        [Display(Name = "说明")]
        [StringLength(100, ErrorMessage = "长度不可超过100")]
        public  string Remark { get; set; }

        [Display(Name = "角色状态")]
        public  string StateFlag { get; set; }
        public string StateFlagStr { get; set; }
        [Display(Name = "角色用户")]
        public List<int> Users { get; set; }
        [Display(Name = "所属的系统")]
        public List<int> Funcs { get; set; }
        public string FuncsStr { get; set; }
        [Display(Name = "操作的菜单")]
        public List<int> Menus { get; set; }
        public string MenusStr { get; set; }
        [Display(Name = "执行的操作")]
        public List<int> Opers { get; set; }
        public string OpersStr { get; set; }

        public string UsersStr { get; set; }
        public RoleViewModel()
        {

        }

        public RoleViewModel(SkyRole  sr)
        {
            this.CreateTime = sr.CreateTime.ToString();
            this.CreateUser = sr.CreateUser;
            this.Remark = sr.Remark;
            this.RoldID = sr.RoldID;
            this.RoleName = sr.RoleName;
            this.StateFlag = sr.StateFlag;
            this.StateFlagStr = sr.StateFlag == "1" ? "激活" : "未激活";
            this.UpdateTime = sr.UpdateTime.ToString();
            this.UpdateUser = sr.UpdateUser;
            this.Funcs = new List<int>();
            this.FuncsStr = string.Empty;
            foreach(SkyFunction sf in sr.Funcs)
            {
                if (!this.Funcs.Contains(sf.FuncID))
                {
                    this.Funcs.Add(sf.FuncID);
                    this.FuncsStr += sf.FuncName + "; ";
                }
            }
            this.Menus = new List<int>();
            this.MenusStr = string.Empty;
            foreach(SkyMenu sm in sr.Menus)
            {
                if (!this.Menus.Contains(sm.MenuID))
                {
                    this.Menus.Add(sm.MenuID);
                    this.MenusStr += sm.Name + ": ";
                }
            }
            this.Opers = new List<int>();
            this.OpersStr = string.Empty;
            foreach (SkyOperate sm in sr.Opers)
            {
                if (!this.Opers.Contains(sm.OperID))
                {
                    this.Opers.Add(sm.OperID);
                    this.OpersStr += sm.OperName + ": ";
                }
            }

            this.Users = new List<int>();
            this.UsersStr = string.Empty;
            foreach(SkyUser su in sr.Users)
            {
                if(!this.Users.Contains(su.UserID))
                {
                    this.Users.Add(su.UserID);
                    this.UsersStr += su.UserName + "; ";
                }
            }
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

        public List<string> Funcs2Str()
        {
            List<string> rl = new List<string>();
            foreach (int i in this.Funcs)
            {
                rl.Add(i.ToString());
            }
            return rl;
        }

        public List<string> Opers2Str()
        {
            List<string> rl = new List<string>();
            foreach (int i in this.Opers)
            {
                rl.Add(i.ToString());
            }
            return rl;
        }

        public List<string> Users2Str()
        {
            List<string> rl = new List<string>();
            foreach (int i in this.Users)
            {
                rl.Add(i.ToString());
            }
            return rl;
        }
    }


}