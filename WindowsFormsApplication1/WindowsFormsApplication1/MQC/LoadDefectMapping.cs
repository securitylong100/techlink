using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace WindowsFormsApplication1.MQC
{
    class LoadDefectMapping
    {
        public List<NGItemsMapping> listNGMapping(string Dept)
        {
            List<NGItemsMapping> nGItemsMappings = new List<NGItemsMapping>();
            StringBuilder sql = new StringBuilder();
            sql.Append("select distinct modelcode, processcode, processname, itemcode, itemname ");
            sql.Append("from m_process ");
            sql.Append("where 1=1 ");
            sql.Append("and modelcode = '" + Dept+ "' ");
            sqlCON sql12 = new sqlCON();
            DataTable dt = new DataTable();
            sql12.sqlDataAdapterFillDatatable(sql.ToString(), ref dt);
            nGItemsMappings = (from DataRow dr in dt.Rows
                                select new NGItemsMapping()
                                {
                                    Department = dr["modelcode"].ToString(),
                                    NGCode_Process = dr["processcode"].ToString(),
                                    NGCodeName_Process = dr["processname"].ToString(),
                                    NGCode_SFT = dr["itemcode"].ToString(),
                                    NGCodeName_SFT = dr["itemname"].ToString()

                                }).ToList();
            return nGItemsMappings;
        }
    }
}
