using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Code.MvcUI.Models;
using Code.Entities;
using Code.BLL;

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

    }
}
