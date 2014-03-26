using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Code.Entities;
using Code_DAL;
using PetaPoco;

namespace Code.BLL
{
    public class OrganizationBLL
    {
        /// <summary>
        /// 获取组织结构列表
        /// </summary>
        /// <returns></returns>
        public List<tb_organization> GetOrganizationList()
        {
            List<tb_organization> result = new List<tb_organization>();
            using (var db = DataBaseHelper.GetDataBase())
            {
                Sql sql = PetaPoco.Sql.Builder
                .Append(" SELECT ID,OrgName,ParentID,IsLast")
                .Append(" FROM tb_Organization");
                sql.Where(" Status=1");
                sql.OrderBy(" ID DESC");

                result = db.Fetch<tb_organization>(sql);
            }
            return result;
        }
    }
}
