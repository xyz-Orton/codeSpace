using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Code.MvcUI.Models;
using Code.Entities;
using Code.BLL;
using Code.Utilities;

namespace Code.MvcUI.Controllers
{
    public class UserManageController : Controller
    {
        UserData _userData = new UserData();//获取用户信息
        UserBLL _userBLL = new UserBLL();

        public ActionResult Index(int? page)
        {
            int size = 10;
            int index = page.HasValue ? page.Value : 1;
            int record = 0;

            tb_user searchEntity = new tb_user();
            List<tb_user> data = _userBLL.GetUserList(index, size, out record, searchEntity);

            var modelResult = new GoMusic.MvcPager.PagedList<tb_user>(data, index, size, record);
            ViewBag.Menu = Menu.UserList;
            return View(modelResult);
        }

        [HttpPost]
        public ActionResult Delete(int? ID)
        {
            if (!ID.HasValue || ID.Value <= 0)
            {
                return Json(new ResultMessage() { Success = false, Message = "未找到该ID,删除失败！" });
            }
            bool Success = true;
            string Message = "";
            try
            {
                _userBLL.DeleteUserInfo(ID.Value);
            }
            catch (Exception ex)
            {
                Success = false;
                Message = "操作异常，删除失败！";
            }
            return Json(new ResultMessage() { Success = Success, Message = Message });
        }
    }
}
