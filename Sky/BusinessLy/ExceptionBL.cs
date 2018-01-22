using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web;
using Sky.SkyUserData.Entities;
using Sky.SkyUserData.DA;

namespace Sky.BusinessLy
{
    public class ExceptionBL
    {
        private SkyExceptionData _exb;

        public ExceptionBL()
        {
            _exb = new SkyExceptionData();
        }

        public IList<SkyException> GetException(Expression<Func<SkyException, bool>> where)
        {
            return _exb.GetExceptionList(where);
        }
    }
}