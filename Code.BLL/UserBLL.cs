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
        public tb_user GetUser(string loginName, string loginPwd)
        {
            tb_user user = null;

            using (var db = DataBaseHelper.GetDataBase())
            {
                // 命令参数形式
                Sql sql = Sql.Builder.Append("select * from tb_user ").Where(" LoginName=@0 and Password=@1",
                new object[] { 
                    loginName,loginPwd
                });

                user = db.FirstOrDefault<tb_user>(sql);
            }

            return user;
        }

        /// <summary>
        /// 获取用户信息(未删除)  用于登录
        /// </summary>
        public tb_user GetUserNoDelete(string loginName, string loginPwd)
        {
            tb_user user = null;

            using (var db = DataBaseHelper.GetDataBase())
            {
                // 命令参数形式
                Sql sql = Sql.Builder.Append("select * from tb_user ").Where(" LoginName=@0 and Password=@1 and (status=@2 or status=@3)",
                new object[] { 
                    loginName,loginPwd,(int)StatusEnum.Normal,(int)StatusEnum.Forbidden
                });

                user = db.FirstOrDefault<tb_user>(sql);
            }

            return user;
        }

        /// <summary>
        /// 获取用户列表
        /// </summary>
        public List<tb_user> GetUserList(int pageIndex, int pageSize, out int recordCount, tb_user searchEntity)
        {
            using (var db = DataBaseHelper.GetDataBase())
            {
                Sql sql = PetaPoco.Sql.Builder;
                sql.Append("select t1.ID,t1.OrgID,t1.LoginName,t1.UserName,t1.Sex,t1.Telephone,t1.Email,t1.Status,t2.OrgName");
                sql.Append("from tb_user t1");
                sql.Append("left join tb_organization t2 on t1.OrgID=t2.ID");

                sql.Where("t1.status!=@0", (int)StatusEnum.Deleted);
                if (!string.IsNullOrEmpty(searchEntity.UserName))
                {
                    sql.Where("t1.UserName like @0", "%" + searchEntity.UserName.Trim() + "%");
                }
                if (!string.IsNullOrEmpty(searchEntity.LoginName))
                {
                    sql.Where("t1.LoginName like @0", "%" + searchEntity.LoginName.Trim() + "%");
                }
                if (searchEntity.Sex.HasValue)
                {
                    sql.Where("t1.Sex=@0", searchEntity.Sex.Value);
                }
                if (searchEntity.Status.HasValue)
                {
                    sql.Where("t1.Status=@0", searchEntity.Status.Value);
                }
                if (searchEntity.OrgID.HasValue)
                {
                    sql.Where("t1.OrgID=@0", searchEntity.OrgID.Value);
                }

                sql.Append("order by t1.ID desc");

                var result = db.Page<tb_user>(pageIndex, pageSize, sql);
                recordCount = (int)result.TotalItems;
                return result.Items;
            }
        }

        /// <summary>
        /// 删除用户信息  物理删除
        /// </summary>
        public void DeleteUserInfo(int ID)
        {
            using (var db = DataBaseHelper.GetDataBase())
            {
                db.Delete<tb_user>(ID);
            }
        }
    }
}
