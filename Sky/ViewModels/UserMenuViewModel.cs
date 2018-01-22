using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace Sky.ViewModels
{
    public class LeafMenu
    {
        public int menuid { get; set; }
        public string menuname { get; set; }
        public string url { get; set; }
        public string icon { get; set; }

        public override string ToString()
        {
            //return "{menuid:" + this.menuid + ",menuname:" + this.menuname + ",url:" + this.url + ",icon:" + this.icon + "}";
            return JsonConvert.SerializeObject(this);
        }
    }

    public class MainMenu
    {
        public int menuid { get; set; }
        public string menuname { get; set; }
        public string icon { get; set; }
        public  List<LeafMenu> menus {get;set;}

        public override string ToString()
        {
            /*
            string rlt = "{menuid:" + this.menuid + ",menuname:" + this.menuname + ",icon:" + this.icon + ",menus:[";
            for(int i =0;i< this.menus.Count;i++)
            {
                if(i ==0)
                {
                    rlt = rlt + this.menus[i].ToString();
                }
                else
                {
                    rlt = rlt +"," + this.menus[i].ToString();
                }
            }
            rlt = rlt + "]}";
            return rlt;
             * */
            return JsonConvert.SerializeObject(this);
        }
    }

    public class FuncMenu
    {
        public string FunctionName { get; set; }
        public List<MainMenu> FunctionMenus { get; set; }

        public override string ToString()
        {
            string rlt =  this.FunctionName + ":[";
            for(int i = 0; i < this.FunctionMenus.Count; i++)
            {
                if(i == 0)
                {
                    rlt = rlt + this.FunctionMenus[i].ToString();
                }
                else
                {
                    rlt = rlt + "," + this.FunctionMenus[i].ToString();
                }
            }
            rlt = rlt + "]";
            return rlt;
        }
    }

    public class UserMenuViewModel
    {
        public List<FuncMenu> UserMenus { get; set; }

        public override string ToString()
        {
            string rlt = "{";
            for(int i = 0; i < this.UserMenus.Count; i++)
            {
                if(i == 0)
                {
                    rlt = rlt + this.UserMenus[i].ToString();
                }
                else
                {
                    rlt = rlt + "," + this.UserMenus[i].ToString();
                }
            }

            return rlt+"}";
        }
    }
}