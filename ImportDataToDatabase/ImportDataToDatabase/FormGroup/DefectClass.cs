using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using ImportDataToDatabase.Common;
using ImportDataToDatabase.Log;

namespace ImportDataToDatabase.FormGroup
{
    class DefectClass
    {
        public List<NGItemsMapping> listNGMapping(string Dept)
        {
            List<NGItemsMapping> nGItemsMappings = new List<NGItemsMapping>();
            StringBuilder sql = new StringBuilder();
            sql.Append("select distinct modelcode, processcode, processname, itemcode, itemname ");
            sql.Append("from m_process ");
            sql.Append("where 1=1 ");
            sql.Append("and modelcode = '" + Dept + "' ");
            SQLCommon data = new SQLCommon();
            DataTable dt = new DataTable();
            data.sqlDataAdapterFillDatatable(sql.ToString(), ref dt,SQLCommon.ERPSOFT);
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
        public string GetCodeNGFrom_process(string Dept,string PLCNG)
        {
            string Str = "";
            StringBuilder sql = new StringBuilder();
            sql.Append("select  itemcode ");
            sql.Append("from m_process ");
            sql.Append("where 1=1 ");
            sql.Append("and modelcode = '" + Dept + "' ");
            sql.Append("and processcode = '" + PLCNG + "' ");
            SQLCommon data = new SQLCommon();
          Str =   data.sqlExecuteScalarString(sql.ToString(), SQLCommon.ERPSOFT);
            return Str;
        }
        public bool InsertDefect2SFT_OP_EXCEPT(string _ID, int _SEQUENCE, int _EXCEPTQTY,string _OPID,string _USERID,
 string _EXCEPTTYPE,string _EXCEPTGROUP,string _EXCEPTREASON, string _ERP_OPSEQ ,string _ERP_OPID, int _PKQTY, int _PKQTYPER)
        {
            try
            {
               
                
            StringBuilder sql = new StringBuilder();
            sql.Append(" insert into SFT_OP_EXCEPT ");
            sql.Append(@"(
ID,
SEQUENCE,
EXCEPTQTY,
OPID,
EQID,
USERID,
ROUTESEQUENCE,
STEPSEQUENCE,
ALTSTEPSEQUENCE,
OPSEQUENCE,
EXCEPTTYPE,
EXCEPTGROUP,
EXCEPTREASON,
OPERID,
WSID,
TRANSORDERID,
ERP_OPSEQ,
ERP_OPID,
ERP_WSID,
PKQTY,
PKQTYPER,
PKUNIT,
UNIID,
OE001,
OE002,
OE003,
OE004,
OE005,
OE006 
 )");
            sql.Append(" values ( ");
                sql.Append("'"+ _ID + "',");
                sql.Append("" + _SEQUENCE + ",");
                sql.Append("" + _EXCEPTQTY + ",");
                sql.Append("'" + _OPID + "',");
                sql.Append("'" + "" + "',");
                sql.Append("'" + _USERID + "',");
                sql.Append("NULL,NULL,NULL,NULL,");
                sql.Append("'" + _EXCEPTTYPE + "',");
                sql.Append("'" + _EXCEPTGROUP + "',");
                sql.Append("'" + _EXCEPTREASON + "',");
                sql.Append("'" + _USERID + "',");
                sql.Append("NULL,NULL,");
                sql.Append("'" + _ERP_OPSEQ + "',");
                sql.Append("'" + _ERP_OPID + "',");
                sql.Append("'" + _ERP_OPID + "',");
                sql.Append("" + _PKQTY + ",");
                sql.Append("" + _PKQTYPER + ",");
                sql.Append("'" + "" + "',");
                sql.Append("NULL,");
                sql.Append("'" + "" + "',");
                sql.Append("NULL,NULL,NULL,NULL,NULL");
                sql.Append(" )");

                string sql2 = sql.ToString();

                SQLCommon sQLCommon = new SQLCommon();
            sQLCommon.ERPSQLExcuteNonQuery(sql.ToString(), SQLCommon.SFT_TEST);
            }
            catch (Exception ex)
            {

                Logfile.Output(StatusLog.Error, " InsertDefect2SFT_OP_EXCEPT()", ex.Message);
                return false;

            }
            return true;
        }
       public string [] GetDefectItemFromSFT (string defectCode)
        {
            string[] strDefectItem = new string[2];
            try
            {
             
                StringBuilder sql = new StringBuilder();
                sql.Append(@"
select l.CIL003,s.CIS003 from dbo.SFT_COLLECTITEM_SUBLINE s 
inner join dbo.SFT_COLLECTITEM_LINE  l on s.CIS001 = l.CIL002
where 1=1
 ");
                sql.Append("and s.CIS002  = '" + defectCode + "' ");
                SQLCommon sQLCommon = new SQLCommon();
                DataTable dt = new DataTable();
                sQLCommon.sqlDataAdapterFillDatatable(sql.ToString(), ref dt, SQLCommon.SFT_TEST);
                strDefectItem = dt.Rows[0].ItemArray.Select(x => x.ToString()).ToArray();
            }
            catch (Exception ex)
            {

                Logfile.Output(StatusLog.Error, "GetDefectItemFromSFT (string defectCode)", ex.Message);
            }
            return strDefectItem;
        }

        public bool InsertToSFT_OP_EXCEPT(string codeSX,string PLCNG, int QTYNG)

        {
            try
            {

            
            string SFTNGCode = GetCodeNGFrom_process("B01", PLCNG);
            string[] DefectSFTArray = GetDefectItemFromSFT(SFTNGCode);
            int _sequence = GetSequence_SFT_OP_EXCEPT(codeSX, "defect", DefectSFTArray[0], DefectSFTArray[1],  "10", "B01", "B01");
            var insert = InsertDefect2SFT_OP_EXCEPT(codeSX, _sequence, QTYNG, "B01---B01", "ERP", "defect", DefectSFTArray[0],
             DefectSFTArray[1], "10", "B01", 0, 0);
            }
            catch (Exception ex)
            {

                Logfile.Output(StatusLog.Error, "InsertToSFT_OP_EXCEPT(string codeSX,string PLCNG, int QTYNG)", ex.Message);
                return false;
            }
            return true;
        }
        public int GetSequence_SFT_OP_EXCEPT(string ID,string EXCEPTTYPE, string EXCEPTGROUP,string EXCEPTREASON, string ERP_OPSEQ,
            string ERP_OPID, string ERP_WSID)
        {
            int sequence = 0;
            try
            {

          
            StringBuilder sql = new StringBuilder();
           string temp = "";
            sql.Append(@"
select max(SEQUENCE) from  SFT_OP_EXCEPT where 1=1
 ");
            sql.Append("and ID = '" + ID + "' ");
            sql.Append("and EXCEPTTYPE = '" + EXCEPTTYPE + "' ");
            sql.Append("and EXCEPTGROUP = '" + EXCEPTGROUP + "' ");
            sql.Append("and EXCEPTREASON = '" + EXCEPTREASON + "' ");
            sql.Append("and ERP_OPSEQ = '" + ERP_OPSEQ + "' ");
            sql.Append("and ERP_OPID = '" + ERP_OPID + "' ");
            sql.Append("and ERP_WSID = '" + ERP_WSID + "' ");
            SQLCommon sQLCommon = new SQLCommon();
             temp = sQLCommon.sqlExecuteScalarString(sql.ToString(), SQLCommon.SFT_TEST);
            if(temp != null && temp != "")
            {
                sequence = int.Parse(temp)+1;
            }
            else
            { sequence = 0; }
            }
            catch (Exception ex)
            {

                Logfile.Output(StatusLog.Error, " GetSequence_SFT_OP_EXCEPT", ex.Message);
            }
            return sequence;
        }
    }
   
   
    public class NGItemsMapping
    {
        public string Department { get; set; }
        public string NGCode_Process { get; set; }
        public string NGCodeName_Process { get; set; }
        public string NGCode_SFT { get; set; }
        public string NGCodeName_SFT { get; set; }
    }
}
