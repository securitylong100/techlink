using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using System.Windows.Forms;
using ImportDataToDatabase.Common;
using ImportDataToDatabase.Log;

namespace ImportDataToDatabase.FormGroup
{
    public class insertERP
    {
        string TOID = "";
        double LOTSIZE_B02 = 0;
        double LOTSIZE_B01 = 0;
        string ITEMID_TC047 = "";
        string ITEMNAME_TC048 = "";
        string TRANSNO = "";
        string TO008 = "";

        public void InsertdataToERP(string barcode, string output, string NG)
        {
            try
            {
                string[] QR = barcode.Split(';');
                string[] ML = QR[0].Split('-');
                string date = DateTime.Now.ToString("yyyyMMdd");
                string time = DateTime.Now.ToString("HH:mm:ss");
                string month = DateTime.Now.ToString("yyMM");
                SQLCommon data = new SQLCommon();
                // An chinh sua
                string TC002 = GetTC002();
                TO008 = TC002;
                TOID = "AD21-" + TC002; //chay thuc te
                //
                string TC047 = data.sqlExecuteScalarString("select distinct TA006 from MOCTA where TA001 = '" + ML[0] + "' and TA002 = '" + ML[1] + "'", SQLCommon.ERP_TEST);
                ITEMID_TC047 = TC047;
                string TC048 = data.sqlExecuteScalarString("select distinct TA034 from MOCTA where TA001 = '" + ML[0] + "' and TA002 = '" + ML[1] + "'", SQLCommon.ERP_TEST);
                ITEMNAME_TC048 = TC048;
                string TA007 = data.sqlExecuteScalarString("select distinct TA007 from SFCTA where TA004 ='B01' and  TA001 = '" + ML[0] + "' and TA002 = '" + ML[1] + "'", SQLCommon.ERP_TEST);
                int TC036 = int.Parse(output) + int.Parse(NG);
                // AN chinh sua
                TRANSNO = GetTB039(); //update to transfer table
                //
               StringBuilder sqlInsertSFCTC = new StringBuilder();
                sqlInsertSFCTC.Append("insert into SFCTC ");
                sqlInsertSFCTC.Append(@"(COMPANY,CREATOR,USR_GROUP,CREATE_DATE,MODIFIER,MODI_DATE,FLAG,CREATE_TIME,CREATE_AP,CREATE_PRID,MODI_TIME,MODI_AP,MODI_PRID,");
                sqlInsertSFCTC.Append(@"TC001,TC002,TC003,TC004,TC005,TC006,TC007,TC008,TC009,TC010,TC011,TC012,TC013,TC014,TC015,TC016,TC017,TC018,TC019,TC020,");
                sqlInsertSFCTC.Append(@"TC021,TC022,TC023,TC024,TC025,TC026,TC027,TC033,TC034,TC035,TC036,TC037,TC038,TC039,TC040,");
                sqlInsertSFCTC.Append(@"TC041,TC042,TC043,TC044,TC045,TC046,TC047,TC048,TC049,TC050,TC051,TC055)");
                sqlInsertSFCTC.Append(" values ( ");
                sqlInsertSFCTC.Append("'TECHLINK','BQC01','JG01','" + date + "','BQC01','" + date + "',2,'" + time + "','SFT','Sftb03','" + time + "','SFT','',");
                sqlInsertSFCTC.Append("'AD21','" + TC002 + "','0001','" + ML[0] + "','" + ML[1] + "','" + QR[1] + "','" + QR[2] + "','0020','B02','PCS','','','1'," + output + ",0," + NG + ",0,0,0,0,");
                sqlInsertSFCTC.Append("0,'Y','B01','',0,'N','N','" + date + "','" + date + "','N'," + TC036 + ",0,'" + date + "','0','',");
                sqlInsertSFCTC.Append("'B01',0,0,0,0,0,'" + TC047 + "','" + TC048 + "','大管','KG','0','N'");
                sqlInsertSFCTC.Append(")");

                StringBuilder sqlInsertSFCTB = new StringBuilder();
                sqlInsertSFCTB.Append("insert into SFCTB ");
                sqlInsertSFCTB.Append("(COMPANY,CREATOR,USR_GROUP,CREATE_DATE,MODIFIER,MODI_DATE,FLAG,CREATE_TIME,CREATE_AP,CREATE_PRID,MODI_TIME,MODI_AP,MODI_PRID,");
                sqlInsertSFCTB.Append("TB001,TB002,TB003,TB004,TB005,TB006,TB007,TB008,TB009,TB010,TB011,TB012,TB013,TB014,TB015,TB016,TB017,TB018,TB019,TB020,");
                sqlInsertSFCTB.Append(" TB021,TB022,TB023,TB024,TB025,TB026,TB031,TB036,TB037,TB038,TB039)");

                sqlInsertSFCTB.Append(" values ( ");
                sqlInsertSFCTB.Append("'TECHLINK','BQC01','JG01','" + date + "','BQC01','" + date + "',2,'" + time + "','SFT','Sftb03','" + time + "','SFT','',");
                sqlInsertSFCTB.Append("'AD21','" + TC002 + "','" + date + "','1','B01','" + TA007 + "','1','B01','" + TA007 + "','TL',0,'N','Y','','" + date + "','ERP','N','','1','',");
                sqlInsertSFCTB.Append("'','1','1','','" + month + "',0.2,'0','VND',1,'AD21','" + TRANSNO + "'");
                sqlInsertSFCTB.Append(")");

                SQLCommon sqlInsert = new SQLCommon();
                sqlInsert.ERPSQLExcuteNonQuery(sqlInsertSFCTB.ToString(), SQLCommon.ERP_TEST);
                sqlInsert.ERPSQLExcuteNonQuery(sqlInsertSFCTC.ToString(), SQLCommon.ERP_TEST);
            }
            catch (Exception ex)
            {

                Logfile.Output(StatusLog.Error, "InsertdataToERP(string barcode, string output, string NG)", ex.Message);
            }

        }
        public string GetTB039()
        {
            string _TB039 = "";
            string dateFormat = DateTime.Now.ToString("yyyyMMdd").Substring(1);
            string countFormatReset = "0001";
            int countUp = 0;
             SQLCommon data = new SQLCommon();
            string strData = data.sqlExecuteScalarString("select max(TRANSNO) from SFT_TRANSORDER where TRANSTYPE = 'AD21'  ", SQLCommon.SFT_TEST);
            if (strData != null && strData.Trim().Count() == 11)
            {
                string DateDatabase = strData.Trim().Substring(0, 7);
                string strCount = strData.Trim().Substring(7);
                if (dateFormat == DateDatabase)
                {
                    countUp = int.Parse(strCount) + 1;
                    string countFormatup = countUp.ToString("0000");
                    _TB039 = dateFormat + countFormatup;
                }
                else
                {
                    _TB039 = dateFormat + countFormatReset;
                }
            }

            return _TB039;
        }
        public string GetTC002()
        {
            string _TC002 = "";
            string dateFormat = DateTime.Now.ToString("yyMM");
            string countFormatReset = "0001";
            int countUp = 0;
            SQLCommon data = new SQLCommon();
            string strData = data.sqlExecuteScalarString("select max(TB002) from SFCTB where TB001 = 'AD21'  ", SQLCommon.ERP_TEST);
            if (strData != null &&  strData.Trim().Count() == 8)
            {
                string DateDatabase = strData.Trim().Substring(0, 4);
                string strCount = strData.Trim().Substring(4);
                if (dateFormat == DateDatabase)
                {
                    countUp = int.Parse(strCount) + 1;
                    string countFormatup = countUp.ToString("0000");
                    _TC002 = dateFormat + countFormatup;
                }
                else
                {
                    _TC002 = dateFormat + countFormatReset;
                }
            }

            return _TC002;
        }
        public void updateERP(string barcode)
        {
            try
            {
                string[] QR = barcode.Split(';');
                string[] ML = QR[0].Split('-');
                SQLCommon data = new SQLCommon();
                string TA011 = data.sqlExecuteScalarString("select sum(TC014)  from SFCTC where TC004 = '" + ML[0] + "' and   TC005 = '" + ML[1] + "' and TC001 = 'AD21'", SQLCommon.ERP_TEST);
                string TA012 = data.sqlExecuteScalarString("select sum(TC016)  from SFCTC where TC004 = '" + ML[0] + "' and   TC005 = '" + ML[1] + "' and TC001 = 'AD21'", SQLCommon.ERP_TEST);
                StringBuilder updateSFCTA = new StringBuilder();
                updateSFCTA.Append("update SFCTA set TA011 = " + TA011 + ", TA012 = " + TA012 + " where TA001 = '" + ML[0] + "' and TA002 = '" + ML[1] + "' and TA003 = '" + QR[1] + "' and TA004 = '" + QR[2] + "'");
                SQLCommon sqlUpdate = new SQLCommon();
                sqlUpdate.ERPSQLExcuteNonQuery(updateSFCTA.ToString(), SQLCommon.ERP_TEST);
            }
            catch (Exception ex)
            {

                Logfile.Output(StatusLog.Error, "updateERP(string barcode)", ex.Message);
            }
        }
        public void updateERPMQC(string serno)
        {
            try
            {

                StringBuilder update = new StringBuilder();
                update.Append("update m_ERPMQC set status = 'OK' where serno = '" + serno + "'");
                SQLCommon data = new SQLCommon();
                data.ERPSQLExcuteNonQuery(update.ToString(), SQLCommon.ERPSOFT);
            }
            catch (Exception ex)
            {

                Logfile.Output(StatusLog.Error, "updateERPMQC(string serno)", ex.Message);
            }
        }
        public bool InsertToERPMQC_Error(DataTable table, string CountSummary, string Remark)
        {
            try
            {


                StringBuilder sqlInsert = new StringBuilder();
                sqlInsert.Append("insert into m_ERPMQC_Error ");
                sqlInsert.Append(@"(serno, lot, model, site, factory, line, process, item, inspectdate, inspecttime, data, judge, status, remark )");
                sqlInsert.Append(" values ( ");
                sqlInsert.Append("'" + table.Rows[0]["serno"].ToString() + "' , ");
                sqlInsert.Append("'" + table.Rows[0]["lot"].ToString() + "' , ");
                sqlInsert.Append("'" + table.Rows[0]["model"].ToString() + "' , ");
                sqlInsert.Append("'" + table.Rows[0]["site"].ToString() + "' , ");
                sqlInsert.Append("'" + table.Rows[0]["factory"].ToString() + "' , ");
                sqlInsert.Append("'" + table.Rows[0]["line"].ToString() + "' , ");
                sqlInsert.Append("'" + table.Rows[0]["process"].ToString() + "' , ");
                sqlInsert.Append("'" + table.Rows[0]["item"].ToString() + "' , ");
                sqlInsert.Append("'" + table.Rows[0]["inspectdate"].ToString() + "' , ");
                sqlInsert.Append("'" + table.Rows[0]["inspecttime"].ToString() + "' , ");
                sqlInsert.Append("'" + CountSummary.ToString() + "' , ");
                sqlInsert.Append("'" + table.Rows[0]["judge"].ToString() + "' , ");
                sqlInsert.Append("'" + table.Rows[0]["status"].ToString() + "', ");
                sqlInsert.Append("'" + Remark + "' ) ");
                SQLCommon data = new SQLCommon();
                data.ERPSQLExcuteNonQuery(sqlInsert.ToString(), SQLCommon.ERPSOFT);
                return true;
            }
            catch (Exception ex)
            {
                Logfile.Output(StatusLog.Error, "InsertToERPMQC_Error(DataTable table, string CountSummary,string Remark)", ex.Message);
                return false;
            }


        }
        public bool UpdateToERPMQC_Error(string Status, string serno)
        {
            try
            {
                StringBuilder sqlupdate = new StringBuilder();
                sqlupdate.Append("update m_ERPMQC_Error set status = '" + Status + "' where ");
                sqlupdate.Append("serno = '" + serno + "'");
                SQLCommon data = new SQLCommon();
                data.ERPSQLExcuteNonQuery(sqlupdate.ToString(), SQLCommon.ERPSOFT);
                return true;
            }
            catch (Exception ex)
            {
                Logfile.Output(StatusLog.Error, " UpdateToERPMQC_Error(string Status, string serno)", ex.Message);
                return false;
            }


        }
        public void UpdatedataToSFT(string barcode)
        {
            try
            {


                SQLCommon data = new SQLCommon();
                string[] QR = barcode.Split(';');
                string[] ML = QR[0].Split('-');

                string str_OUTQTY = data.sqlExecuteScalarString("select sum(OUTQTY) as OUTQTY from SFT_OP_REALRUN where ID = '" + QR[0] + "' and OPID = 'B01---B01' and SEQUENCE not like 0", SQLCommon.SFT_TEST);
                double OUTQTY = 0;
                if (str_OUTQTY == "" || str_OUTQTY == null)
                    OUTQTY = 0;
                else OUTQTY = double.Parse(str_OUTQTY);

                string str_DEFECTQTY = data.sqlExecuteScalarString("select sum(DEFECTQTY) as DEFECTQTY from SFT_OP_REALRUN where ID = '" + QR[0] + "' and OPID = 'B01---B01' and SEQUENCE not like 0", SQLCommon.SFT_TEST);
                double DEFECTQTY = 0;
                if (str_DEFECTQTY == "" || str_DEFECTQTY == null)
                    DEFECTQTY = 0;
                else DEFECTQTY = double.Parse(str_DEFECTQTY);
                //  double DEFECTQTY = (str_DEFECTQTY != "" || str_DEFECTQTY != null) ? double.Parse(str_OUTQTY) : 0;
                double ALERADYDEFECTQTY = DEFECTQTY;
                double ARRIVEQTY = OUTQTY + DEFECTQTY;
                double OR019 = DEFECTQTY;
                double OR017 = double.Parse(data.sqlExecuteScalarString("select MAX(OR017)  from SFT_OP_REALRUN where ID = '" + QR[0] + "' and OPID = 'B01---B01' and SEQUENCE = 0", SQLCommon.SFT_TEST));
                double OR017B2 = OR017 - OR019;
                double OR020 = OR017 - ARRIVEQTY;
                LOTSIZE_B01 = OR020; //dung cho update vào LOTTAble
                LOTSIZE_B02 = OUTQTY; //dung cho update vào LOTTAble
                StringBuilder update = new StringBuilder();
                update.Append("update SFT_OP_REALRUN set ALERADYDEFECTQTY = " + ALERADYDEFECTQTY + ",");
                update.Append("OUTQTY = " + OUTQTY + ",");
                update.Append("DEFECTQTY = " + DEFECTQTY + ",");
                update.Append("ARRIVEQTY = " + ARRIVEQTY + ",");
                update.Append("OR019 = " + OR019 + ",");
                update.Append("OR020 = " + OR020 + " ");
                update.Append("where ID = '" + QR[0] + "' and OPID = 'B01---B01' and SEQUENCE  = 0");
                data.ERPSQLExcuteNonQuery(update.ToString(), SQLCommon.SFT_TEST);

                //update toSFT_OP_REALRUN B02
                StringBuilder UpdateB02 = new StringBuilder();
                UpdateB02.Append("update SFT_OP_REALRUN set OR017 = " + OR017B2 + ",");
                UpdateB02.Append("OR020 = " + OUTQTY + " ");
                UpdateB02.Append("where ID = '" + QR[0] + "' and OPID = 'B02---B01' and SEQUENCE = 0");
                data.ERPSQLExcuteNonQuery(UpdateB02.ToString(), SQLCommon.SFT_TEST);//chi dieu se kiem tra lai co up hay ko

                //update into LOT table B01
                string updateLOT = "update LOT set TYPE = 0, LOTSIZE =  " + LOTSIZE_B01 + ",ISPLANNED = 0  where ID = '" + QR[0] + "' and ERP_OPSEQ = '0010' and ERP_OPID = 'B01'";
                data.ERPSQLExcuteNonQuery(updateLOT, SQLCommon.SFT_TEST);
            }
            catch (Exception ex)
            {

                Logfile.Output(StatusLog.Error, " UpdatedataToSFT(string barcode)", ex.Message);
            }

        }
        public void InsertdataToSFT(string barcode, string output, string NG)
        {
            //  TOID = "D201-190xxxxx"; //chay thu nghiem
            try
            {


                string[] QR = barcode.Split(';');
                string[] ML = QR[0].Split('-');

                string datetime = DateTime.Now.ToString("yyyyMMdd HH:mm:ss");
                SQLCommon data = new SQLCommon();
                string str_OUTQTY = data.sqlExecuteScalarString("select sum(OUTQTY) as OUTQTY from SFT_OP_REALRUN where ID = '" + QR[0] + "' and OPID = 'B01---B01' and SEQUENCE not like 0", SQLCommon.SFT_TEST);
                double OUTQTY = 0;
                if (str_OUTQTY == "" || str_OUTQTY == null)
                    OUTQTY = 0;
                else OUTQTY = double.Parse(str_OUTQTY);
                LOTSIZE_B02 = OUTQTY + int.Parse(output);
                int SEQUENCE = int.Parse(data.sqlExecuteScalarString("select max(SEQUENCE) from SFT_OP_REALRUN where ID = '" + QR[0] + "'", SQLCommon.SFT_TEST)) + 1;

                //insert  into SFT_OP_REALRUN
                StringBuilder sqlInsertSFT_OP_REALRUN = new StringBuilder();
                sqlInsertSFT_OP_REALRUN.Append(@"insert into SFT_OP_REALRUN ");
                sqlInsertSFT_OP_REALRUN.Append(@"(ID,SEQUENCE,OPID,OUTQTY,OUTTIME,ALERADYDEFECTQTY,");
                sqlInsertSFT_OP_REALRUN.Append(" DEFECTQTY,OPERID,OUTUNIT,ERP_OPSEQ,MANWORKTIME,COINSTYPE,");
                sqlInsertSFT_OP_REALRUN.Append("TOID,ERP_OPID,ERP_WSID,OR002,OR003,OR004,OR013,OR014,OR019)");
                sqlInsertSFT_OP_REALRUN.Append(" values ( ");

                sqlInsertSFT_OP_REALRUN.Append("'" + QR[0] + "'," + SEQUENCE + ",'B01---B01'," + output + ",'" + datetime + "'," + NG + ",");
                sqlInsertSFT_OP_REALRUN.Append(NG + ",'BQC01','PCS','0010',500,'VND',");
                sqlInsertSFT_OP_REALRUN.Append("'" + TOID + "','B01','B01','0020','B02','B01'," + SEQUENCE * 2 + ",'BQC01'," + NG);
                sqlInsertSFT_OP_REALRUN.Append(")");
  //              data.ERPSQLExcuteNonQuery(sqlInsertSFT_OP_REALRUN.ToString(), SQLCommon.SFT_TEST);

                //delete from LOt table
                string sqldeleteLOT = "delete from LOT where ID = '" + QR[0] + "' and ERP_OPSEQ = '0020' and ERP_OPID = 'B02'";
 //               data.ERPSQLExcuteNonQuery(sqldeleteLOT, SQLCommon.SFT_TEST);

                //insert into LOT table B02
                StringBuilder sqlinsertSFT_LoT = new StringBuilder();
                sqlinsertSFT_LoT.Append(@"insert into LOT ");
                sqlinsertSFT_LoT.Append(" ( ID,TYPE,MOID,ITEMID,LOTSIZE,STATUS,ISPLANNED,UNIT,");
                sqlinsertSFT_LoT.Append("QTYPER,ERP_OPSEQ,ERP_OPID,ERP_WSID,LOT004,LOT005)");
                sqlinsertSFT_LoT.Append(" values ( ");
                sqlinsertSFT_LoT.Append("'" + QR[0] + "',0,'" + QR[0] + "','" + ITEMID_TC047 + "'," + LOTSIZE_B02 + ",0,1,'PCS',");
                sqlinsertSFT_LoT.Append("1,'0020','B02','B01',1,1");
                sqlinsertSFT_LoT.Append(")");
 //               data.ERPSQLExcuteNonQuery(sqlinsertSFT_LoT.ToString(), SQLCommon.SFT_TEST);

                //insert into SFT_WS_RUN--Checkin
                int SEQUENCE_SFT_WS_RUN = int.Parse(data.sqlExecuteScalarString("select max(SEQUENCE) from SFT_WS_RUN where ID = '" + QR[0] + "'", SQLCommon.SFT_TEST)) + 1;
                int Total = int.Parse(output) + int.Parse(NG);
                string date = DateTime.Now.ToString("yyyyMMdd");
                string month = DateTime.Now.ToString("yyyyMM");

                StringBuilder sqlinsertSFT_WS_RUN_IN = new StringBuilder();
                sqlinsertSFT_WS_RUN_IN.Append(@"insert into SFT_WS_RUN ");
                sqlinsertSFT_WS_RUN_IN.Append("(WSID,OPID,ID,SEQUENCE,EXECUTETIME,EXECUTETYPE,EXECUTEQTY,USERID,");
                sqlinsertSFT_WS_RUN_IN.Append("QTYPERUNIT,UNIT,QTYPER,PLUSINDEX,ERP_OPSEQ,ERP_OPID,ERP_WSID,WR002,");
                sqlinsertSFT_WS_RUN_IN.Append("WR003,WR004,WR005,WR007,WR008,WR009,WR021,");
                sqlinsertSFT_WS_RUN_IN.Append(" WR026,CREATE_DATE,WR031,WR032)");
                sqlinsertSFT_WS_RUN_IN.Append(" values(");
                sqlinsertSFT_WS_RUN_IN.Append("'B01','B01---B01','" + QR[0] + "'," + SEQUENCE_SFT_WS_RUN + ",'" + datetime + "','checkIn',"+Total+ ",'BQC01',");
                sqlinsertSFT_WS_RUN_IN.Append("0,'PCS',1,0,'0010','B01','B01','',");
                sqlinsertSFT_WS_RUN_IN.Append("'',"+Total+","+Total+ ",'0010','B01','B01','',");
                sqlinsertSFT_WS_RUN_IN.Append("'"+datetime+"','"+datetime+"',0,0");
                sqlinsertSFT_WS_RUN_IN.Append(")");
//                data.ERPSQLExcuteNonQuery(sqlinsertSFT_WS_RUN_IN.ToString(), SQLCommon.SFT_TEST);

                //insert into SFT_WS_RUN--CheckOUT
                StringBuilder sqlinsertSFT_WS_RUN_OUT = new StringBuilder();
                sqlinsertSFT_WS_RUN_OUT.Append(@"insert into SFT_WS_RUN ");
                sqlinsertSFT_WS_RUN_OUT.Append("(WSID,OPID,ID,SEQUENCE,EXECUTETIME,EXECUTETYPE,EXECUTEQTY,USERID,");
                sqlinsertSFT_WS_RUN_OUT.Append("QTYPERUNIT,UNIT,QTYPER,PLUSINDEX,ERP_OPSEQ,ERP_OPID,ERP_WSID,WR002,");
                sqlinsertSFT_WS_RUN_OUT.Append("WR003,WR004,WR005,WR007,WR008,WR009,WR021,");
                sqlinsertSFT_WS_RUN_OUT.Append(" WR026,CREATE_DATE,WR030,WR031,WR032)");
                sqlinsertSFT_WS_RUN_OUT.Append(" values(");
                sqlinsertSFT_WS_RUN_OUT.Append("'B01','B01---B01','" + QR[0] + "'," + SEQUENCE_SFT_WS_RUN+1 + ",'" + datetime + "','checkOut'," + output + ",'BQC01',");
                sqlinsertSFT_WS_RUN_OUT.Append("0,'PCS',1,0,'0010','B01','B01','"+ TOID + "',");
                sqlinsertSFT_WS_RUN_OUT.Append("''," + output + "," + output + ",'0010','B01','B01','BQC01',");
                sqlinsertSFT_WS_RUN_OUT.Append("'" + datetime + "','" + datetime + "',1,0,0");
                sqlinsertSFT_WS_RUN_OUT.Append(")");
                //                data.ERPSQLExcuteNonQuery(sqlinsertSFT_WS_RUN_OUT.ToString(), SQLCommon.SFT_TEST);


                //insert into SFT_TRANSORDER <có  duplicate key in object 'dbo.SFT_TRANSORDER'.>
                StringBuilder sqlinsertSFT_TRANSORDER = new StringBuilder();
                sqlinsertSFT_TRANSORDER.Append(@"insert into SFT_TRANSORDER ");
                sqlinsertSFT_TRANSORDER.Append("(CREATER,CREATE_DATE,FLAG,TRANSTYPE,TRANSNO,TRANSDATE,OUTTYPE,OUTDEPID,");
                sqlinsertSFT_TRANSORDER.Append("OUTDEPNAME,INTYPE,INDEPID,INDEPNAME,FACTORYID,CONFIRMCODE,DOCUMENTDATE,");
                sqlinsertSFT_TRANSORDER.Append("INVOICECOUNT,TAXATIONTYPE,DISCOUNTDEVIDE,DECLARATIONDATE,SALESTAXRATE,");
                sqlinsertSFT_TRANSORDER.Append("COMPANYID,KEYID,STOCKINTYPE,TO001,TO007,TO008,TO011,TO012,COINSTYPE,CONFIRMER,TO013)");
                sqlinsertSFT_TRANSORDER.Append(" values(");
                sqlinsertSFT_TRANSORDER.Append("'BQC01','"+datetime+ "',0,'AD21','"+TRANSNO+"','"+date+ "','1','B01',");
                sqlinsertSFT_TRANSORDER.Append("'M+H生产线ONGM+H','1','B01','M+H生产线ONGM+H','TL','Y','"+date+"',");
                sqlinsertSFT_TRANSORDER.Append("'1','1','1','"+month+"','0.2',");
                sqlinsertSFT_TRANSORDER.Append("'TECHLINK','"+QR[0]+"','0','1','AD21','"+TO008+ "',0,0,'VND','BQC01',1");
                sqlinsertSFT_TRANSORDER.Append(")");
                //              data.ERPSQLExcuteNonQuery(sqlinsertSFT_TRANSORDER.ToString(), SQLCommon.SFT_TEST);
                //insert into SFT_TRANSORDER_LINE 


                int PRODUCTIONSEQ_SFT_TRANSORDER_LINE = int.Parse(data.sqlExecuteScalarString("select max(PRODUCTIONSEQ) from SFT_TRANSORDER_LINE where KEYID = '" + QR[0] + "'", SQLCommon.SFT_TEST));
                if (PRODUCTIONSEQ_SFT_TRANSORDER_LINE == -1) PRODUCTIONSEQ_SFT_TRANSORDER_LINE = 0;
                PRODUCTIONSEQ_SFT_TRANSORDER_LINE++;
                StringBuilder sqlinsertSFT_TRANSORDER_LINE = new StringBuilder();
                sqlinsertSFT_TRANSORDER_LINE.Append(@"insert into SFT_TRANSORDER_LINE ");
                sqlinsertSFT_TRANSORDER_LINE.Append("(CREATE_DATE,TRANSORDERTYPE,TRANSNO,SN,MOTYPE,MONO,OUTOPSEQ,");//1
                sqlinsertSFT_TRANSORDER_LINE.Append("OUTOP,INOPSEQ,INOP,UNIT,PATTERN,SCRAPQTY,LABORHOUR,MACHINEHOUR,");//2
                sqlinsertSFT_TRANSORDER_LINE.Append("OUTDEP,EMERGENCY,TRANSQTY,INDEP,ITEMID,ITEMNAME,ITEMDESCRIPTION,");//3
                sqlinsertSFT_TRANSORDER_LINE.Append("TC015,TC017,TC018,TC019,KEYID,PRODUCTIONSEQ,TL002,TL003,TL004,");//4
                sqlinsertSFT_TRANSORDER_LINE.Append("TL005,TL006,SFTUPDATE,TC055,TL007,TL008,TL010,TL011,TL012,TL015,");//5
                sqlinsertSFT_TRANSORDER_LINE.Append("TL016,SPC,TWINUNIT,KEY_TRANSORDER,FACTORYID,INWSTYPE,OUTWSTYPE,");//6
                sqlinsertSFT_TRANSORDER_LINE.Append("TL017,TL018,TL023,TL024,TL025,TL029,TL027)");//7
                sqlinsertSFT_TRANSORDER_LINE.Append(" values(");
                sqlinsertSFT_TRANSORDER_LINE.Append("'" + datetime + "','AD21','" + TRANSNO + "','1','" + ML[0] + "','" + ML[1] + "','0010',");//1
                sqlinsertSFT_TRANSORDER_LINE.Append("'B01','0020','B02','PCS','1'," + NG + ",0,0,");//2
                sqlinsertSFT_TRANSORDER_LINE.Append("'B01','N'," + Total + ",'B01','" + ITEMID_TC047 + "','" + ITEMNAME_TC048 + "','大管',");//3
                sqlinsertSFT_TRANSORDER_LINE.Append("0,0,0,'0','" + QR[0] + "'," + PRODUCTIONSEQ_SFT_TRANSORDER_LINE + "," + output + ",0,0,");//4
                sqlinsertSFT_TRANSORDER_LINE.Append("0,'0',0,'0',0,0,'" + date + "','AD21','" + TO008 + "'," + SEQUENCE_SFT_WS_RUN + ","); //5
                sqlinsertSFT_TRANSORDER_LINE.Append("0,'N','Y',1,'TL','1','1',");//6
                sqlinsertSFT_TRANSORDER_LINE.Append("'0',1,0,0,0,'N',0");
                sqlinsertSFT_TRANSORDER_LINE.Append(")");
                data.ERPSQLExcuteNonQuery(sqlinsertSFT_TRANSORDER_LINE.ToString(), SQLCommon.SFT_TEST);

            }
            catch (Exception ex)
            {

                Logfile.Output(StatusLog.Error, " InsertdataToSFT(string barcode, string output, string NG)", ex.Message);
            }
        }

    }
}
