using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using UploadDataToDatabase.Log;
using UploadDataToDatabase.Class;

namespace UploadDataToDatabase
{
    public partial class Form1 : CommonForm
    {
        private string path = Environment.CurrentDirectory;
        string version = "";
        // this timer calls bgWorker again and again after regular intervals
        System.Windows.Forms.Timer tmrCallBgWorker;
        // this is our worker
        BackgroundWorker bgWorker;
        System.Windows.Forms.Timer tmrSendMail;
        // this is our worker
        BackgroundWorker bgSendMailWorker;
        // this is the timer to make sure that worker gets called
        System.Threading.Timer tmrEnsureWorkerGetsCalled;
        List<ScheduleReportItems> listSchedule;
        DataTable dtScheduleSendMail;
        DataTable dtListEmail;
        // object used for safe access
        object lockObject = new object();
        object lockObjectSendMail = new object();
        List<ScheduleReportItems> listReport = new List<ScheduleReportItems>();
        List<EmailNeedSend> listEmail = new List<EmailNeedSend>();
        
        public Form1()
        {
            InitializeComponent();
            dtp_from.Value = DateTime.Now.AddDays(-30);
            dtp_todate.Value = DateTime.Now;

            version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Major.ToString(); //AssemblyVersion을 가져온다.
            version += "." + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Minor.ToString();
            version += "." + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Build.ToString();
            Text = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name + " " + version;
            btn_Start.Text = "Start";

            //timer_update.Start();
            // this timer calls bgWorker again and again after regular intervals
            tmrCallBgWorker = new System.Windows.Forms.Timer();
            tmrCallBgWorker.Tick += new EventHandler(tmrCallBgWorker_Tick);
            //tmrCallBgWorker.Interval = 10000;

            // this is our worker
            bgWorker = new BackgroundWorker();
            
            // work happens in this method
            bgWorker.DoWork += new DoWorkEventHandler(bg_DoWork);
            bgWorker.ProgressChanged += BgWorker_ProgressChanged;
            bgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bg_RunWorkerCompleted);
            bgWorker.WorkerReportsProgress = true;

            tmrSendMail = new System.Windows.Forms.Timer();
            tmrSendMail.Tick += TmrSendMail_Tick;

            bgSendMailWorker = new BackgroundWorker();
            bgSendMailWorker.DoWork += BgSendMailWorker_DoWork;
            bgSendMailWorker.RunWorkerCompleted += BgSendMailWorker_RunWorkerCompleted;
            bgSendMailWorker.WorkerReportsProgress = true;
            listSchedule = new List<ScheduleReportItems>();
            dtScheduleSendMail = new DataTable();
        }

