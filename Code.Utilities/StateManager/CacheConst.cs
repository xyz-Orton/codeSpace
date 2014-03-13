using System;
using System.Collections.Generic;
using System.Text;

namespace Code.Utilities
{
    /// <summary>
    /// Cache Names
    /// </summary>
    public class CacheConst
    {
        /// <summary>
        /// 用于缓存所有权限和菜单
        /// </summary>
        public static readonly string BASE_PERMISSION = "BASE_PERMISSION";

        /// <summary>
        /// 用于缓存所有用户角色信息
        /// </summary>
        public static readonly string BASE_USERALLROLES = "BASE_USERALLROLES";

        /// <summary>
        /// 用于缓存所有的 角色权限关系表
        /// </summary>
        public static readonly string BASE_RELATION = "BASE_RolePermissionRelation";
    }
}
