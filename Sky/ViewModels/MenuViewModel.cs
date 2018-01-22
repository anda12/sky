using System;
using System.Collections.Generic;
using Sky.SkyUserData.Entities;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Sky.ViewModels
{
    public class MenuViewModel
    {
        [DisplayName("菜单ID")]
        public virtual int MenuID { get; set; }
        [Required(ErrorMessage = "请填写菜单名成")]
        [DisplayName("菜单名称")]
        [StringLength(32, ErrorMessage = "{0} 必须至少包含 {2} 个字符。", MinimumLength = 3)]
        public virtual string Name { get; set; }
        [DisplayName("父菜单ID")]
        public virtual int ParentID { get; set; }
        [DisplayName("菜单网址")]
        [StringLength(64, ErrorMessage = "{0} 必须至少包含 {2} 个字符。", MinimumLength = 3)]
        public virtual string URL { get; set; }
        [DisplayName("显示图标")]
        public virtual string Iconic { get; set; }
        public virtual string IconicStr { get; set; }
        [DisplayName("显示排序")]
        public virtual int Sort { get; set; }
        [DisplayName("备注")]
        [StringLength(128, ErrorMessage = "{0} 必须至少包含 {2} 个字符。")]
        public virtual string Remark { get; set; }
        [DisplayName("菜单级别")]
        public virtual int MenuLevel { get; set; }
        [DisplayName("是否激活")]
        public virtual string StateFlag { get; set; }
        public virtual string StateFlagStr { get; set; }
        [DisplayName("是否叶子节点")]
        public virtual string IsLeaf { get; set; }
        [DisplayName("菜单所属角色")]
        public virtual List<int> Roles { get; set; }
        public string RolesStr { get; set; }
        [DisplayName("菜单所属系统")]
        public virtual List<int> Function { get; set; }

        [DisplayName("菜单支持操作")]
        public virtual List<int> Opers { get; set; }

        public string OpersStr { get; set; }
        public string FunctionStr { get; set; }
        public MenuViewModel() { }

        public string HtmlEncode(string theString)
        {
            theString = theString.Replace(">", "&gt;");
            theString = theString.Replace("<", "&lt;");
            theString = theString.Replace(" ", "&nbsp;");
            theString = theString.Replace("\"", "&quot;");
            theString = theString.Replace("\'", "&#39;");
            theString = theString.Replace("\n", "<br/>");
            return theString;
        }
        public MenuViewModel(SkyMenu sm) 
        {
            this.Function = new List<int>();
            this.FunctionStr = string.Empty;
            foreach(SkyFunction sf in sm.Function)
            {
                this.Function.Add(sf.FuncID);
                this.FunctionStr += sf.FuncName + "; ";
            }


            //this.IconicStr = "<div class='easyui-panel' data-options='iconCls:\"" + sm.Iconic + "\",title:\"图标\",width:40 '></div>";
            //this.IconicStr = HtmlEncode(this.IconicStr);
            this.IsLeaf = sm.IsLeaf;
            this.MenuID = sm.MenuID;
            this.MenuLevel = sm.MenuLevel;
            this.Name = sm.Name;
            this.ParentID = sm.ParentID;
            this.Remark = sm.Remark;
            this.Iconic = sm.Iconic + "_" + this.MenuID;
            this.Roles = new List<int>();
            this.RolesStr = string.Empty;
            foreach(SkyRole sr in sm.Roles)
            {
                this.Roles.Add(sr.RoldID);
                this.RolesStr += sr.RoleName + "; ";
            }
            this.Opers = new List<int>();
            this.OpersStr = string.Empty;
            foreach(SkyOperate so in sm.Opers)
            {
                this.Opers.Add(so.OperID);
                this.OpersStr += so.OperName + "; ";
            }
            this.Sort = sm.Sort;
            this.StateFlag = sm.StateFlag;
            this.StateFlagStr = sm.StateFlag == "1"?"激活":"未激活";
            this.URL = sm.URL;
        }

        public List<string> Function2Str()
        {
            List<string> rl = new List<string>();
            foreach (int i in this.Function)
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

        public List<string> Opers2Str()
        {
            List<string> rl = new List<string>();
            foreach (int i in this.Opers)
            {
                rl.Add(i.ToString());
            }
            return rl;
        }
    }
}