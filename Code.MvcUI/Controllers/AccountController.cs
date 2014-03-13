using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Code.MvcUI.Models;
using Code.Utilities;
using Code.BLL;
using Code.Entities;

namespace Code.MvcUI.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult LogOn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogOn(LoginModel model)
        {
            string message = string.Empty;
            bool flag = false;

            if (!ModelState.IsValid)
                return Json(new { Message = "请输入用户名或密码.", Success = flag });

            string loginName = model.UserName;
            string loginPwd = model.Password;

            //去掉空格
            if (!string.IsNullOrEmpty(loginName))
                loginName = HttpContext.Server.UrlDecode(loginName).Trim();

            //判断登录名是否有sql注入字符
            if (ValidationHelper.IsSafeSqlString(loginName))
                return Json(new { Message = "用户名输入有误，格式不正确.", Success = flag });

            //密码不能去掉空格
            if (!string.IsNullOrEmpty(loginPwd))
                loginPwd = HttpContext.Server.UrlDecode(loginPwd);

            //说明，如果正式发布，需要对密码进行加密
            UserBLL userBll = new UserBLL();
            try
            {
                tb_User user = userBll.GetUserNoDelete(loginName, Encrypt.MD5(loginPwd));

                if (user == null)
                    return Json(new { Message = "用户名或密码不正确.", Success = flag });

                if (user.Status == (int)UserStatusEnum.Forbidden)
                    return Json(new { Message = "该帐号已被禁用，不能登录.", Success = flag });
                else if (user.Status == (int)UserStatusEnum.Deleted)
                    return Json(new { Message = "该帐号已被删除，不能登录.", Success = flag });

                flag = true;

                UserData userData = new UserData()
                {
                    ID = user.ID,
                    LoginName = user.LoginName,
                    UserName = user.UserName,
                    UserType = user.UserType,
                    OrgId = user.OrgID
                };
                UserData.SaveUserBySession(userData, System.Web.HttpContext.Current.Session);
            }
            catch (Exception)
            {
                message = "程序出错啦！";
            }
            return Json(new { Message = message, Success = flag });

        }
    }
}
