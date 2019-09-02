﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace WindowsFormsApplication1
{
    class DBUtils
    {

        public static SqlConnection GetDBConnection()
        {
            //Data Source=LONG;Initial Catalog=TEST;Integrated Security=True
            string datasource = "Long";
            string database = "TEST";
            string username = "SQLUSER";
            string password = "12345";

            //string datasource = "172.16.0.12";
            //string database = "ERPSOFT";
            //string username = "ERPUSER";
            //string password = "12345";



            return DBSQLServerUtils.GetDBConnection(datasource, database, username, password);
        }
        public static SqlConnection GetERPDBConnection()
        {
            //Data Source=LONG;Initial Catalog=TEST;Integrated Security=True
            //string datasource = "172.16.0.11";
            //string database = "TECHLINK";
            //string username = "soft";
            //string password = "techlink@!@#";

            string datasource = "Long";
            string database = "TECHLINK";
            string username = "SQLUSER";
            string password = "12345";


            return DBSQLServerUtils.GetERPDBConnection(datasource, database, username, password);
        }

    }
}
