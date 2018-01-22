using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sky.Common;
using Sky.ViewModels;

namespace Sky.Controllers
{
    public class LoginController : BaseController
    {
        // GET: Login
        public ActionResult Index()
        {
            CheckCookieInfo();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]//防伪造令牌来避免CSRF攻击
        public ActionResult Index(LoginOnViewModel model)
        {
            #region 验证码验证

            if (Session["ValidateCode"] != null && model.ValidateCode != null && model.ValidateCode.ToLower() != Session["ValidateCode"].ToString())
            {
                ModelState.AddModelError("Error_PersonLogin", "验证码错误！");
                return View();
            }
            Session["ValidateCode"] = null;
            #endregion

            if (ModelState.IsValid)
            {
                AccountViewModel person = ValidateUser(model.UsID, xEncrypt.EncryptText(model.UsPassword));
                if (person != null) //登录成功
                {

                    string sessionId = Guid.NewGuid().ToString();//作为Memcache的key
                    try
                    {
                        MemcacheHelper.Set(sessionId, Common.SerializerHelper.SerializeToString(person), DateTime.Now.AddMinutes(20));//使用Memcache代替Session解决数据在不同Web服务器之间共享的问题。
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                    Response.Cookies["sessionId"].Value = sessionId;//将Memcache的key以cookie的形式返回到浏览器端的内存中，当用户再次请求其它的页面请求报文中会以Cookie将该值再次发送服务端。

                    if (model.RememberMe)
                    {
                        HttpCookie ckUid = new HttpCookie("ckUid", model.UsID);
                        HttpCookie ckPwd = new HttpCookie("ckPwd", xEncrypt.EncryptText(model.UsPassword));
                        ckUid.Expires = DateTime.Now.AddDays(3);
                        ckPwd.Expires = DateTime.Now.AddDays(3);
                        Response.Cookies["sessionId"].Expires = DateTime.Now.AddDays(3);
                        Response.Cookies.Add(ckUid);
                        Response.Cookies.Add(ckPwd);
                    }
                    return RedirectToAction("Index", "Home");
                }
            }
            ModelState.AddModelError("Error_PersonLogin", "用户名或者密码出错。");
            return View();
        }

        public ActionResult ShowValidateCode()
        {
            ValidateCode validateCode = new ValidateCode();
            string code = validateCode.CreateValidateCode(4);
            Session["ValidateCode"] = code;
            byte[] buffer = validateCode.CreateValidateGraphic(code);
            return File(buffer, "image/jpeg");
        }

        public ActionResult LogOff()
        {
            if (Request.Cookies["sessionId"] != null)
            {
                string key = Request.Cookies["sessionId"].Value;
                Common.MemcacheHelper.Delete(key);
                Response.Cookies["ckUid"].Expires = DateTime.Now.AddDays(-1);
                Response.Cookies["ckPwd"].Expires = DateTime.Now.AddDays(-1);
            }
            return RedirectToAction("Index", "Login");
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <returns></returns>
        public ActionResult ChangePassword()
        {
            AccountViewModel av = GetCurrentAccount();
            if (av != null)
            {
                string currentPerson = av.UsID;
                ViewBag.PersonNamea = currentPerson;
                if (string.IsNullOrWhiteSpace(currentPerson))
                {
                    ModelState.AddModelError("Error_PwdModify", "对不起，请重新登陆");
                }
            }
            else
            {
                ModelState.AddModelError("Error_PwdModify", "对不起，请重新登陆");
            }
            return View();
        }

        
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="model">修改密码的模型</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ChangePassword(EditPassViewModel model)
        {
            AccountViewModel av = GetCurrentAccount();
            if (av==null)
            {
                ModelState.AddModelError("Error_PwdModify", "对不起，请重新登陆");
                return View();
            }
            else
            {
                 if(string.IsNullOrWhiteSpace(av.UsID))
                 {
                     ModelState.AddModelError("Error_PwdModify", "对不起，请重新登陆");
                     return View();
                 }
            }
            if (ModelState.IsValid)
            {
                if (null != (ValidateUser(av.UsID, xEncrypt.EncryptText(model.OldPassword))))
                {
                    if (ChangePass(av.UsID, xEncrypt.EncryptText(model.OldPassword), xEncrypt.EncryptText(model.NewPassword)) == 1)
                    {
                        ModelState.AddModelError("Error_PwdModify", "修改密码成功");
                        return View();
                    }
                }
            }
            ModelState.AddModelError("Error_PwdModify", "修改密码不成功，请核实数据");
            return View();
        }
        

        private void CheckCookieInfo()
        {
            if (Request.Cookies["ckUid"] != null && Request.Cookies["ckPwd"] != null)
            {
                string userName = Request.Cookies["ckUid"].Value;
                string userPwd = Request.Cookies["ckPwd"].Value;
                //判断Cookie中存储的用户密码和用户名是否正确.
                AccountViewModel person = ValidateUser(userName, userPwd);
                if (person != null)
                {
                    string sessionId = Guid.NewGuid().ToString();//作为Memcache的key
                    var account = person.ToString();
                    Common.MemcacheHelper.Set(sessionId, Common.SerializerHelper.SerializeToString(account), DateTime.Now.AddMinutes(20));//使用Memcache代替Session解决数据在不同Web服务器之间共享的问题。
                    Response.Cookies["sessionId"].Value = sessionId;//将Memcache的key以cookie的形式返回到浏览器端的内存中，当用户再次请求其它的页面请求报文中会以Cookie将该值再次发送服务端。
                    //Response.Redirect("/Home/Index"); 尽量不要用这样的写法
                    RedirectToAction("Index", "Home");
                }
                else
                {
                    //如果说帐号秘密是错误的，就没必要再把登录用户名和密码存在cookies中了
                    Response.Cookies["ckUid"].Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies["ckPwd"].Expires = DateTime.Now.AddDays(-1);
                }
            }
        }
    }
}