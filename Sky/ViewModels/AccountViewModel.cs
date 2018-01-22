using System;
using System.Collections.Generic;
using System.Linq;
using Sky.SkyUserData.Entities;

namespace Sky.ViewModels
{
    public class AccountViewModel
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        //login username
        public string UsID { get; set; }
        public string UsPassword { get; set; }

        public List<int> Roles { get; set; }
        public List<string> funclist { get; set; }
        public UserMenuViewModel um { get; set; }

        public SkySysViewModel ss { get; set; }
    }
}