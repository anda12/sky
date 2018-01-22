using System;
using System.Collections.Generic;
using Sky.SkyUserData.Entities;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Sky.ViewModels
{
    public class UserViewModel
    {
        [Required(ErrorMessage = "请填写用户名")]
        [DisplayName("用户名")]
        [StringLength(18, ErrorMessage = "{0} 必须至少包含 {2} 个字符。", MinimumLength = 4)]
        public string UsID { get; set; }

        [Required(ErrorMessage = "请填写密码")]
        [DisplayName("密码")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "{0} 必须至少包含 {2} 个字符。", MinimumLength = 3)]
        public string UPassword { get; set; }

        [DataType(DataType.Password)]
        [DisplayName("确认密码")]
        [StringLength(100, ErrorMessage = "{0} 必须至少包含 {2} 个字符。", MinimumLength = 3)]
        [Compare("UPassword", ErrorMessage = "两次密码不一致")]
        public string CUPassword { get; set; }

        [DisplayName("性别")]
        public string Gender { get; set; }

        [DisplayName("真实姓名")]
        [StringLength(18, ErrorMessage = "{0} 必须至少包含 {2} 个字符。", MinimumLength = 4)]
        public string UserName { get; set; }

        [DisplayName("角色")]
        public List<int> Role { get; set; }

        public string Roles { get; set; }

        [DisplayName("是否激活")]
        public bool StateFlag { get; set; }
        public string StateFlags { get; set; }

        [DisplayName("创建者ID")]
        public int CreateUser { get; set; }
        [DisplayName("创建时间")]
        public string CreateTime { get; set; }
        [DisplayName("用户ID")]
        public string UserID { get; set; }

        public int UpdateUser { get; set; }
        public string UpdateTime { get; set; }


        public UserViewModel(SkyUser su)
        {
            this.CreateTime = su.CreateTime.ToString();
            this.CreateUser = su.CreateUser;
            if (su.Gender == '1')
            {
                this.Gender = "男";
            }
            else
            {
                this.Gender = "女";
            }
            this.StateFlag = su.StateFlag == "1" ? true : false;
            this.StateFlags = su.StateFlag == "1" ? "激活" : "未激活";
            this.UserID = su.UserID.ToString();
            this.UsID = su.UsID;

            this.UPassword = su.UPassword;
            this.CUPassword = su.UPassword;
            this.UpdateTime = su.UpdateTime.ToString();
            this.UpdateUser = su.UpdateUser;
            this.Role = new List<int>();
            this.Roles = string.Empty;

            foreach (SkyRole sr in su.Roles)
            {
                //RoleListS rl = new RoleListS() { id= sr.RoldID, name=sr.RoleName};
                this.Role.Add(sr.RoldID);
                this.Roles += sr.RoleName + "; ";
            }

            this.UserName = su.UserName;
        }

        public UserViewModel()
        {

        }

        public List<string> Role2Str()
        {
            List<string> rl = new List<string>();
            foreach(int i in this.Role)
            {
                rl.Add(i.ToString());
            }
            return rl;
        }
    }
}