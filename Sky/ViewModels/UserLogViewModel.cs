using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sky.SkyUserData.Entities;

namespace Sky.ViewModels
{
    public class UserLogViewModel
    {
        public virtual string ClassName { get; set; }
        public virtual string FuncCode { get; set; }
        public virtual string ImportanceDegree { get; set; }
        public virtual string LogDatetime { get; set; }
        public virtual int LogID { get; set; }
        public virtual string OperationType { get; set; }
        public virtual string Remark { get; set; }
        public virtual string SystemCode { get; set; }
        public virtual int UserID { get; set; }

        public  UserLogViewModel(SkyUsrLog su)
        {
            this.ClassName = su.ClassName;
            this.FuncCode = su.FuncCode;
            this.ImportanceDegree = su.ImportanceDegree;
            this.LogDatetime = su.LogDatetime.ToString();
            this.LogID = su.LogID;
            this.OperationType = su.OperationType;
            this.Remark = su.Remark;
            this.SystemCode = su.SystemCode;
            this.UserID = su.UserID;
        }
    }
}