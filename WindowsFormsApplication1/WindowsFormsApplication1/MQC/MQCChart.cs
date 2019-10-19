using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;
using WindowsFormsApplication1.ChartDrawing;

namespace WindowsFormsApplication1.MQC
{
    public partial class MQCChart : MetroForm
    {
        List<chartdatabyDate> chartdatabyDates = new List<chartdatabyDate>();
        List<chartdatabyDate> chartdatadefect= new List<chartdatabyDate>();
        List<chartdatabyDate> chartdatabyDates_old = new List<chartdatabyDate>();
        List<chartdatabyDate> chartdatadefect_old = new List<chartdatabyDate>();
        MQCItem1 MQC = new MQCItem1();
        BackgroundWorker bgWorker;
        // this timer calls bgWorker again and again after regular intervals
        System.Windows.Forms.Timer tmrCallBgWorker;
        // this is the timer to make sure that worker gets called
        System.Threading.Timer tmrEnsureWorkerGetsCalled;
        // object used for safe access
        object lockObject = new object();
        int countRefresh = 10;
        public MQCChart(MQCItem1 mQC,List<chartdatabyDate> chartdata, List<chartdatabyDate> chartdataDefect)
        {
            InitializeComponent();
            this.Text = "PRODUCTION MONITORING CHART";
            MQC = mQC;
            chartdatabyDates = chartdata;
            chartdatabyDates_old = chartdatabyDates;
            chartdatadefect = chartdataDefect;
            chartdatadefect_old = chartdatadefect;
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
            List<MQCDataItems> listMQC = new List<MQCDataItems>();
            LoadDataMQC loadDataMQC = new LoadDataMQC();
            listMQC = loadDataMQC.listMQCDataItems(DateTime.Now.Date, DateTime.Now.Date.AddDays(1), MQC.product, "", MQC.department, MQC.process);
            chartdatabyDates = new List<chartdatabyDate>();
            chartdatadefect = new List<chartdatabyDate>();
            foreach (var item in listMQC)
            {
                if (item.item == "OUTPUT")
                {
                    chartdatabyDates.Add(new chartdatabyDate { date = item.inspectdate, time = item.inspecttime, value = item.data });
                }
                else if (item.remark == "NG")
                {
                    chartdatadefect.Add(new chartdatabyDate { date = item.inspectdate, time = item.inspecttime, value = item.data });
                }
            }
           

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

                    {
                        bgWorker.RunWorkerAsync();
                        countRefresh--;
                    }
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
         
            if (chartdatabyDates_old.Count != chartdatabyDates.Count || chartdatadefect_old.Count != chartdatadefect.Count)
            {
                LiveChartDrawing liveChartDrawing = new LiveChartDrawing();
                liveChartDrawing.DrawingLiveChart(chartdatabyDates, chartdatadefect, ref columnChart);
                chartdatabyDates_old = chartdatabyDates;
                chartdatadefect_old = chartdatadefect;
            }
            if(countRefresh == 0)
            {
                LiveChartDrawing liveChartDrawing = new LiveChartDrawing();
                liveChartDrawing.DrawingLiveChart(chartdatabyDates, chartdatadefect, ref columnChart);
                chartdatabyDates_old = chartdatabyDates;
                chartdatadefect_old = chartdatadefect;
                countRefresh = 10;
            }
           

        }
    
        #endregion backround worker task
        private void MQCChart_Load(object sender, EventArgs e)
        {
            
            LiveChartDrawing liveChartDrawing = new LiveChartDrawing();
            liveChartDrawing.DrawingLiveChart(chartdatabyDates, chartdatadefect, ref columnChart);
            SettingTimerForBrwoker();
        }
        private void SettingTimerForBrwoker()
        {
            int timerInterval = 2000;

            tmrCallBgWorker.Interval = timerInterval;
            tmrCallBgWorker.Start();
        }
    }
}
