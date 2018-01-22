using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Sky.ViewModels
{
    public class LoginOnViewModel
    {
        [Required(ErrorMessage = "请填写用户名")]
        [DisplayName("用户名")]
        public string UsID { get; set; }

        [StringLength(100, ErrorMessage = "{0} 必须至少包含 {2} 个字符。", MinimumLength = 3)]
        [Required(ErrorMessage = "请填写密码")]
        [DataType(DataType.Password)]
        [DisplayName("密码")]
        public string UsPassword { get; set; }

        [Required(ErrorMessage = "请填写验证码")]
        [DisplayName("验证码")]
        public string ValidateCode { get; set; }

        [DisplayName("记住我?")]
        public bool RememberMe { get; set; }
    }
}