using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.SessionState;
using Code.Utilities;

namespace Code.MvcUI.Models
{
    public class UserData
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public int ID { set; get; }

        /// <summary>
        /// 登录名
        /// </summary>
        public string LoginName { get; set; }

        /// <summary>
        /// 真实姓名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 用户类型
        /// </summary>
        public int? UserType { get; set; }

        /// <summary>
        /// 组织架构Id
        /// </summary>
        public int? OrgId { get; set; }


        public UserData()
        {
        }

        public UserData(int id, string loginName, string userName, int UserType, int orgId)
        {
            this.ID = id;
            this.LoginName = loginName;
            this.UserName = userName;
            this.UserType = UserType;
            this.OrgId = orgId;
        }

        public static void SaveUserBySession(UserData user, HttpSessionState session)
        {
            if (user == null) return;
            if (session == null) return;
            if (user.ID > 0) session["ID"] = user.ID;
            if (!string.IsNullOrEmpty(user.LoginName)) session["LoginName"] = user.LoginName;
            if (!string.IsNullOrEmpty(user.UserName)) session["UserName"] = user.UserName;
            if (user.UserType != null && user.UserType.HasValue) session["UserType"] = user.UserType;
            if (null != user.OrgId && user.OrgId.HasValue) session["OrgID"] = user.OrgId;
        }

        public static UserData GetUserBySession(HttpSessionState session)
        {
            if (session == null) return null;

            UserData user = new UserData();
            if (session["ID"] != null)
                user.ID = Convert.ToInt32(session["ID"]);
            if (session["UserId"] != null)
                user.LoginName = session["LoginName"].ToString();
            if (session["UserName"] != null)
                user.UserName = session["UserName"].ToString();
            if (session["UserType"] != null)
                user.UserType = ConverterHelper.ObjectToInt(session["UserType"]);
            if (session["OrgId"] != null)
                user.OrgId = Convert.ToInt32(session["OrgId"]);

            if (user.ID > 0)
                return user;

            return null;
        }

        public static UserData InitData(HttpSessionState session = null)
        {
            UserData user = null;
            if (session == null)
                session = HttpContext.Current.Session;
            user = GetUserBySession(session);

            return user;
        }
    }
}