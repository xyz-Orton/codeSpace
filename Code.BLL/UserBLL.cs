using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Code.Entities;
using Code_DAL;
using PetaPoco;

namespace Code.BLL
{
    public class UserBLL
    {
        /// <summary>
        /// 获取用户信息
        /// </summary>
        public tb_User GetUser(string loginName, string loginPwd)
        {
            tb_User user = null;

            using (var db = DataBaseHelper.GetDataBase())
            {
                // 命令参数形式
                Sql sql = Sql.Builder.Append("select * from tb_user ").Where(" LoginName=@0 and Password=@1",
                new object[] { 
                    loginName,loginPwd
                });

                user = db.FirstOrDefault<tb_User>(sql);
            }

            return user;
        }

        /// <summary>
        /// 获取用户信息(未删除)  用于登录
        /// </summary>
        public tb_User GetUserNoDelete(string loginName, string loginPwd)
        {
            tb_User user = null;

            using (var db = DataBaseHelper.GetDataBase())
            {
                // 命令参数形式
                Sql sql = Sql.Builder.Append("select * from tb_user ").Where(" LoginName=@0 and Password=@1 and (status=@2 or status=@3)",
                new object[] { 
                    loginName,loginPwd,(int)UserStatusEnum.Normal,(int)UserStatusEnum.Forbidden
                });

                user = db.FirstOrDefault<tb_User>(sql);
            }

            return user;
        }
    }
}
