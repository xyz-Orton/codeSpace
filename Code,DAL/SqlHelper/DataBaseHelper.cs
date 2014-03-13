using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using PetaPoco;

namespace Code_DAL
{
    public class DataBaseHelper
    {
        private static string ConnectionStringName
        {
            get
            {
                ConnectionStringSettings connStr = ConfigurationManager.ConnectionStrings["DefaultConnection"];

                if (connStr != null)
                {
                    return connStr.Name;
                }
                return "";
            }
        }

        public static Database GetDataBase()
        {
            return new Database(ConnectionStringName);
        }
    }
}
