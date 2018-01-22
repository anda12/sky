using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Sky.Common
{
    public class ModeStateError
    {
        public static string GetModeStateError(ModelStateDictionary ModelState)
        {
            //List<string> sb = new List<string>();
            string rlt = "\r\n";
            //获取所有错误的Key
            List<string> Keys = ModelState.Keys.ToList();
            //获取每一个key对应的ModelStateDictionary
            foreach (var key in Keys)
            {
                var errors = ModelState[key].Errors.ToList();
                //将错误描述添加到sb中
                foreach (var error in errors)
                {
                    //sb.Add(error.ErrorMessage);
                    rlt += error.ErrorMessage + "\r\n";
                }
            }
            return rlt;
        }
    }
}