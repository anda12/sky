using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sky.Model
{
    public class NHPara
    {
        public string ColumeName { get; set; }
        public string type { get; set; }   //str, int, datetime
        public string filter { get; set; }  //eq, ge, le, contain, between
        public object value { get; set; }
    }

    public class NHPaging
    {
        public int rows { get; set; }
        public int page { get; set; }
        public int start { get; set; }
    }
}