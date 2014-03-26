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
        OrganizationBLL _organizationBLL = new OrganizationBLL();

        public ActionResult Index(int? page)
        {
            int size = 10;
            int index = page.HasValue ? page.Value : 1;
            int record = 0;

            tb_user searchEntity = new tb_user();
            searchEntity.UserName = Request["txtUserName"];
            int status = ConverterHelper.ObjectToInt(Request["selStatus"]);
            if (status > 0)
            {
                searchEntity.Status = (byte)status;
            }
            List<tb_user> data = _userBLL.GetUserList(index, size, out record, searchEntity);

            var modelResult = new GoMusic.MvcPager.PagedList<tb_user>(data, index, size, record);
            ViewBag.Menu = Menu.UserList;
            ViewBag.sUserName = Request["txtUserName"];
            ViewBag.sStatus = Request["selStatus"];
            return View(modelResult);
        }

        /// <summary>
        /// 删除用户
        /// </summary>
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

        /// <summary>
        /// 获取全部组织架构
        /// </summary>
        /// <returns></returns>
        public ActionResult GetOrganizationList()
        {
            List<OrganizationResult> _treeData = new List<OrganizationResult>();
            List<tb_organization> List = _organizationBLL.GetOrganizationList();
            foreach (tb_organization et in List)
            {
                OrganizationResult _td = new OrganizationResult();

                _td.id = et.ID;
                _td.fullName = et.OrgName;
                _td.name = StringExtensions.Left(et.OrgName, 9);
                _td.pId = (et.ParentId == null) ? -1 : et.ParentId.Value;
                //----------------------------------------
                _td.isClass = et.IsLast == 1 ? true : false;
                _td.isParent = et.IsLast == 1 ? false : true;
                _td.open = et.IsLast == 1 ? false : true;
                _treeData.Add(_td);
            }

            return Json(_treeData);
        }
    }
}