        private void BgSendMailWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Logfile.Output(StatusLog.Normal, "Send mail complete");
        }
      
        private void BgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            btn_Start.Text = "Starting";
            pbar_upload.Value = e.ProgressPercentage;

        }

        void bg_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Complete");
        }

        void bg_DoWork(object sender, DoWorkEventArgs e)
        {
            // does a job like writing to serial communication, webservices etc
            var worker = sender as BackgroundWorker;
            int intProgress = 0;

            //dtp_from.Value = DateTime.Now.AddDays(-90);
            //dtp_todate.Value = DateTime.Now;
            if (checkInvalidDateTimeSpan(dtp_from.Value, DateTime.Now))
            {
                
                if (chb_uploadShipping.Checked)
                {

                    try //Upload Shipping 
                    {
                        intProgress += 10;
                        worker.ReportProgress(intProgress);
                        UploadDataShipping(dtp_from.Value, DateTime.Now);
                        intProgress +=90;
                        worker.ReportProgress(intProgress);
                       

                    }
                    catch (Exception ex)
                    {

                        Logfile.Output(StatusLog.Error, "Upload data for Shipping fail : ", ex.Message);
                    }
                }

                if (chb_uploadProduction.Checked)
                {
                    try //Upload Production
                    {
                        intProgress += 10;
                        worker.ReportProgress(intProgress);
                        UploadDataProduction(dtp_from.Value, DateTime.Now);
                        intProgress += 90;
                        worker.ReportProgress(intProgress);
                    }
                    catch (Exception ex)
                    {

                        Logfile.Output(StatusLog.Error, "Upload  data for Production fail : ", ex.Message);
                    }
                }
                if (chb_uploadMaterial.Checked)
                {
                    try //Upload Material 
                    {
                        intProgress += 10;
                        worker.ReportProgress(intProgress);
                        UploadDataMaterial(dtp_from.Value, DateTime.Now);
                        intProgress += 90;
                        worker.ReportProgress(intProgress);
                    }
                    catch (Exception ex)
                    {

                        Logfile.Output(StatusLog.Error, "Upload  data for Production fail : ", ex.Message);
                    }
                }
                Logfile.Output(StatusLog.Error, "Upload  data just Finished  ! ");
                //   MessageBox.Show("Upload  data just Finished  ! ");
            }
         
          //  System.Diagnostics.Debug.WriteLine("run !");
            System.Threading.Thread.Sleep(100);
        }

        void tmrCallBgWorker_Tick(object sender, EventArgs e)
        {
            if (Monitor.TryEnter(lockObject))
            {
                try
                {
                    // if bgworker is not busy the call the worker
                    if (!bgWorker.IsBusy)
                        bgWorker.RunWorkerAsync();
                }
                finally
                {
                    Monitor.Exit(lockObject);
                }

            }
            else
            {

                // as the bgworker is busy we will start a timer that will try to call the bgworker again after some time
                tmrEnsureWorkerGetsCalled = new System.Threading.Timer(new TimerCallback(tmrEnsureWorkerGetsCalled_Callback), null, 0, 10);

            }

        }

        void tmrEnsureWorkerSendmailGetsCalled_Callback(object obj)
        {
            // this timer was started as the bgworker was busy before now it will try to call the bgworker again
            if (Monitor.TryEnter(lockObjectSendMail))
            {
                try
                {
                    if (!bgSendMailWorker.IsBusy)
                        bgSendMailWorker.RunWorkerAsync();
                }
                finally
                {
                    Monitor.Exit(lockObjectSendMail);
                }
                tmrEnsureWorkerGetsCalled = null;
            }
        }
        void tmrEnsureWorkerGetsCalled_Callback(object obj)
        {
            // this timer was started as the bgworker was busy before now it will try to call the bgworker again
            if (Monitor.TryEnter(lockObject))
            {
                try
                {
                    if (!bgWorker.IsBusy)
                        bgWorker.RunWorkerAsync();
                }
                finally
                {
                    Monitor.Exit(lockObject);
                }
                tmrEnsureWorkerGetsCalled = null;
            }
        }
      
        private void UploadDataShipping ( DateTime datefrom ,DateTime dateto)
        {
         
            dtScheduleSendMail = new DataTable();
            StringBuilder sql = new StringBuilder();
            sql.Append(@"select
CONVERT(date,coptcs.CREATE_DATE) as Create_Date,
coptcs.TC001 as Code_Type, 
coptcs.TC002 as Code_No,
coptcs.TC005 as Department_code,
copmas.MA002 as Clients_Name,
coptcs.TC012 as Clients_Order_Code, 
coptds.TD004 as Product_Code,
coptds.TD005 as Product_Name,
coptds.TD008 as Order_Quanity,
coptds.TD010 as Unit,
coptcs.TC005 as Department,
coptds.TD047 as Client_Request_Date,
sum(copths.TH008) as Quanity_Delivery,
copths.TH004 as Product_Delivery,
copths.TH001 as Delivery_Code,
copths.TH005 as Name_Product_Delivery,
max(coptgs.TG003) as Delivery_Date,
coptjs.TJ008 as Quanity_Return,
coptis.TI003 as Return_Date,
 (sum(copths.TH008)/coptds.TD008)*100 as Shipping_Percent
 from COPTC coptcs
left join COPTD  coptds on coptcs.TC002 = coptds.TD002  and coptcs.TC001 = coptds.TD001 -- cong doan tao don
left join MOCTB  moctbs on coptcs.TC002 = moctbs.TB002  and coptcs.TC001 = moctbs.TB001
inner join COPMA copmas on copmas.MA001 = coptcs.TC004
left join COPTH copths on coptcs.TC002 = copths.TH015 and  coptcs.TC001 = copths.TH014--cong doan giao hang
left join COPTG coptgs on copths.TH002  = coptgs.TG002 and copths.TH001  = coptgs.TG001 --cong doan giao hang
left join COPTJ coptjs on coptcs.TC002 = coptjs.TJ019 and coptcs.TC001 = coptjs.TJ018-- cong doan tra hang
left join COPTI coptis on coptjs.TJ002 = coptis.TI002 and coptjs.TJ001 = coptis.TI001 --cong doan tra hang
where 1=1  
and copths.TH004  = coptds.TD004 ");
           
        
                sql.Append(" and CONVERT(date,coptcs.CREATE_DATE)  >= '" + datefrom + "' ");
                sql.Append(" and CONVERT(date,coptcs.CREATE_DATE) <= '" + dateto + "' ");

            
            sql.Append(@"group by 
                                   coptcs.CREATE_DATE,
                                    coptcs.TC001 ,
                                    coptcs.TC002 ,
                                    coptcs.TC005 ,
                                    copmas.MA002, 
                                   coptcs.TC012,
                                    coptds.TD004,
                                    coptds.TD005,
                                   coptds.TD008,
                                    coptds.TD010,
                                    coptcs.TC005,
                                    coptds.TD047,
                                    copths.TH004,
                                    copths.TH001,
                                    copths.TH005,
                                  --  coptgs.TG003,
                                    coptjs.TJ008,
                                    coptis.TI003
                                    ");
            sql.Append("order by coptcs.TC001, coptcs.TC002");
            sqlERPCON con = new sqlERPCON();
            con.sqlDataAdapterFillDatatable(sql.ToString(), ref dtScheduleSendMail);
            //checkdata
            if (dtScheduleSendMail.Rows.Count > 0)
            {
                for (int i = 0; i < dtScheduleSendMail.Rows.Count; i++)
                {
                    string sqlcheck = "";

                    string MaTaoDon = dtScheduleSendMail.Rows[i]["Code_Type"].ToString().Replace("'", "");
                    string codeDon = dtScheduleSendMail.Rows[i]["Code_No"].ToString().Replace("'", "");
                    string codeSanPham = dtScheduleSendMail.Rows[i]["Product_Code"].ToString().Replace("'", "");
                    string ShippingPercent = dtScheduleSendMail.Rows[i]["Shipping_Percent"].ToString().Replace("'", "");

                    sqlcheck = "select COUNT(*) from t_OCTC where TC02 = '" + MaTaoDon + "' and TC03 ='" + codeDon + "' and TC06='" + codeSanPham + "'";
                    sqlCON check = new sqlCON();
                    if (int.Parse(check.sqlExecuteScalarString(sqlcheck)) == 0) //insert
                    {
                        string list = "";
                        for (int j = 0; j < dtScheduleSendMail.Columns.Count; j++)
                        {
                            list += "'";
                            list += dtScheduleSendMail.Rows[i][j].ToString().Replace("'", "") + "',";
                        }
                        StringBuilder sqlinsert = new StringBuilder();
                        sqlinsert.Append("insert into t_OCTC ");
                        sqlinsert.Append(@"(TC01,TC02,TC03,TC04,TC05,TC06,TC07,TC08,TC09,TC10,TC11,TC12,TC13,TC14,TC15,TC16,TC17,TC18,TC19,TC32,TC31,UserName,datetimeRST) values ( ");
                        sqlinsert.Append(list);
                        if (ShippingPercent != "")
                        {
                            if (double.Parse(ShippingPercent) >= 100)
                                sqlinsert.Append("'OK',");
                            else
                            {
                                sqlinsert.Append("'WAITING',");
                            }
                        }
                        else { sqlinsert.Append("'WAITING',"); }
                        sqlinsert.Append("'" + Class.valiballecommon.GetStorage().UserName + "',GETDATE())");
                        sqlCON insert = new sqlCON();
                        insert.sqlExecuteNonQuery(sqlinsert.ToString(), false);
                    }
                    else //update
                    {

                        StringBuilder sqlupdate = new StringBuilder();
                        sqlupdate.Append("update t_OCTC set ");
                        sqlupdate.Append(@"TC13 = '" + dtScheduleSendMail.Rows[i]["Quanity_Delivery"].ToString().Replace("'", "") + "',");
                        sqlupdate.Append(@"TC17 = '" + dtScheduleSendMail.Rows[i]["Delivery_Date"].ToString().Replace("'", "") + "',");
                        sqlupdate.Append(@"TC32 = '" + ShippingPercent + "',");
                        if (ShippingPercent != "")
                        {
                            if (double.Parse(ShippingPercent) >= 100)
                                sqlupdate.Append(@"TC31 = 'OK'");
                            else
                            {
                                sqlupdate.Append(@"TC31 = 'WAITING'");
                            }
                        }
                        else { sqlupdate.Append(@"TC31 = 'WAITING'"); }
                        sqlupdate.Append(@" where TC02 = '" + MaTaoDon + "' and TC03 ='" + codeDon + "' and TC06='" + codeSanPham + "'");

                        sqlCON update = new sqlCON();
                        update.sqlExecuteNonQuery(sqlupdate.ToString(), false);
                    }
                }
            }
        }
        private void UploadDataProduction(DateTime datefrom, DateTime dateto)
        {

            dtScheduleSendMail = new DataTable();
            StringBuilder sql = new StringBuilder();
            sql.Append(@"
                            select
                            CONVERT(date,moctas.CREATE_DATE) as Create_Date,
                            moctas.TA026 as Code_Type,
                            moctas.TA027 as Code_No,
                            moctas.TA001 as Production_Planning_Code,
                            moctas.TA002 as Production_Planning_No,
                            moctas.TA006 as Product_Code,
                            moctas.TA034 as Product_Name,
                            moctas.TA009 as Production_Start_Date,
                            moctas.TA010 as Estimate_Complete_Date,
                            moctas.TA012 as Actual_Production_Date,
                            moctas.TA013 as Confirm,
                            moctas.TA015 as Plan_Quanity,
                            sum(moctgs.TG011) as Finished_Goods,
                            sum(moctgs.TG012) as NG_Quanity,
                            sum(moctgs.TG013) as Good_Quanity,
                            moctgs.TG007 as Unit, 
                            max(CONVERT(date,moctfs.TF003)) as Input_Date
                            from MOCTA moctas
                            left join MOCTG moctgs on moctgs.TG014 = moctas.TA001 and moctgs.TG015 = moctas.TA002
                            left join MOCTF moctfs on moctfs.TF001 = moctgs.TG001 and moctfs.TF002 = moctgs.TG002
                            where 1=1 ");
        
        
          
                sql.Append(" and CONVERT(date,moctas.CREATE_DATE) >= '" + datefrom + "'");
                sql.Append(" and CONVERT(date,moctas.CREATE_DATE) <= '" + dateto + "'");
          

            sql.Append(@" group by 
                                    moctas.CREATE_DATE,
                                    moctas.TA001 ,
                                    moctas.TA002 ,                                   
                                    moctas.TA006,
                                    moctas.TA009 ,
                                    moctas.TA010 ,
                                    moctas.TA012 ,
                                    moctas.TA013 ,
                                    moctas.TA015,
                                    moctas.TA034,
                                    moctas.TA026 ,
                                    moctas.TA027,
                                    
                                    moctgs.TG007 ");

            sql.Append(" order by moctas.TA002");
            sqlERPCON con = new sqlERPCON();
            con.sqlDataAdapterFillDatatable(sql.ToString(), ref dtScheduleSendMail);
            //checkdata
            if (dtScheduleSendMail.Rows.Count > 0)
            {
                for (int i = 0; i < dtScheduleSendMail.Rows.Count; i++)
                {

                    string MaTaoDon = dtScheduleSendMail.Rows[i]["Code_Type"].ToString().Replace("'", "");
                    string codeTaoLenh = dtScheduleSendMail.Rows[i]["Code_No"].ToString().Replace("'", "");
                    string sqlcheck = "select COUNT(*) from t_OCTB where TB02 = '" + MaTaoDon + "' and TB03 ='" + codeTaoLenh + "'";
                    double FinishedGoodQty = 0;
                    double NGQty = 0;
                    double OKQty = 0;
                    double PercentofOKQty = 0;
                    try
                    {
                        FinishedGoodQty = double.Parse(dtScheduleSendMail.Rows[i]["Finished_Goods"].ToString().Replace("'", ""));
                        NGQty = double.Parse(dtScheduleSendMail.Rows[i]["NG_Quanity"].ToString().Replace("'", ""));
                        OKQty = double.Parse(dtScheduleSendMail.Rows[i]["Good_Quanity"].ToString().Replace("'", ""));
                        if (FinishedGoodQty != 0)
                            PercentofOKQty = Math.Round((OKQty / FinishedGoodQty) * 100, 2);

                    }
                    catch (Exception ex)
                    {
                        FinishedGoodQty = 0;
                        NGQty = 0;
                        OKQty = 0;
                        //MessageBox.Show(ex.Message);

                    }


                    sqlCON check = new sqlCON();
                    if (int.Parse(check.sqlExecuteScalarString(sqlcheck)) == 0) //insert
                    {
                        string list = "";
                        for (int j = 0; j < dtScheduleSendMail.Columns.Count; j++)
                        {
                            list += "'";
                            list += dtScheduleSendMail.Rows[i][j].ToString() + "',";
                        }
                        StringBuilder sqlinsert = new StringBuilder();
                        sqlinsert.Append("insert into t_OCTB ");
                        sqlinsert.Append(@"(TB01,TB02,TB03,TB04,TB05,TB06,TB07,TB08,TB09,TB10,TB11,TB12,TB13,TB14,TB15,TB16,TB17,TB31,TB32,TB33,UserName,datetimeRST) values ( ");
                        sqlinsert.Append(list);
                        if (PercentofOKQty >= 100)
                        {
                            sqlinsert.Append("'OK', '" + PercentofOKQty.ToString() + " ' , '0' " + ",");
                        }
                        else if (PercentofOKQty > 98)
                        {
                            sqlinsert.Append("'OK', '" + PercentofOKQty.ToString() + " ' , '1' " + ",");
                        }
                        else if (PercentofOKQty < 98 && PercentofOKQty > 95)
                        {
                            sqlinsert.Append("'OK', '" + PercentofOKQty.ToString() + " ' , '2' " + ",");
                        }
                        else if (PercentofOKQty < 95)
                        {
                            sqlinsert.Append("'NG', '" + PercentofOKQty.ToString() + " ' , '0' " + ",");
                        }

                        sqlinsert.Append("'" + Class.valiballecommon.GetStorage().UserName + "',GETDATE())");
                        sqlCON insert = new sqlCON();
                        insert.sqlExecuteNonQuery(sqlinsert.ToString(), false);
                    }
                    else //update
                    {
                        StringBuilder sqlupdate = new StringBuilder();
                        sqlupdate.Append("update t_OCTB set ");
                        sqlupdate.Append(@"TB13 = '" + dtScheduleSendMail.Rows[i]["Finished_Goods"].ToString() + "',");
                        sqlupdate.Append(@"TB14 = '" + dtScheduleSendMail.Rows[i]["NG_Quanity"].ToString() + "',");
                        sqlupdate.Append(@"TB15 = '" + dtScheduleSendMail.Rows[i]["Good_Quanity"].ToString() + "',");
                        sqlupdate.Append(@"TB17 = '" + dtScheduleSendMail.Rows[i]["Input_Date"].ToString() + "'");
                        if (PercentofOKQty >= 100)
                        {
                            sqlupdate.Append(@", TB31 = 'OK' ,");
                            sqlupdate.Append(@" TB32 = ' " + PercentofOKQty.ToString() + "' ,");
                            sqlupdate.Append(@"TB33 = '0' ");
                        }
                        else if (PercentofOKQty > 98)
                        {
                            sqlupdate.Append(@", TB31 = 'OK' ,");
                            sqlupdate.Append(@" TB32 = ' " + PercentofOKQty.ToString() + "' ,");
                            sqlupdate.Append(@"TB33 = '1' ");
                        }
                        else if (PercentofOKQty < 98 && PercentofOKQty > 95)
                        {
                            sqlupdate.Append(@", TB31 = 'OK' ,");
                            sqlupdate.Append(@" TB32 = ' " + PercentofOKQty.ToString() + "' ,");
                            sqlupdate.Append(@"TB33 = '2' ");

                        }
                        else if (PercentofOKQty < 95)
                        {
                            sqlupdate.Append(@", TB31 = 'NG' ,");
                            sqlupdate.Append(@" TB32 = ' " + PercentofOKQty.ToString() + "' ,");
                            sqlupdate.Append(@"TB33 = '0' ");
                        }
                        //  sqlupdate.Append("'" + Class.valiballecommon.GetStorage().UserName + "',GETDATE())");

                        sqlupdate.Append(@" where TB02 = '" + MaTaoDon + "' and TB03 ='" + codeTaoLenh + "'");

                        sqlCON update = new sqlCON();
                        update.sqlExecuteNonQuery(sqlupdate.ToString(), false);
                    }
                }
            }

        }
        private void UploadDataMaterial (DateTime datefrom, DateTime dateto)
        {
         
            dtScheduleSendMail = new DataTable();
            StringBuilder sql = new StringBuilder();
            sql.Append(@"
                                                       select distinct
CONVERT(date,moctas.CREATE_DATE) as Create_Date,
moctas.TA026 as Code_Type,
moctas.TA027 as Code_No,
moctas.TA001 as Production_Planning_Code,
moctas.TA002 as Production_Planning_No,
moctas.TA006 as Product_Code,
moctas.TA034 as Product_Name,
moctas.TA009 as Production_Start_Date,
moctas.TA010 as Estimate_Complete_Date,
moctas.TA012 as Actual_Production_Date,
moctas.TA013 as Confirm,
moctas.TA015 as Product_Quanity,
moctbs.TB003 as Material_Code,
moctbs.TB012 as Material_Name,
moctbs.TB009 as Warehourse_Code,
moctbs.TB015 as Ready_Material_Date,
moctbs.TB004 as amount_of_material_receive,
moctbs.TB005 as amount_of_material_use,
invmbs.MB064 as Avaiable_Material_Quanity,
sum(moctes.TE005) as Production_Material_Quantity,
moctbs.TB007 as Unit
from MOCTA moctas
left join MOCTB moctbs on moctas.TA001 = moctbs.TB001 and moctas.TA002 = moctbs.TB002
left join INVMB invmbs on moctbs.TB003 = invmbs.MB001
left join MOCTE moctes on  moctes.TE004 =moctbs.TB003 and moctes.TE011 =moctas.TA001 and  moctes.TE012 =moctas.TA002
                            where 1=1  and moctes.TE001 not like '%6%' ");
            

                sql.Append(" and CONVERT(date,moctas.CREATE_DATE) >= '" + datefrom + "' ");
                sql.Append(" and CONVERT(date,moctas.CREATE_DATE) <= '" + dateto + "' ");
            

            sql.Append(@" group by
 moctas.CREATE_DATE,
                                    moctas.TA001 ,
                                    moctas.TA002 ,
                                    moctas.TA003,
                                    moctas.TA006,
                                    moctas.TA009 ,
                                    moctas.TA010 ,
                                    moctas.TA012 ,
                                    moctas.TA013 ,
                                    moctas.TA015,
                                    moctas.TA024 ,
                                    moctas.TA025,
                                    moctas.TA026 ,
                                    moctas.TA027,
                                    moctas.TA034,
                                    moctbs.TB003,
                                    moctbs.TB012,
                                    moctbs.TB009,
                                    moctbs.TB015,
                                    moctbs.TB004,
                                    moctbs.TB005,
                                    moctbs.TB007,
									invmbs.MB064
									
");

            sql.Append(" order by moctas.TA002");
            sqlERPCON con = new sqlERPCON();
            con.sqlDataAdapterFillDatatable(sql.ToString(), ref dtScheduleSendMail);
            //  checkdata
            if (dtScheduleSendMail.Rows.Count > 0)
            {
                try
                {


                    for (int i = 0; i < dtScheduleSendMail.Rows.Count; i++)
                    {
                        string sqlcheck = "";

                        string MaDDH = dtScheduleSendMail.Rows[i]["Code_Type"].ToString();
                        string SoDDH = dtScheduleSendMail.Rows[i]["Code_No"].ToString();
                        string MaLSX = dtScheduleSendMail.Rows[i]["Production_Planning_Code"].ToString();
                        string SoLSX = dtScheduleSendMail.Rows[i]["Production_Planning_No"].ToString();
                        string codeSanPham = dtScheduleSendMail.Rows[i]["Product_Code"].ToString();
                        string MaVatLieu = dtScheduleSendMail.Rows[i]["Material_Code"].ToString();

                        double SoNVLCanLanh = (dtScheduleSendMail.Rows[i]["amount_of_material_receive"] != null && dtScheduleSendMail.Rows[i]["amount_of_material_receive"].ToString() != "") ? double.Parse(dtScheduleSendMail.Rows[i]["amount_of_material_receive"].ToString()) : 0;
                        double SoNVLTrongKho = (dtScheduleSendMail.Rows[i]["Avaiable_Material_Quanity"] != null && dtScheduleSendMail.Rows[i]["Avaiable_Material_Quanity"].ToString() != "") ? double.Parse(dtScheduleSendMail.Rows[i]["Avaiable_Material_Quanity"].ToString()) : 0;


                        sqlcheck = @"select COUNT(*) from t_OCTD where TD02 = '" + MaDDH + "' and TD03 ='" + SoDDH + "' and TD04='" + MaLSX + "' and TD05='" + SoLSX
                         + "' and TD07 ='" + codeSanPham + "' and TD15 ='" + MaVatLieu + "'";
                        sqlCON check = new sqlCON();
                        if (int.Parse(check.sqlExecuteScalarString(sqlcheck)) == 0) //insert
                        {
                            string list = "";
                            for (int j = 0; j < dtScheduleSendMail.Columns.Count; j++)
                            {
                                list += "'";
                                list += dtScheduleSendMail.Rows[i][j].ToString() + "',";
                            }
                            StringBuilder sqlinsert = new StringBuilder();
                            sqlinsert.Append("insert into t_OCTD ");
                            sqlinsert.Append(@"(TD01,TD02,TD03,TD04,TD05,TD06,TD07,TD08,TD09,TD10,TD11,TD12,TD13,TD14,TD15,TD16,TD17,TD18,TD19,TD20,TD21,TD31,TD32,TD33,UserName,datetimeRST) values ( ");
                            sqlinsert.Append(list);
                            if (SoNVLTrongKho > SoNVLCanLanh)
                            {
                                sqlinsert.Append("'OK', 'OK' , '0' " + ",");
                            }
                            else if (SoNVLTrongKho == SoNVLCanLanh)
                            {
                                sqlinsert.Append("'OK', 'OK' , '1' " + ",");
                            }
                            else if (SoNVLTrongKho < SoNVLCanLanh)
                            {
                                sqlinsert.Append("'NG', 'NG' , '2' " + ",");

                            }
                            sqlinsert.Append("'" + Class.valiballecommon.GetStorage().UserName + "',GETDATE())");
                            sqlCON insert = new sqlCON();
                            insert.sqlExecuteNonQuery(sqlinsert.ToString(), false);
                        }
                        else //update
                        {

                            StringBuilder sqlupdate = new StringBuilder();
                            sqlupdate.Append("update t_OCTD set ");
                            sqlupdate.Append(@"TD18 = '" + dtScheduleSendMail.Rows[i]["amount_of_material_receive"].ToString() + "',");
                            sqlupdate.Append(@"TD18 = '" + dtScheduleSendMail.Rows[i]["amount_of_material_use"].ToString() + "',");
                            sqlupdate.Append(@"TD19 = '" + dtScheduleSendMail.Rows[i]["Avaiable_Material_Quanity"].ToString() + "',");
                            sqlupdate.Append(@"TD20 = '" + dtScheduleSendMail.Rows[i]["Production_Material_Quantity"].ToString() + "'");
                            if (SoNVLTrongKho > SoNVLCanLanh)
                            {
                                sqlupdate.Append(@", TD31 = 'OK' ,");
                                sqlupdate.Append(@" TD32 = 'OK' ,");
                                sqlupdate.Append(@"TD33 = '0', ");
                            }
                            else if (SoNVLTrongKho == SoNVLCanLanh)
                            {
                                sqlupdate.Append(@", TD31 = 'OK' ,");
                                sqlupdate.Append(@" TD32 = 'OK' ,");
                                sqlupdate.Append(@"TD33 = '1' ,");
                            }
                            else if (SoNVLTrongKho < SoNVLCanLanh)
                            {
                                sqlupdate.Append(@", TD31 = 'NG' ,");
                                sqlupdate.Append(@" TD32 = 'NG' ,");
                                sqlupdate.Append(@"TD33 = '2' ,");
                            }
                            sqlupdate.Append(@" UserName = '" + Class.valiballecommon.GetStorage().UserName + "' ,");
                            sqlupdate.Append(@" datetimeRST = GETDATE()");

                            sqlupdate.Append(@" where TD02 = '" + MaDDH + "' and TD03 ='" + SoDDH + "' and TD04='" + MaLSX + "' and TD05='" + SoLSX
                         + "' and TD07 ='" + codeSanPham + "' and TD15 ='" + MaVatLieu + "'");

                            sqlCON update = new sqlCON();
                            update.sqlExecuteNonQuery(sqlupdate.ToString(), false);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logfile.Output(StatusLog.Error, "Upload  data for Production fail : ", "Update or Insert to database fail: " + ex.Message, "Error");
                    MessageBox.Show("Update or Insert to database fail: " + ex.Message, "Error");
                }
            }
        }


        private void Btn_Start_Click(object sender, EventArgs e)
        { //pbar_upload.Visible = true;
            pbar_upload.Minimum =0;
            pbar_upload.Maximum = 0;
            pbar_upload.Value = 0;
            if (chb_uploadShipping.Checked)
            {
                pbar_upload.Maximum += 100;
            }
            if (chb_uploadProduction.Checked)
            {
                pbar_upload.Maximum += 100;
            }
            if (chb_uploadMaterial.Checked)
            {
                pbar_upload.Maximum += 100;
            }

            if (btn_Start.Text == "Start")
            {
                int timerInterval = (int)(num_hours.Value * 3600 + num_minutes.Value * 60 + num_seconds.Value) * 1000;

                tmrCallBgWorker.Interval = timerInterval;
                tmrCallBgWorker.Start();
                btn_Start.Text = "Starting";
            }
         else
            {
                tmrCallBgWorker.Stop() ;
                btn_Start.Text = "Start";
            }

            
        }
        private void UploadDatatoDatabase ()
        {
           

            if (checkInvalidDateTimeSpan(dtp_from.Value, dtp_todate.Value))
            {
               
                if (chb_uploadShipping.Checked)
                {
                    
                    try //Upload Shipping 
                    {
                      //  pbar_upload.Value += 10;
                        UploadDataShipping(dtp_from.Value, dtp_todate.Value);
                       // pbar_upload.Value += 90;
                //     pbar_upload.Update();

                    }
                    catch (Exception ex)
                    {

                        Logfile.Output(StatusLog.Error, "Upload data for Shipping fail : ", ex.Message);
                    }
                }

                if (chb_uploadProduction.Checked)
                {
                    try //Upload Production
                    {
                  //      pbar_upload.Value += 10;
                        UploadDataProduction(dtp_from.Value, dtp_todate.Value);
                    //    pbar_upload.Value += 90;
                   //     pbar_upload.Update();
                    }
                    catch (Exception ex)
                    {

                        Logfile.Output(StatusLog.Error, "Upload  data for Production fail : ", ex.Message);
                    }
                }
                if (chb_uploadMaterial.Checked)
                {
                    try //Upload Material 
                    {
                  //      pbar_upload.Value += 10;
                        UploadDataMaterial(dtp_from.Value, dtp_todate.Value);
                   //     pbar_upload.Value += 90;
                   //     pbar_upload.Update();
                    }
                    catch (Exception ex)
                    {

                        Logfile.Output(StatusLog.Error, "Upload  data for Production fail : ", ex.Message);
                    }
                }
                Logfile.Output(StatusLog.Error, "Upload  data just Finished  ! ");
             //   MessageBox.Show("Upload  data just Finished  ! ");
            }
        }
        private bool checkInvalidDateTimeSpan(DateTime from, DateTime to)
        {
            if(to.Date < from.Date)
            {
                MessageBox.Show("Please choose date to > date from", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
           
            return true;
        }

      

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveConfigure saveConfigure = new SaveConfigure();
            saveConfigure.fromdate = dtp_from.Value;
            saveConfigure.tomdate = dtp_todate.Value;
            saveConfigure.hours = (int)num_hours.Value;
            saveConfigure.minutes = (int)num_minutes.Value;
            saveConfigure.seconds = (int)num_seconds.Value;
            saveConfigure.UpdateShipping = chb_uploadShipping.Checked;
            saveConfigure.UpdateProduction= chb_uploadProduction.Checked;
            saveConfigure.UpdateMaterial = chb_uploadMaterial.Checked;
            try
            {
                SaveObject.Save_data(path + @"\Configure.ini", saveConfigure);
            }
            catch (Exception ex)
            {

                MessageBox.Show("Save configure fail: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
          
      
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SaveConfigure saveConfigure = new SaveConfigure();
            if (File.Exists(path + @"\Configure.ini"))
            {
                saveConfigure = (SaveConfigure)SaveObject.Load_data(path + @"\Configure.ini");

                if (saveConfigure != null)
                {
                    //dtp_from.Value = saveConfigure.fromdate;
                    //dtp_todate.Value = saveConfigure.tomdate;
                    num_hours.Value = saveConfigure.hours;
                    num_minutes.Value = saveConfigure.minutes;
                    num_seconds.Value = saveConfigure.seconds;
                    chb_uploadShipping.Checked = saveConfigure.UpdateShipping;
                    chb_uploadProduction.Checked = saveConfigure.UpdateProduction;
                    chb_uploadMaterial.Checked = saveConfigure.UpdateMaterial;
                }
                else
                {
                    dtp_from.Value = DateTime.Now.AddDays(-30);
                    dtp_todate.Value = DateTime.Now;
                    num_hours.Value = 0;
                    num_minutes.Value = 0;
                    num_seconds.Value = 10;
                    chb_uploadShipping.Checked = false;
                    chb_uploadProduction.Checked = false;
                   chb_uploadMaterial.Checked = false;
                }
            }
            else
            {
                dtp_from.Value =DateTime.Now.AddDays(-30);
                dtp_todate.Value = DateTime.Now;
                num_hours.Value = 0;
                num_minutes.Value = 0;
                num_seconds.Value = 10;
                chb_uploadShipping.Checked = false;
                chb_uploadProduction.Checked = false;
                chb_uploadMaterial.Checked = false;
            }
            dgv_show.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader;
            dgv_show.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgv_show.AutoGenerateColumns = true;
            dgv_show.DefaultCellStyle.Font = new Font("Verdana", 8, FontStyle.Regular);
            dgv_show.ColumnHeadersDefaultCellStyle.Font = new Font("Verdana", 8, FontStyle.Bold);
            dgv_show.AllowUserToAddRows = false;
            StringBuilder sql = new StringBuilder();
            sql.Append("select reportname, reporttype, hours, day, date, month,isBodyHTML,subject, attach, comments from t_report_schedule where 1=1 ");
            sqlCON tf = new sqlCON();
            tf.sqlDataAdapterFillDatatable(sql.ToString(), ref dtScheduleSendMail);
           
            dgv_show.DataSource = dtScheduleSendMail;
            dgv_show.Refresh();
            listReport= Listreport(dtScheduleSendMail);
            dtListEmail = new DataTable();
            StringBuilder sqllistmail = new StringBuilder();
            sqllistmail.Append("select emailaddress, deptcode, status, usingfunction from m_email where 1=1 ");
            tf = new sqlCON();
            tf.sqlDataAdapterFillDatatable(sqllistmail.ToString(), ref dtListEmail);
            listEmail = ListEmailNeedSend(dtListEmail);
        }
        
        private void Btn_add_Click(object sender, EventArgs e)
        {
            FormConfig.ReportSchechuleForm reportSchechule = new FormConfig.ReportSchechuleForm();
            reportSchechule.FormClosed += ReportSchechule_FormClosed;
            reportSchechule.ShowDialog();
        }

        private void ReportSchechule_FormClosed(object sender, FormClosedEventArgs e)
        {
            dtScheduleSendMail = new DataTable(); 
              StringBuilder sql = new StringBuilder();
            sql.Append("select reportname, reporttype, hours, day, date, month,isBodyHTML,subject, attach, comments from t_report_schedule where 1=1 ");
            sqlCON tf = new sqlCON();
            tf.sqlDataAdapterFillDatatable(sql.ToString(), ref dtScheduleSendMail);
            dgv_show.DataSource = dtScheduleSendMail;
            dgv_show.Refresh();
            listReport = new List<ScheduleReportItems>();
            listReport = Listreport(dtScheduleSendMail);


        }

        private void Btn_Remove_Click(object sender, EventArgs e)
        {
            if (dgv_show.RowCount > 0)
            {
                int rownumber = dgv_show.SelectedCells[0].RowIndex;
                string reportname = dgv_show.Rows[rownumber].Cells[0].Value.ToString();
                string sql = "delete from t_report_schedule where reportname = '" + reportname + "'";
                sqlCON connect = new sqlCON();
                connect.sqlExecuteNonQuery(sql, true);
                dtScheduleSendMail = new DataTable();
                StringBuilder sql2 = new StringBuilder();
                sql2.Append("select reportname, reporttype, hours, day, date, month,isBodyHTML, subject,attach, comments from t_report_schedule where 1=1 ");
                sqlCON tf = new sqlCON();
                tf.sqlDataAdapterFillDatatable(sql2.ToString(), ref dtScheduleSendMail);
                dgv_show.DataSource = dtScheduleSendMail;
                dgv_show.Refresh();
              
            }
        }

        private void Dgv_show_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv_show.RowCount > 0) { btn_Remove.Enabled = true; }
        }

        private void Btn_startSendmail_Click(object sender, EventArgs e)
        {
           
            if (btn_startSendmail.Text == "Start")
            {
                int timerInterval = (int)(nmr_hoursSendmail.Value * 3600 + nmr_minutesSendMail.Value * 60 + nmr_secondSendmail.Value) * 1000;

                tmrSendMail.Interval = timerInterval;
                tmrSendMail.Start();
                btn_startSendmail.Text = "Starting";
            }
            else
            {
                tmrSendMail.Stop();
                btn_startSendmail.Text = "Start";
            }
        }
        private List<ScheduleReportItems> Listreport (DataTable dt)
        {
            List<ScheduleReportItems> list = new List<ScheduleReportItems>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ScheduleReportItems item = new ScheduleReportItems();
                item.ReportName = dt.Rows[i]["reportname"].ToString();
                item.ReportType = dt.Rows[i]["reporttype"].ToString();
                item.Hours= dt.Rows[i]["hours"].ToString();
                item.Day = dt.Rows[i]["day"].ToString();
                item.Date = dt.Rows[i]["date"].ToString();
                item.Month = dt.Rows[i]["month"].ToString();
                item.IsBodyHTML = bool.Parse(dt.Rows[i]["isBodyHTML"].ToString());
                item.Subject = dt.Rows[i]["subject"].ToString();
                item.AttachedFolder = dt.Rows[i]["attach"].ToString();
                item.Contents = dt.Rows[i]["comments"].ToString();
                list.Add(item);
            }
            return list;
        }
        private List<EmailNeedSend> ListEmailNeedSend(DataTable dt)
        {
            List<EmailNeedSend> list = new List<EmailNeedSend>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                EmailNeedSend item = new EmailNeedSend();
                item.EmailReceive = dt.Rows[i]["emailaddress"].ToString();
                item.DepartmentCode = dt.Rows[i]["deptcode"].ToString();
                item.Status = dt.Rows[i]["status"].ToString();
                item.Function = dt.Rows[i]["usingfunction"].ToString();

                list.Add(item);
            }
            return list;
        }
        private bool SendEmailStatus()
        {
            foreach (var item in listReport)
            {
                if (item.ReportType == "Daily")
                {
                    if (DateTime.Now.Hour == int.Parse(item.Hours))
                    {
                        if (item.isSentMail == false)
                        {
                            SendMailFunction sendmail = new SendMailFunction();
                            if (sendmail.SendMailtoReport(item, listEmail))
                                item.isSentMail = true;
                        }
                    }
                    else if (DateTime.Now.Hour > int.Parse(item.Hours))
                    {
                        item.isSentMail = false;
                    }
                }
                else if (item.ReportType == "Weekly")
                {
                    if (DateTime.Now.DayOfWeek.ToString() == item.Day && DateTime.Now.Hour == int.Parse(item.Hours))
                    {
                        if (item.isSentMail == false)
                        {
                            SendMailFunction sendmail = new SendMailFunction();
                            if (sendmail.SendMailtoReport(item, listEmail))
                                item.isSentMail = true;
                        }
                    }
                    else if (DateTime.Now.DayOfWeek.ToString() == item.Day && DateTime.Now.Hour > int.Parse(item.Hours))
                    {
                        item.isSentMail = false;
                    }
                }
                else if (item.ReportType == "Monthly")
                {
                    if (DateTime.Now.Date.ToString() == item.Date && DateTime.Now.Hour == int.Parse(item.Hours))
                    {
                        if (item.isSentMail == false)
                        {
                            SendMailFunction sendmail = new SendMailFunction();
                            if (sendmail.SendMailtoReport(item, listEmail))
                                item.isSentMail = true;
                        }
                    }
                    else if (DateTime.Now.Date.ToString() == item.Date && DateTime.Now.Hour > int.Parse(item.Hours))
                    {
                        item.isSentMail = false;
                    }
                }
                else if (item.ReportType == "Yearly")
                {
                    if (DateTime.Now.ToString("MMMM") == item.Month && DateTime.Now.Date.ToString() == item.Date && DateTime.Now.Hour == int.Parse(item.Hours))
                    {
                        if (item.isSentMail == false)
                        {
                            SendMailFunction sendmail = new SendMailFunction();
                            if (sendmail.SendMailtoReport(item, listEmail))
                                item.isSentMail = true;
                        }
                    }
                    else if (DateTime.Now.ToString("MMMM") == item.Month && DateTime.Now.Date.ToString() == item.Date && DateTime.Now.Hour > int.Parse(item.Hours))
                    {
                        item.isSentMail = false;
                    }
                }

            }
            return false;
        }
        private void BgSendMailWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var worker = sender as BackgroundWorker;
            SendEmailStatus();

        }

        private void TmrSendMail_Tick(object sender, EventArgs e)
        {
            if (Monitor.TryEnter(lockObjectSendMail))
            {
                try
                {
                    // if bgworker is not busy the call the worker
                    if (!bgSendMailWorker.IsBusy)
                        bgSendMailWorker.RunWorkerAsync();
                }
                finally
                {
                    Monitor.Exit(lockObjectSendMail);
                }

            }
            else
            {

                // as the bgworker is busy we will start a timer that will try to call the bgworker again after some time
                tmrEnsureWorkerGetsCalled = new System.Threading.Timer(new TimerCallback(tmrEnsureWorkerSendmailGetsCalled_Callback), null, 0, 10);

            }
        }

        private void Rb_auto_CheckedChanged(object sender, EventArgs e)
        {
           if(rb_auto.Checked)
            {
                rb_manual.Checked = false;
            }
           else
            {
                rb_auto.Checked = true;
            }
        }

        private void Rb_manual_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_manual.Checked)
            {
                rb_auto.Checked = false;
            }
            else
            {
                rb_auto.Checked = true;
            }
        }
    }
}
