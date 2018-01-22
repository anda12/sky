using System.Web;
using System.Web.Optimization;

namespace Sky
{
    public class BundleConfig
    {
        // 有关绑定的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/easyui").Include(
            "~/Scripts/jquery.js",
            "~/Scripts/jquery-easyui.js",
            "~/Scripts/locale/easyui-lang-zh_CN.js",
            "~/Scripts/sky/menus4.js",
            "~/Scripts/easyuiExt.js",
            "~/Scripts/sky/topshow.js",
            "~/Scripts/JScriptCommon.js"
            ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/themes/default/easyui.css",
                "~/Content/themes/icon.css",
                "~/Content/sky/easyuiExt.css",
                "~/Content/demo.css"
                ));
            bundles.Add(new StyleBundle("~/Content/Login/css").Include(
                "~/Content/bootstrap.min.css", 
                "~/Content/sky/signin.css"
                ));

            bundles.Add(new ScriptBundle("~/bundles/jscookie").Include(
               "~/Scripts/jquery-cookie.js"));


            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
               "~/Scripts/jquery-validate-unobtrusive.js",
               "~/Scripts/jquery-validate*"));

            /*
            bundles.Add(new ScriptBundle("~/Scripts/jquery").Include(
                        "~/Scripts/bootstrap.js",
                        "~/Scripts/easyloader.js",
                        "~/Scripts/jquery.easyui-{version}.js",
                        "~/Scripts/jquery.validate-vsdoc.js",
                        "~/Scripts/jquery.validate.js",
                        "~/Scripts/jquery.validate.unobtrusive.js",
                        "~/Scripts/modernizr-{version}.js",
                        "~/Scripts/respond.js",
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery-{version}.intellisense.js",
                        "~/Scripts/locale/easyui-lang-zh_CN.js"
                        ));
            bundles.Add(new StyleBundle("~/Content/css").Include(
                        "~/Content/themes/color.css",
                        "~/Content/themes/icon.css",
                        "~/Content/themes/mobile.css",
                        "~/Content/themes/default/accordion.css",
                        "~/Content/themes/default/calendar.css",
                        "~/Content/themes/default/combo.css",
                        "~/Content/themes/default/combobox.css",
                        "~/Content/themes/default/datagrid.css",
                        "~/Content/themes/default/datalist.css",
                        "~/Content/themes/default/datebox.css",
                        "~/Content/themes/default/dialog.css",
                        "~/Content/themes/default/easyui.css",
                        "~/Content/themes/default/filebox.css",
                        "~/Content/themes/default/layout.css",
                        "~/Content/themes/default/linkbutton.css",
                        "~/Content/themes/default/menu.css",
                        "~/Content/themes/default/menubutton.css",
                        "~/Content/themes/default/messager.css",
                        "~/Content/themes/default/numberbox.css",
                        "~/Content/themes/default/pagination.css",
                        "~/Content/themes/default/panel.css",
                        "~/Content/themes/default/progressbar.css",
                        "~/Content/themes/default/propertygrid.css",
                        "~/Content/themes/default/searchbox.css",
                        "~/Content/themes/default/slider.css",
                        "~/Content/themes/default/spinner.css",
                        "~/Content/themes/default/splitbutton.css",
                        "~/Content/themes/default/splitbutton.css",
                        "~/Content/themes/default/tabs.css",
                        "~/Content/themes/default/textbox.css",
                        "~/Content/themes/default/tooltip.css",
                        "~/Content/themes/default/tree.css",
                        "~/Content/themes/default/validatebox.css",
                        "~/Content/themes/default/window.css",
                        "~/Content/demo.css",
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
             * */
        }
    }
}
