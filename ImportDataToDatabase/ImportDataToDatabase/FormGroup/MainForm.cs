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
    public partial class MainForm : Form
    {

        string[] filespath;

        DataTable table;
        //private string path = Environment.CurrentDirectory + "\\";
        private string path = @"\\172.16.0.5\Program\export" + "\\";
        string version = "";
        //Khai bao background worker
        BackgroundWorker bgWorker;
        // this timer calls bgWorker again and again after regular intervals
        System.Windows.Forms.Timer tmrCallBgWorker;
        // this is the timer to make sure that worker gets called
        System.Threading.Timer tmrEnsureWorkerGetsCalled;
        // object used for safe access
        object lockObject = new object();
        System.Windows.Forms.Timer tmrCountdown;
        //Bien them vao
        int intCountCSV = 0;
        int intCountOK = 0;
        int intCountNG = 0;
        int CountTimer = 0;
        string strLot = "";

        

        public MainForm()
        {
            InitializeComponent();
            table = new DataTable();
            version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Major.ToString(); //AssemblyVersion을 가져온다.
            version += "." + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Minor.ToString();
            version += "." + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Build.ToString();
            Text = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name + " " + version;
            Logfile.Output(StatusLog.Normal, "Starting ", Text);
            // this is our worker
            bgWorker = new BackgroundWorker();
           

            // work happens in this method
            bgWorker.DoWork += new DoWorkEventHandler(bg_DoWork);
            bgWorker.ProgressChanged += BgWorker_ProgressChanged;
            bgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bg_RunWorkerCompleted);
            bgWorker.WorkerReportsProgress = true;
            //timer_update.Start();
            // this timer calls bgWorker again and again after regular intervals
            tmrCallBgWorker = new System.Windows.Forms.Timer();//Timer for do task
            tmrCallBgWorker.Tick += new EventHandler(tmrCallBgWorker_Tick);
            tmrCountdown = new System.Windows.Forms.Timer();
            tmrCountdown.Tick += new EventHandler(TmrCountdown_Tick); 
            // tmrCallBgWorker.Interval = 10000;
        }

        private void TmrCountdown_Tick(object sender, EventArgs e)
        {
            tmrCountdown.Stop();

            if (CountTimer == 0)
            {
                tsTimer.Text = CountTimer.ToString() + "s ";
                CountTimer = (int)numTimer.Value;
            }
            else
            {
                tsTimer.Text = CountTimer.ToString() + "s ";
                CountTimer--;
            }
            tmrCountdown.Start();
        }
        #region Backround worker task
        void bg_DoWork(object sender, DoWorkEventArgs e)
        {


            var worker = sender as BackgroundWorker;
            worker.ReportProgress(10);
            intCountCSV =  CounterCSV(txtFolderSource.Text);
            if (intCountCSV > 0)
            {
                worker.ReportProgress(30);
                CompareFormat(filespath);
                worker.ReportProgress(70);
            }
            exportcsvToPLC();
            worker.ReportProgress(100);
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
        private void BgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage == 10)
            {
                lbNumCSV.Text = "0";
                lbl_OK.Text = "";
                lbl_NG.Text = "";
                lbl_barcode.Text = "";
                tsStatus.Text = "Waiting file...";
            }
            else if (e.ProgressPercentage == 30)
            {
                tsStatus.Text = "Find down files to update";
                lbNumCSV.Text = intCountCSV.ToString();
            }
            else if (e.ProgressPercentage == 70)
            {
                lbl_OK.Text = "OK: " + intCountOK.ToString();
                lbl_NG.Text = "NG: " + intCountNG.ToString();
                lbl_barcode.Text = "Barcode: " + strLot;
                tsStatus.Text = "Update finished";
            }
            else if (e.ProgressPercentage == 100)
            {
              
                tsStatus.Text = "Waiting file...";
            }



        }

        void bg_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //  LoadcbInUI();
            try
            {


                //IF CLICK BUTTON STOP
                if (e.Cancelled)
                    tsStatus.Text = "Stop send data";
                //IF ERROR WHILE SEND
                else if (e.Error != null)
                    tsStatus.Text = "Error while send data";
                //IF COUNTER = 0 AND SEND DATA TO DB
              
            }
            catch (Exception ex)
            {
                Logfile.Output(StatusLog.Error, "bwSendData_RunWorkerCompleted", ex.Message);

            }

        }
      
        #endregion backround worker task
        //GET SETTING FILE
        private void MainForm_Load(object sender, EventArgs e)
        {
            string fileset = path + @"setting.txt";
            if (File.Exists(fileset))
            {
                foreach (string line in File.ReadAllLines(fileset))
                {
                    if (line.Contains("source=")) txtFolderSource.Text
                            = line.Remove(0, 7);
                    if (line.Contains("save=")) txtFolderSave.Text
                            = line.Remove(0, 5);
                    if (line.Contains("timer=")) numTimer.Text
                            = line.Remove(0, 6);
                }
                Logfile.Output(StatusLog.Normal, "Loaded Configure");
            }

        }

        //SELECT SOURCE FOLDER
        private void btnFSource_Click(object sender, EventArgs e)
        {
            OpenFileDialog FSopen = new OpenFileDialog();
            FSopen.CheckFileExists = false;
            FSopen.CheckPathExists = false;
            FSopen.ShowReadOnly = true;
            FSopen.FileName = "Select Folder";
            if (FSopen.ShowDialog() == DialogResult.OK)
            {
                txtFolderSource.Text = Path.GetDirectoryName(FSopen.FileName) + @"\";
                intCountCSV = CounterCSV(txtFolderSource.Text);
            }
        }

        //COUNTER NUMBER OF CSV FILES IN SOURCE FOLDER
        private int CounterCSV(string path)
        {
            int intReturn = 0;
            if (Directory.Exists(path))
            {

                filespath = Directory.GetFiles(path, "*.csv");
                intReturn = filespath.Count();
                intCountCSV = intReturn;
            }
            return intReturn;
        }

        //SELECT FOLDER FOR SAVE WRONG FORMAT FILE
        private void btnFSave_Click(object sender, EventArgs e)
        {
            OpenFileDialog FSopen = new OpenFileDialog();
            FSopen.CheckFileExists = false;
            FSopen.CheckPathExists = false;
            FSopen.ShowReadOnly = true;
            FSopen.FileName = "Select Folder";
            if (FSopen.ShowDialog() == DialogResult.OK)
            {
                txtFolderSave.Text = Path.GetDirectoryName(FSopen.FileName) + @"\";
            }
        }

        //READ CSV AND COMPARE NUMBER OF COLUMNS
        private Boolean ReadCSV(string pathfile, int numcol, ref DataTable dt)
        {
            StreamReader reader = new StreamReader(pathfile, false);
            dt = new DataTable();
            dt.Columns.Add("serno");
            dt.Columns.Add("lot");
            dt.Columns.Add("model");
            dt.Columns.Add("site");
            dt.Columns.Add("factory");
            dt.Columns.Add("line");
            dt.Columns.Add("process");
            dt.Columns.Add("item");
            dt.Columns.Add("inspectdate");
            dt.Columns.Add("inspecttime");
            dt.Columns.Add("data");
            dt.Columns.Add("judge");
            dt.Columns.Add("status");
            dt.Columns.Add("remark");

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var value = line.Split(',');
                if (value.Count() == numcol)
                    dt.Rows.Add(value);
            }
            reader.Close();
            if (dt.Rows.Count == 0) return false;
            else return true;
        }

        //IMPORT DATA FROM CSV TO DB
        private void ImportToDB(string file)
        {
            try
            {
                SQLCommon sql = new SQLCommon();
                sql.InsertCSVToDatabase(ref table, SQLCommon.ERPSOFT);
            }
            catch
            {
                MoveToFolder(file);
            }
        }

        //MOVE FILE WRONG FORMAT TO SAVE FOLER
        private void MoveToFolder(string file)
        {
            string tofile = txtFolderSave.Text + Path.GetFileName(file);
            File.Move(file, tofile);
        }

        //COMPARE FORMAT FILE
        private void CompareFormat(string[] files)
        {
            try
            {


                if (intCountCSV > 0)
                {
                    //lbl_NG.Text = "NG";
                    //lbl_OK.Text = "OK";
                    //lbl_barcode.Text = "Barcode: ";
                    foreach (string file in files)
                    {
                        try
                        {
                      
                        if (ReadCSV(file, 14, ref table))
                        {

                           // ImportToDB(file);
                            intCountOK = CounterỌKERP(ref table);
                            intCountNG = CounterNGERP(ref table);
                            strLot = table.Rows[0]["lot"].ToString();
                            string code = table.Rows[0]["serno"].ToString().Split('-')[0];
                            string No = table.Rows[0]["serno"].ToString().Split('-')[1];
                            string typeNG = "";
                            string DateUp = DateTime.Now.ToString("yyyyMMdd");
                            string TImeUp = DateTime.Now.ToString("HH:mm:ss");
                            //An Them Code Check Nguyen Vat Lieu ngay 10/05/2019
                            string MaLSX = code + "-" + No;
                            Material material = new Material();
                            bool IsDuSoLuong = false;
                            bool IsNVL = false;
                            List<string> _messages = new List<string>();
                            List<MaterialAdapt> listMaterial = new List<MaterialAdapt>();
                            double SL_UPload = intCountOK + intCountNG;
                            //Chua ma Lenh San xuat vao 2 truong dang dua vao
                            bool IsResultheck = material.KiemtraNguyenVatLieu(code, No, SL_UPload, out IsDuSoLuong, out IsNVL, out listMaterial, out _messages);
                            insertERP classinsert = new insertERP();
                            classinsert.InsertToERPMQC(table);
                            DefectClass defectClass = new DefectClass();
                            if (IsResultheck == true)
                            {

                                classinsert.InsertdataToERP(table.Rows[0]["lot"].ToString(), intCountOK.ToString(), intCountNG.ToString(),DateUp, TImeUp);
                                classinsert.updateERP(table.Rows[0]["lot"].ToString());                              
                                classinsert.InsertdataToSFT(table.Rows[0]["lot"].ToString(), intCountOK.ToString(), intCountNG.ToString(),DateUp, TImeUp);
                                classinsert.UpdatedataToSFT(table.Rows[0]["lot"].ToString());
                               

                                for (int i = 0; i < table.Rows.Count; i++)
                                {
                                    typeNG = table.Rows[i]["item"].ToString();
                                    string SL = table.Rows[i]["data"].ToString();
                                    
                                    if (int.Parse(SL) > 0 && typeNG.Contains("NG")) 
                                    {
                                        var insert = defectClass.InsertToSFT_OP_EXCEPT(MaLSX, typeNG, int.Parse(SL), classinsert.Sequence_OP_REAL_RUN);
                                    }
                                }
                                classinsert.updateERPMQC(table.Rows[0]["serno"].ToString());
                                classinsert.UpdateToERPMQC_Error("OK", table.Rows[0]["serno"].ToString());
                            }
                            else //insert to Temperate database
                            {
                                classinsert.InsertToERPMQC_Error(table);
                              
                            }
                            table.Clear();
                          File.Delete(file);
                        }
                        else MoveToFolder(file);
                        }
                        catch (Exception ex)
                        {
                            MoveToFolder(file);

                        }
                    }
                    UpdateFromERPMQC_ErrorToSFT_ERP();
                }
              
            }
            catch (Exception ex)
            {
              

                Logfile.Output(StatusLog.Error, "CompareFormat(string[] files)", ex.Message);
            }
        }

        //Counter OK < chưa dung để đó>
        private void UpdateFromERPMQC_ErrorToSFT_ERP()
        {
            try
            {


                StringBuilder sqlquery = new StringBuilder();
                sqlquery.Append(@"select distinct
serno,lot,model,site,factory,line,process,item,inspectdate,inspecttime,data,judge,status,remark
from m_ERPMQC_Error 
where status !='OK' ");
                SQLCommon data = new SQLCommon();
                DataTable dtTable = new DataTable();
                data.sqlDataAdapterFillDatatable(sqlquery.ToString(), ref dtTable, SQLCommon.ERPSOFT);

                for (int i = 0; i < dtTable.Rows.Count; i++)
                {
                    Material material = new Material();
                    bool IsDuSoLuong = false;
                    bool IsNVL = false;
                    List<string> _messages = new List<string>();
                    List<MaterialAdapt> listMaterial = new List<MaterialAdapt>();
                    //Chua ma Lenh San xuat vao 2 truong dang dua vao
                    string code = dtTable.Rows[i]["serno"].ToString().Split('-')[0];
                    string No = dtTable.Rows[i]["serno"].ToString().Split('-')[1];
                    string DateUp = dtTable.Rows[i]["inspectdate"].ToString().Replace("-", "").Substring(0,8);
                    string TimeUp = dtTable.Rows[i]["inspecttime"].ToString().Substring(0, 8);
                    string typeNG = "";
                    string MaLSX = code + "-" + No;
                    bool IsResultheck = material.KiemtraNguyenVatLieu(code, No, 0, out IsDuSoLuong, out IsNVL, out listMaterial, out _messages);
                    DefectClass defectClass = new DefectClass();
                    if (IsNVL)
                    {//Update Status
                        string test = dtTable.Rows[i]["remark"].ToString();
                        int countOK = dtTable.Rows[i]["remark"].ToString() == "OP" ? int.Parse(dtTable.Rows[i]["data"].ToString()) : 0;
                        int countNG = dtTable.Rows[i]["remark"].ToString() == "NG" ? int.Parse(dtTable.Rows[i]["data"].ToString()) : 0;
                        insertERP classinsert = new insertERP();
                        classinsert.InsertdataToERP(dtTable.Rows[0]["lot"].ToString(), countOK.ToString(), countNG.ToString(),DateUp, TimeUp);
                        classinsert.updateERP(dtTable.Rows[0]["lot"].ToString());                     
                        classinsert.InsertdataToSFT(dtTable.Rows[0]["lot"].ToString(), countOK.ToString(), countNG.ToString(), DateUp, TimeUp);
                        classinsert.UpdatedataToSFT(dtTable.Rows[0]["lot"].ToString());
                        for (int j = 0; j < table.Rows.Count; i++)
                        {
                            typeNG = table.Rows[j]["item"].ToString();
                            string SL = table.Rows[j]["data"].ToString();

                            if (int.Parse(SL) > 0 && typeNG.Contains("NG"))
                            {
                                var insert = defectClass.InsertToSFT_OP_EXCEPT(MaLSX, typeNG, int.Parse(SL), classinsert.Sequence_OP_REAL_RUN);
                            }
                        }
                        classinsert.updateERPMQC(dtTable.Rows[0]["serno"].ToString());
                        classinsert.UpdateToERPMQC_Error("OK", dtTable.Rows[0]["serno"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                Logfile.Output(StatusLog.Error, "UpdateFromERPMQC_ErrorToSFT_ERP ()", ex.Message);
            }

        }

        public int CounterỌKERP(ref DataTable dt)
        {
            int OK = 0;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["remark"].ToString() == "OP") OK += int.Parse(dt.Rows[i]["data"].ToString());
            }
            return OK;
        }
        //Counter NG < chưa dung để đó>
        public int CounterNGERP(ref DataTable dt)
        {
            int NG = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["remark"].ToString() == "NG") NG += int.Parse(dt.Rows[i]["data"].ToString());
            }
            return NG;
        }
        #region SETTING TIMER FOR SEND DATA
        //BUTTON START FOR START COUNTING
        private void btnStart_Click(object sender, EventArgs e)
        {
            btnStart.Enabled = false;
            btnStop.Enabled = true;
            btnExit.Enabled = false;
            numTimer.Enabled = false;
            int timerInterval = (int)(numTimer.Value) * 1000;
            tmrCallBgWorker.Interval = timerInterval;
            tmrCallBgWorker.Start();
            tmrCountdown.Interval = 1000;
            tmrCountdown.Start();
            CountTimer = (int)numTimer.Value;
            //c = (int)numTimer.Value;
            //if (bwSendData.IsBusy) bwSendData.CancelAsync();
            //else bwSendData.RunWorkerAsync();

        }

        //COUNTING....
      

        //UPDATE COUNTER EACH 1S
        private void bwSendData_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            tsStatus.Text = "Counting for send...";
            tsTimer.Text = e.ProgressPercentage.ToString() + " s";
        }

        //UPDATE STATUS
      

        //BUTTON STOP FOR STOP SEND DATA
        private void btnStop_Click(object sender, EventArgs e)
        {
         //   if (bwSendData.IsBusy)
            {
                btnStart.Enabled = true;
                btnStop.Enabled = false;
                btnExit.Enabled = true;
                numTimer.Enabled = true;
                tmrCallBgWorker.Stop();
                tmrCountdown.Stop();
                // bwSendData.CancelAsync();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //WHILE TIMER COUTING NOT ALLOW FOR EXIT APPLICATION
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!btnExit.Enabled) e.Cancel = true;
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            string fileset = path + @"setting.txt";
            using (StreamWriter sw = new StreamWriter(fileset))
            {
                sw.WriteLine("source=" + txtFolderSource.Text);
                sw.WriteLine("save=" + txtFolderSave.Text);
                sw.WriteLine("timer=" + numTimer.Text);
                sw.Close();
            }
        }
        #endregion

        //export excel to PLC 
        void exportcsvToPLC()
        {
            try
            {


                File.Delete(path + "ListProduct.csv");
                StringBuilder sql = new StringBuilder();
                sql.Append(@"
select TA001+'-'+RTRIM(TA002)+';'+TA003+';'+TA004+';'+ RTRIM(TA006),  RTRIM(TC047),RTRIM(TA010),  RTRIM(TA011), RTRIM(TA012)  ,TA001,TA002,TA003,TA004, TA006 from SFCTA 
left join SFCTC on TA001 = TC004 and TA002 = TC005
where 1=1
and TA004 = 'B01' and TA001 = 'B511'
and TA011+TA012 <TA010
group by TA001,TA002,TA003,TA004, TA006, TA010, TA011,TA012, TC047
");
                DataTable dtshow = new DataTable();
                SQLCommon data = new SQLCommon();
                data.sqlDataAdapterFillDatatable(sql.ToString(), ref dtshow, SQLCommon.ERP_TEST);

                StringBuilder builder = new StringBuilder();
                int rowcount = dtshow.Rows.Count;
                int columncount = dtshow.Columns.Count;
                List<string> cols = new List<string>();

                // builder.AppendLine(string.Join("\t", cols.ToArray()));
                for (int i = 0; i < rowcount; i++)
                {
                    cols = new List<string>();
                    for (int j = 0; j < 5; j++) //Chỉ lay 4 cọt đâu thôi, yêu cầu của Đức
                    {
                        cols.Add(dtshow.Rows[i][j].ToString() + @",");
                    }
                    builder.AppendLine(string.Join("", cols.ToArray()));
                }
                System.IO.File.WriteAllText(path + "ListProduct.csv", builder.ToString());
            }
            catch (Exception ex)
            {

                Logfile.Output(StatusLog.Error, "exportcsvToPLC()", ex.Message);
            }
        }

   

       
    }
}