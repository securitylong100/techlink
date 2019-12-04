using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;
using WindowsFormsApplication1.MQC.Report;

namespace WindowsFormsApplication1.MQC
{
    public partial class ProductionMain :MetroForm
    {
        bool isStartup = false;
        DateTime dateTimeFrom;
        DateTime dateTimeTo;
        string dept = "";
        string process = "";
        BackgroundWorker bgWorker;
        // this timer calls bgWorker again and again after regular intervals
        System.Windows.Forms.Timer tmrCallBgWorker;
        // this is the timer to make sure that worker gets called
        System.Threading.Timer tmrEnsureWorkerGetsCalled;
        // object used for safe access
        object lockObject = new object();
        MQCItem1 MQCItem1 = new MQCItem1();
        List<MQCItem1> ListMQCshow = new List<MQCItem1>();
        List<MQCItem1> ListMQCTake= new List<MQCItem1>();
        int CountColumn = Properties.Settings.Default.IntLayoutMQCColumn;
        int CountRow = Properties.Settings.Default.intLayoutMQCRows;
        int width = 0; int height = 0;
        LineUI line = new LineUI(null,null,12);
        public string pathMonth = Environment.CurrentDirectory + @"\Resources\Month.xls";
        public ProductionMain()
        {
            InitializeComponent();
            IntializeforTableLayout();
         
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
           
        }
        #region Backround worker task
        void bg_DoWork(object sender, DoWorkEventArgs e)
        {

            var worker = sender as BackgroundWorker;
           
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



        }

