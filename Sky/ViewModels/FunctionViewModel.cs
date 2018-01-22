using System;
using System.Collections.Generic;
using Sky.SkyUserData.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
namespace Sky.ViewModels
{
    public class FunctionViewModel
    {
        [Display(Name = "主键")]
        public int FuncID { get; set; }

        [DisplayName("子系统编码")]
        [StringLength(12, ErrorMessage = "{0} 必须至少包含 {2} 个字符。", MinimumLength = 4)]
        public string FuncCode { get; set; }
        [Required(ErrorMessage = "请填写子系统名称")]
        [DisplayName("子系统名")]
        [StringLength(60, ErrorMessage = "{0} 必须至少包含 {2} 个字符。", MinimumLength = 4)]
        public string FuncName { get; set; }
        [Display(Name = "角色状态")]
        public string StateFlag { get; set; }

        public string StateFlagStr { get; set; }

        [DisplayName("系统名")]
        [StringLength(8, ErrorMessage = "{0} 必须至少包含 {2} 个字符。", MinimumLength = 3)]
        public string System { get; set; }
        public string Iconic { get; set; }

        public List<int> Roles { get; set; }

        public string RolesStr { get; set; }
        public List<int> Menus { get; set; }

        public string MenusStr { get; set; }
        public FunctionViewModel()
        {
        }

        public FunctionViewModel(SkyFunction sf)
        {
            this.FuncCode = sf.FuncCode;
            this.FuncID = sf.FuncID;
            this.FuncName = sf.FuncName;
            this.Iconic = sf.Iconic;
            this.StateFlag = sf.StateFlag;
            this.StateFlagStr = sf.StateFlag == "1" ? "激活" : "未激活";
            this.System = sf.System;
            this.MenusStr = string.Empty;
            this.Menus = new List<int>();
            foreach (SkyMenu sm in sf.Menus)
            {
                this.Menus.Add(sm.MenuID);
                this.MenusStr += sm.Name + "; ";
            }

            this.RolesStr = string.Empty;
            this.Roles = new List<int>();
            foreach (SkyRole sr in sf.Roles)
            {
                this.Roles.Add(sr.RoldID);
                this.RolesStr += sr.RoleName + "; ";
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

        public List<string> Roles2Str()
        {
            List<string> rl = new List<string>();
            foreach (int i in this.Roles)
            {
                rl.Add(i.ToString());
            }
            return rl;
        }
    }
}