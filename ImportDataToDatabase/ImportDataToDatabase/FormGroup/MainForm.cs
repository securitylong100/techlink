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
        int c;
        string[] filespath;

        DataTable table;
        private string path = Environment.CurrentDirectory + "\\";
        string version = "";
        public MainForm()
        {
            InitializeComponent();
            table = new DataTable();
            version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Major.ToString(); //AssemblyVersion을 가져온다.
            version += "." + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Minor.ToString();
            version += "." + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Build.ToString();
            Text = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name + " " + version;
            Logfile.Output(StatusLog.Normal, "Starting ", Text);
        }

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
                int num = CounterCSV(txtFolderSource.Text);
            }
        }

        //COUNTER NUMBER OF CSV FILES IN SOURCE FOLDER
        private int CounterCSV(string path)
        {
            filespath = Directory.GetFiles(path, "*.csv");
            int num = filespath.Count();
            lbNumCSV.Text = num.ToString();
            return num;
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
                var value = line.Split('|');
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


                if (files != null)
                {
                    lbl_NG.Text = "NG";
                    lbl_OK.Text = "OK";
                    lbl_barcode.Text = "Barcode: ";
                    foreach (string file in files)
                    {
                        if (ReadCSV(file, 14, ref table))
                        {

                            ImportToDB(file);
                            lbl_OK.Text = "OK: " + CounterỌKERP(ref table).ToString();
                            lbl_NG.Text = "NG: " + CounterNGERP(ref table).ToString();
                            lbl_barcode.Text = "Barcode: " + table.Rows[0]["lot"].ToString();
                            string code = table.Rows[0]["serno"].ToString().Split('-')[0];
                            string No = table.Rows[0]["serno"].ToString().Split('-')[1];
                            string typeNG = "";
                            //An Them Code Check Nguyen Vat Lieu ngay 10/05/2019
                            string MaLSX = code + "-" + No;
                            Material material = new Material();
                            bool IsDuSoLuong = false;
                            bool IsNVL = false;
                            List<string> _messages = new List<string>();
                            List<MaterialAdapt> listMaterial = new List<MaterialAdapt>();
                            double SL_UPload = CounterỌKERP(ref table) + CounterNGERP(ref table);
                            //Chua ma Lenh San xuat vao 2 truong dang dua vao
                            bool IsResultheck = material.KiemtraNguyenVatLieu(code, No, SL_UPload, out IsDuSoLuong, out IsNVL, out listMaterial, out _messages);
                            insertERP classinsert = new insertERP();
                            DefectClass defectClass = new DefectClass();
                            if (IsResultheck == true)
                            {

                                classinsert.InsertdataToERP(table.Rows[0]["lot"].ToString(), CounterỌKERP(ref table).ToString(), CounterNGERP(ref table).ToString());
                                classinsert.updateERP(table.Rows[0]["lot"].ToString());
                                classinsert.updateERPMQC(table.Rows[0]["serno"].ToString());
                                classinsert.InsertdataToSFT(table.Rows[0]["lot"].ToString(), CounterỌKERP(ref table).ToString(), CounterNGERP(ref table).ToString());
                                classinsert.UpdatedataToSFT(table.Rows[0]["lot"].ToString());

                                for (int i = 0; i < table.Rows.Count; i++)
                                {
                                    typeNG = table.Rows[i]["item"].ToString();
                                    string SL = table.Rows[i]["data"].ToString();
                                    
                                    if (int.Parse(SL) > 0 && typeNG.Contains("NG")) 
                                    {
                                        var insert = defectClass.InsertToSFT_OP_EXCEPT(MaLSX, typeNG, int.Parse(SL));
                                    }
                                }

                            }
                            else //insert to Temperate database
                            {
                                classinsert.InsertToERPMQC_Error(table, CounterỌKERP(ref table).ToString(), "OP");
                                classinsert.InsertToERPMQC_Error(table, CounterNGERP(ref table).ToString(), "NG");
                            }
                            table.Clear();
                          File.Delete(file);
                        }
                        else MoveToFolder(file);
                    }
                    UpdateFromERPMQC_ErrorToSFT_ERP();
                }
                CounterCSV(txtFolderSource.Text);
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
                    bool IsResultheck = material.KiemtraNguyenVatLieu(code, No, 0, out IsDuSoLuong, out IsNVL, out listMaterial, out _messages);
                    if (IsNVL)
                    {//Update Status
                        string test = dtTable.Rows[i]["remark"].ToString();
                        int countOK = dtTable.Rows[i]["remark"].ToString() == "OP" ? int.Parse(dtTable.Rows[i]["data"].ToString()) : 0;
                        int countNG = dtTable.Rows[i]["remark"].ToString() == "NG" ? int.Parse(dtTable.Rows[i]["data"].ToString()) : 0;
                        insertERP classinsert = new insertERP();
                        classinsert.InsertdataToERP(dtTable.Rows[0]["lot"].ToString(), countOK.ToString(), countNG.ToString());
                        classinsert.updateERP(dtTable.Rows[0]["lot"].ToString());
                        classinsert.UpdateToERPMQC_Error("OK", dtTable.Rows[0]["serno"].ToString());
                        classinsert.InsertdataToSFT(dtTable.Rows[0]["lot"].ToString(), countOK.ToString(), countNG.ToString());
                        classinsert.UpdatedataToSFT(dtTable.Rows[0]["lot"].ToString());
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
            c = (int)numTimer.Value;
            if (bwSendData.IsBusy) bwSendData.CancelAsync();
            else bwSendData.RunWorkerAsync();

        }

        //COUNTING....
        private void bwSendData_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int i = c; i >= 0; i--)
            {
                bwSendData.ReportProgress(i);
                if (bwSendData.CancellationPending)
                {
                    e.Cancel = true;
                    bwSendData.ReportProgress(c);
                    return;
                }
                Thread.Sleep(1000);
            }
        }

        //UPDATE COUNTER EACH 1S
        private void bwSendData_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            tsStatus.Text = "Counting for send...";
            tsTimer.Text = e.ProgressPercentage.ToString() + " s";
        }

        //UPDATE STATUS
        private void bwSendData_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {


                //IF CLICK BUTTON STOP
                if (e.Cancelled)
                    tsStatus.Text = "Stop send data";
                //IF ERROR WHILE SEND
                else if (e.Error != null)
                    tsStatus.Text = "Error while send data";
                //IF COUNTER = 0 AND SEND DATA TO DB
                else
                {
                    tsStatus.Text = "Sending data...";
                    CompareFormat(filespath);

                    exportcsvToPLC();
                    bwSendData.RunWorkerAsync();

                }
            }
            catch (Exception ex)
            {
                Logfile.Output(StatusLog.Error, "bwSendData_RunWorkerCompleted", ex.Message);

            }
        }

        //BUTTON STOP FOR STOP SEND DATA
        private void btnStop_Click(object sender, EventArgs e)
        {
            if (bwSendData.IsBusy)
            {
                btnStart.Enabled = true;
                btnStop.Enabled = false;
                btnExit.Enabled = true;
                numTimer.Enabled = false;
                bwSendData.CancelAsync();
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
                    builder.AppendLine(string.Join("\t", cols.ToArray()));
                }
                System.IO.File.WriteAllText(path + "ListProduct.csv", builder.ToString());
            }
            catch (Exception ex)
            {

                Logfile.Output(StatusLog.Error, "exportcsvToPLC()", ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //InsertdataToSFT(string barcode, string output, string NG)
            DefectClass defect = new DefectClass();
            //classinsert.InsertdataToSFT("B511-1910013;0010;B01;B01", "2", "1");
            //  var test = defect.listNGMapping("B01");
            //  var insert = defect.InsertDefect2SFT_OP_EXCEPT("**** - 1901009", 1, 0, "B01---B01", "ERP", "defect", "SAI LOGO",
            //   "MO", "10", "B01", 0, 0);
            //  var strarray = defect.GetDefectItemFromSFT("0400500301");
            var insert = defect.InsertToSFT_OP_EXCEPT("****- 1901015", "NG2", 5);

        }
    }
}