        void bg_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                LoadDataERPMQCToShow();
            }
            catch (Exception ex)
            {

                Log.Logfile.Output(Log.StatusLog.Error, "LoadDataERPMQCToShow()", ex.Message);
            }
          

        }
        #endregion backround worker task
        public void IntializeforTableLayout ()
        {
            layoutMain.Controls.Clear();
            layoutMain.ColumnCount = CountColumn;
            layoutMain.RowCount = CountRow;
            float Percerntwidth = (float)(100 / layoutMain.ColumnCount);
            float Percerntheight= (float)(100 / layoutMain.RowCount);
            layoutMain.ColumnStyles.Clear();
            for (int i = 0; i < layoutMain.ColumnCount-1; i++)
            {
                layoutMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, Percerntwidth));
            }
            layoutMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,100 - (Percerntwidth* (layoutMain.ColumnCount - 1))));
            layoutMain.RowStyles.Clear();
            for (int i = 0; i < layoutMain.RowCount-1; i++)
            {
                layoutMain.RowStyles.Add(new RowStyle(SizeType.Percent, Percerntheight));
            }
            layoutMain.RowStyles.Add(new RowStyle(SizeType.Percent,100- Percerntheight *(layoutMain.RowCount - 1)));
            for (int i = 0; i < layoutMain.ColumnCount; i++)
            {
                for (int j = 0; j < layoutMain.RowCount; j++)
                {
                   // line.Dispose();
                    if (this.WindowState == FormWindowState.Normal)
                    line = new LineUI(MQCItem1,cb_Department.Text,20);
                    else if (this.WindowState == FormWindowState.Maximized)
                        line = new LineUI(MQCItem1, cb_Department.Text, 25);

                    line.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
                    layoutMain.Controls.Add(line, i, j);
                    
                }
            }
            isStartup = true;


        }
        private void SettingTimerForBrwoker()
        {
            int timerInterval = Properties.Settings.Default.intTimerMQC;

            tmrCallBgWorker.Interval = timerInterval;
            tmrCallBgWorker.Start();
        }
        private void ProductionMain_Load(object sender, EventArgs e)
        { LoaddataConfigure loaddata = new LoaddataConfigure();
            List<DeptCodeName> deptCodeNames = loaddata.deptCodeNames(ref cb_Department);
            cb_Department.SelectedItem = "[B01] - 胶管OEM生产线ONGOEM";
            SettingTimerForBrwoker();
            SettingTimeFromDateTodate();
            LoadDataERPMQCToShow();
            lb_Clock.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
            this.WindowState = FormWindowState.Maximized;
        }

        private void ProductionMain_Resize(object sender, EventArgs e)
        {

       
            if (isStartup)
            {
                if (this.WindowState == FormWindowState.Maximized )
                {
                    cb_Department.Font = new Font("Times New Roman", 35, FontStyle.Bold);
                    lb_Clock.Font = new Font("Times New Roman", 30, FontStyle.Bold);
                }
                else if (this.WindowState == FormWindowState.Normal && this.Size.Width == width )
                {
                    cb_Department.Font = new Font("Times New Roman", 20, FontStyle.Bold);
                    lb_Clock.Font = new Font("Times New Roman", 20, FontStyle.Bold);
                }
               else
                {
                    width = this.Size.Width;
                    height = this.Size.Height;
                }
            }
           else
            {
                width = this.Size.Width;
                height = this.Size.Height;
            }
        }
        private void LoadDataERPMQCToShow()
        {
            //Load data from m_ERPMQC
            LoadDataMQC dataMQC = new LoadDataMQC();
            dept = "B01";
            process = "MQC";
            ListMQCshow = dataMQC.listMQCItemsOfDept(dateTimeFrom, dateTimeTo, dept, process);
            int topCount = CountColumn * CountRow;
            ListMQCTake = ListMQCshow.Take(topCount).ToList();
            layoutMain.Controls.Clear();

            if (ListMQCTake.Count <= topCount)
            {
                int countList = 0;
                for (int i = 0; i < CountRow; i++)
                {
                    for (int j = 0; j < CountColumn; j++)
                    {
                        if (countList < ListMQCTake.Count)
                        {
                         //   line.Dispose();
                            if (this.WindowState == FormWindowState.Normal)
                                line = new LineUI(ListMQCTake[countList], cb_Department.Text, 20);
                            else if (this.WindowState == FormWindowState.Maximized)
                                line = new LineUI(ListMQCTake[countList], cb_Department.Text, 25);
                            line.Name = ListMQCTake[countList].product;

                            line.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                    | System.Windows.Forms.AnchorStyles.Left)
                    | System.Windows.Forms.AnchorStyles.Right)));

                            layoutMain.Controls.Add(line, j, i);
                            countList++;
                        }
                    }
                }
            }


            lb_Clock.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");

        }
        private void SettingTimeFromDateTodate()
        {
            //Setting DateTime from and To to limit data in database
            var lastDayOfMonth = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);//avoid end of month
            if (DateTime.Now.Day < lastDayOfMonth)
            {
                dateTimeFrom = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 6, 0, 0);
                dateTimeTo = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day + 1, 6, 0, 0);
            }
            else if (DateTime.Now.Day == lastDayOfMonth)
            {
                if (DateTime.Now.Month < 12)
                {
                    dateTimeFrom = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 6, 0, 0);
                    dateTimeTo = new DateTime(DateTime.Now.Year, DateTime.Now.Month + 1, 1, 6, 0, 0);
                }
                else if (DateTime.Now.Month == 12) //avoid 31/12
                {
                    dateTimeFrom = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 6, 0, 0);
                    dateTimeTo = new DateTime(DateTime.Now.Year + 1, 1, 1, 6, 0, 0);
                }
            }
           
        }

        private void Btn_Summary_Click(object sender, EventArgs e)
        {
            //         LoadDataSummary loadDataSummary = new LoadDataSummary();

            //List<MQCItemSummary> itemSummaries = loadDataSummary.GetMQCItemSummaries(dateTimeFrom, dateTimeTo, dept, process);

            //         SummaryWindow summary = new SummaryWindow(itemSummaries);
            //         summary.Show();

            DefectRateReport defectRateReport = new DefectRateReport();
            DateTime date_from = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1, 0, 0, 0);
            DateTime date_to = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 30, 0, 0, 0);
            DefectRateData defectRateData = new DefectRateData();
            defectRateData = defectRateReport.GetDefectRateReport(date_from, date_to, "B01", "0010");
            Class.ToolSupport exportExcel = new Class.ToolSupport();
            exportExcel.ExportToTemplateMQCDefect(pathMonth, @"C:\ERP_Temp\MQC_Report" + "-" + DateTime.Now.ToString("yyyyMMdd hhmmss") + ".xlsx", defectRateData);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            DefectRateReport defectRateReport = new DefectRateReport();
     DateTime date_from = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1, 0, 0, 0);
            DateTime date_to = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 30, 0, 0, 0);
            DefectRateData defectRateData = new DefectRateData();
            defectRateData = defectRateReport.GetDefectRateReport(date_from, date_to, "B01", "0010");
            Class.ToolSupport exportExcel = new Class.ToolSupport();
           exportExcel.ExportToTemplateMQCDefect(pathMonth, @"C:\ERP_Temp\Temp2" + "-"+DateTime.Now.ToString("yyyyMMdd hhmmss") + ".xlsx", defectRateData);
      //      exportExcel.ExportToTemplateMQCDefect(@"D:\AN\Report\Template\Testy.xlsx", @"C:\ERP_Temp\Temp2" + "-" + DateTime.Now.ToString("yyyyMMdd hhmmss") + ".xlsx", defectRateData);

        }

        private void ProductionMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            bgWorker.ProgressChanged -= BgWorker_ProgressChanged;
            bgWorker.RunWorkerCompleted -= new RunWorkerCompletedEventHandler(bg_RunWorkerCompleted);
            tmrCallBgWorker.Tick -= new EventHandler(tmrCallBgWorker_Tick);
            if(line != null)
            line.Dispose();
        }
    }
}
