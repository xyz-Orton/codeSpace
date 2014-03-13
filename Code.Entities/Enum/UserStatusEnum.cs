using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Code.Entities
{
    /// <summary>
    /// 用户状态
    /// </summary>
    public enum UserStatusEnum
    {
        /// <summary>
        /// 正常状态
        /// </summary>
        Normal = 1,

        /// <summary>
        /// 禁用
        /// </summary>
        Forbidden = 2,

        /// <summary>
        /// 逻辑删除
        /// </summary>
        Deleted = 3
    }
}
