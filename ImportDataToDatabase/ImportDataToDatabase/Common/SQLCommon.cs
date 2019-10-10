using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using ImportDataToDatabase.Log;

namespace ImportDataToDatabase.Common
{
    public class SQLCommon
    {
        public static SqlConnection ConnectionDB;
        public static StringBuilder ERPSOFT = new StringBuilder();
        public static StringBuilder ERP_TEST = new StringBuilder();
        public static StringBuilder SFT_TEST= new StringBuilder();
        public static StringBuilder ERP_TL = new StringBuilder();
        public static StringBuilder SFT_TL = new StringBuilder();
        public SQLCommon()
        {
            ERPSOFT.Append(@"Data Source=172.16.0.12;Initial Catalog = ERPSOFT;
                                      Persist Security Info = True;User ID=ERPUSER;Password=12345");
            //   ConnectionString11.Append(@"Data Source=172.16.0.11;Initial Catalog =TEST06;
            //                             Persist Security Info = True;User ID=ERPUSER;Password=12345");
            ERP_TEST.Append(@"Data Source=172.16.0.11;Initial Catalog = TEST06;
                                      Persist Security Info = True;User ID=soft;Password=techlink@!@#");
            ERP_TL.Append(@"Data Source=172.16.0.11;Initial Catalog = TECHLINK;
                                      Persist Security Info = True;User ID=soft;Password=techlink@!@#");
            SFT_TEST.Append(@"Data Source=172.16.0.11;Initial Catalog = SFT_TEST06;
                                      Persist Security Info = True;User ID=soft;Password=techlink@!@#");
            SFT_TL.Append(@"Data Source=172.16.0.11;Initial Catalog = SFT_TECHLINK;
                                      Persist Security Info = True;User ID=soft;Password=techlink@!@#");

        }
        public string sqlExecuteScalarString(string sql, StringBuilder stringBuilder)
        {
            String outstring;


            try
            {
                ConnectionDB = new SqlConnection(stringBuilder.ToString());
                ConnectionDB.Open();
                SqlCommand command = new SqlCommand(sql, ConnectionDB);
                outstring = command.ExecuteScalar().ToString();
                return outstring;
            }
            catch (Exception ex)
            {
              Logfile.Output(StatusLog.Error, "sqlExecuteScalarString(string sql, StringBuilder stringBuilder)",ex.Message);
                return String.Empty;
            }
           // ConnectionDB.Close();

        }
        public void ERPSQLExcuteNonQuery(string sql, StringBuilder stringBuilder)
        {
            try
            {
                ConnectionDB = new SqlConnection(stringBuilder.ToString());
                ConnectionDB.Open();
                SqlCommand command = new SqlCommand(sql, ConnectionDB);
                command.ExecuteNonQuery();
                ConnectionDB.Close();
            }
            catch (Exception ex)
            {

                Logfile.Output(StatusLog.Error, "ERPSQLExcuteNonQuery(string sql, StringBuilder stringBuilder)", ex.Message);
            }
          
        }
      
        public void sqlDataAdapterFillDatatable(string sql, ref DataTable dt, StringBuilder stringBuilder)
        {
            try
            {
                ConnectionDB = new SqlConnection(stringBuilder.ToString());
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter adapter = new SqlDataAdapter();
                {
                    cmd.CommandText = sql;
                    cmd.Connection = ConnectionDB;
                    adapter.SelectCommand = cmd;
                    adapter.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logfile.Output(StatusLog.Error, "ERPsqlDataAdapterFillDatatable(string sql, ref DataTable dt, StringBuilder stringBuilder)", ex.Message);
               

            }
        }
        public void InsertCSVToDatabase(ref DataTable dt, StringBuilder stringBuilder)
        {
            try
            {
                ConnectionDB = new SqlConnection(stringBuilder.ToString());
                ConnectionDB.Open();
                using (var adapte = new SqlDataAdapter("select * from m_ERPMQC", ConnectionDB))
                using (var builder = new SqlCommandBuilder(adapte))
                {
                    adapte.InsertCommand = builder.GetInsertCommand();
                    adapte.Update(dt);
                }
                ConnectionDB.Close();
            }
            catch (Exception ex)
            {

                Logfile.Output(StatusLog.Error, "InsertCSVToDatabase(ref DataTable dt, StringBuilder stringBuilder)", ex.Message);
            }
        
        }

        
    }
}
