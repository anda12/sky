using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sky.Model
{
    public class UserLogModel
    {
        public virtual int LogID { get; set; }
        public virtual int UserID { get; set; }
        public virtual string FuncCode { get; set; }
        public virtual string OperationType { get; set; }
        public virtual string Remark { get; set; }
        public virtual string SystemCode { get; set; }
        public virtual string ClassName { get; set; }
        public virtual string ImportanceDegree { get; set; }
    }
}