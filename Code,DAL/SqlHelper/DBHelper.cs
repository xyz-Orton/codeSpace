using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace Code.DAL
{
    /// <summary>
    /// SqlHelper基类
    /// 有待完善
    /// </summary>
    public class DBHelper
    {
        //数据库连接字符串(web.config来配置)，可以动态更改connectionString支持多数据库.	
        private string _connectionString;
        public string connectionString
        {
            get
            {
                return _connectionString;
            }
            set
            {
                _connectionString = value;
            }
        }
        public DBHelper(string conn)
        {
            connectionString = conn;
        }
        #region 转换
        /// <summary>
        /// 获得一个Model对象实例
        /// </summary>
        public T GetModel<T>(IDataReader dr) where T : new()
        {
            T model = new T();
            PropertyInfo[] pis =
                typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public);
            int iIndex;
            foreach (PropertyInfo pi in pis)
            {
                try
                {
                    iIndex = dr.GetOrdinal(pi.Name);
                }
                catch (IndexOutOfRangeException)
                {
                    continue;
                }
                if (dr.IsDBNull(iIndex))
                    continue;
                dr.GetValue(iIndex);
                pi.SetValue(model, dr.GetValue(iIndex), null);
            }
            return model;
        }

        /// <summary>
        /// 获得List集合
        /// </summary>
        /// <param name="dr">将DataReader里的实体转到List</param>
        public List<T> GetList<T>(IDataReader dr) where T : new()
        {
            List<T> list = new List<T>();
            while (dr.Read())
            {
                list.Add(GetModel<T>(dr));
            }
            return list;
        }

        #endregion
    }
}
