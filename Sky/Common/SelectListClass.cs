using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Sky.Common
{
    public class GloabClass
    {
        /// <summary>
        /// 是否添加选择行
        /// </summary>
        public static bool IsAddSelect = true;
        /// <summary>
        /// //选择行的文本
        /// </summary>
        public static string DefaultSelectText = "——请选择——";
        /// <summary>
        /// //默认选择的值
        /// </summary>
        public static string DefaultSelectValue = "";
        /// <summary>
        /// 默认选择的整数值
        /// </summary>
        public static decimal? DefaultIntValue = null;
    }
    public class SelectListClass : GloabClass
    {
        /// <summary>
        /// 获取性别
        /// </summary>
        /// <returns></returns>
        public static SelectList GetGenders(bool isSelected)
        {
            List<SelectListItem> lists = new List<SelectListItem>();
            if (isSelected == true)
            {
                if (IsAddSelect)
                {
                    lists.Add(new SelectListItem { Text = DefaultSelectValue, Value = DefaultSelectText });
                }
            }
            lists.Add(new SelectListItem { Text = "1", Value = "男" });
            lists.Add(new SelectListItem { Text = "2", Value = "女" });
            return new SelectList(lists, "Text", "Value");
        }
        /// <summary>
        /// 获取所有状态
        /// </summary>
        /// <returns></returns>
        public static SelectList GetStatus(bool isSelected)
        {
            List<SelectListItem> lists = new List<SelectListItem>();
            if (isSelected == true)
            {
                if (IsAddSelect)
                {
                    lists.Add(new SelectListItem { Text = DefaultSelectValue, Value = DefaultSelectText });
                }
            }
            lists.Add(new SelectListItem { Text = "1", Value = "激活" });
            lists.Add(new SelectListItem { Text = "2", Value = "未激活" });
            return new SelectList(lists, "Text", "Value");
        }
        /// <summary>
        /// 获取所有状态
        /// </summary>
        /// <returns></returns>
        public static SelectList GetYesOrNo(bool isSelected)
        {
            List<SelectListItem> lists = new List<SelectListItem>();
            if (isSelected == true)
            {
                if (IsAddSelect)
                {
                    lists.Add(new SelectListItem { Text = DefaultSelectValue, Value = DefaultSelectText });
                }
            }
            lists.Add(new SelectListItem { Text = "1", Value = "是" });
            lists.Add(new SelectListItem { Text = "0", Value = "否" });
            return new SelectList(lists, "Text", "Value");
        }
    }
}