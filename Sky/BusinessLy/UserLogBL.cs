using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sky.ViewModels;
using Sky.Common;
using Sky.SkyUserData.Entities;
using Sky.SkyUserData.DA;
using Sky.Model;

namespace Sky.BusinessLy
{
    public class UserLogBL
    {
        static SkyUsrLogData suld = new SkyUsrLogData();
        public static int RecordUserLog(UserLogModel lm)
        {
            SkyUsrLog log = new SkyUsrLog();

            log.ClassName = lm.ClassName;
            log.FuncCode = lm.FuncCode;
            log.ImportanceDegree = lm.ImportanceDegree;
            log.LogDatetime = DateTime.Now;
            log.OperationType = lm.OperationType;
            log.Remark = lm.Remark;
            log.SystemCode = lm.SystemCode;
            log.UserID = lm.UserID;

            if (suld.Insert(log))
            {
                return 0;
            }
            else
            {
                return -1;
            }
        }

        public List<UserLogViewModel> GetUserLogPaging(int pageIndex, int pageSize, out int totalCount, DateTime start, DateTime end, int userid, string opertype, string syscode)
        {
            IList<SkyUsrLog> sull = suld.GetListPage(pageIndex, pageSize, out totalCount, start, end, userid, opertype, syscode);

            List<UserLogViewModel> rl = new List<UserLogViewModel>();
            foreach(SkyUsrLog su in sull)
            {
                UserLogViewModel uv = new UserLogViewModel(su);
                rl.Add(uv);
            }

            return rl;
        }
    }
}