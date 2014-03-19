using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Code.MvcUI.Models
{
    public class SystemMenu
    {
        public Menu Item { get; set; }
    }

    public enum Menu
    {
        /// <summary>
        /// 无
        /// </summary>
        None,

        Home,

        /// <summary>
        /// 用户管理
        /// </summary>
        UserList
    }
}