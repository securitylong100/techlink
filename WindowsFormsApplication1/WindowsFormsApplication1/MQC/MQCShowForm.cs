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
using WindowsFormsApplication1.Log;

namespace WindowsFormsApplication1.MQC
{
    public partial class MQCShowForm : MetroForm
    {
        NGPanel nGPanel = new NGPanel(null);
        RWPanel nGRework = new RWPanel(null);
        BackgroundWorker bgWorker;
        // this timer calls bgWorker again and again after regular intervals
        System.Windows.Forms.Timer tmrCallBgWorker;
        // this is the timer to make sure that worker gets called
        System.Threading.Timer tmrEnsureWorkerGetsCalled;
        // object used for safe access
        object lockObject = new object();
        DateTime dateTimeFrom;
        DateTime dateTimeTo;
        MQCItem1 mQCItem = new MQCItem1();
        string dept = "";
        string process = "";
        string product = "";
        string po = "";
        bool IsStartup = false;
        string deptFull = "";
        int widthWindow = 0;
        int heightWindow = 0;
        int countRefresh = 10;
        public MQCShowForm(MQCItem1 mQCItem1, string depttext )
        {
            InitializeComponent();
            mQCItem = mQCItem1;
            deptFull = depttext;
            IsStartup = false;
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
            // tmrCallBgWorker.Interval = 10000;

        }
        #region Backround worker task
        void bg_DoWork(object sender, DoWorkEventArgs e)
        {

            var worker = sender as BackgroundWorker;
            LoadDataERPMQCToShow();
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
          

            LoadUIFromMQCITEM();
            lb_mesage_defect.Left += 10;
            lb_messageMaterial.Left += 10;
        }
        public void LoadUIFromMQCITEM()
        {
            try
            {
             lb_outputTotal.Text = mQCItem.TotalOutput.ToString("N0"); /*1234.567 ("N", en-US) -> 1,234.57*/
            lb_TotalInputSFT.Text = mQCItem.InputSFT.ToString("N0");
            lb_NotyetSFT.Text = mQCItem.InputMaterialNotYet.ToString("N0");
            lb_TotalNG.Text = mQCItem.TotalNG.ToString("N0");
            lb_TotalRW.Text = mQCItem.TotalRework.ToString("N0");
            lb_percentNG.Text = mQCItem.percentNG.ToString("P1");
            lb_percentRW.Text = mQCItem.percentRework.ToString("P1");
            lb_dept.Text = deptFull;
            lb_po.Text = mQCItem.PO;
            lb_process.Text = mQCItem.process;
            lb_product.Text = mQCItem.product;
             if(mQCItem.Status == ProductionStatus.ShortageMaterial.ToString())
                {
                    lb_messageMaterial.Text = mQCItem.Measage;
                }
             if(mQCItem.Status == ProductionStatus.HighDefect.ToString())
                {
                    lb_mesage_defect.Text = mQCItem.Measage;
                }
             if(mQCItem.Status == ProductionStatus.Normal.ToString())
                {
                    lb_messageMaterial.Text = "";
                    lb_mesage_defect.Text = "";
                }
             nGPanel = new NGPanel(mQCItem.listNGItems);
            pa_NGPanel.Controls.Clear();
            if (!pa_NGPanel.Controls.Contains(nGPanel))
            {
                nGPanel.Name = mQCItem.product;
              
                nGPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                    | System.Windows.Forms.AnchorStyles.Left)
                    | System.Windows.Forms.AnchorStyles.Right)));
                pa_NGPanel.Controls.Add(nGPanel);

            }


            pa_rework.Controls.Clear();

            nGRework = new RWPanel(mQCItem.listRWItems);
            if (!pa_rework.Controls.Contains(nGRework))
            {

                nGRework.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                     | System.Windows.Forms.AnchorStyles.Left)
                     | System.Windows.Forms.AnchorStyles.Right)));
                pa_rework.Controls.Add(nGRework);
            }

            }
            catch (Exception ex)
            {
                Logfile.Output(StatusLog.Error, "LoadUIFromMQCITEM()", ex.Message);


            }
            
        }
        #endregion backround worker task
        private void MQCShowForm_Load(object sender, EventArgs e)
        {
            //  LoadNGPanel(); //Load NG Panel

            SettingTimerForBrwoker(); //Setting Timer for run backround worker
            SettingTimeFromDateTodate();
            LoadcbInUI();
            LoadUIFromMQCITEM();
            IsStartup = true;
            hScrollBar1.Maximum = nGPanel.Width - layoutPanelNG.Width;
            hScrollbarRework.Maximum = nGRework.Width - layoutPanelRW.Width;
        }

     

    
        private void SettingTimerForBrwoker()
        {
            int timerInterval = 5000;

            tmrCallBgWorker.Interval = timerInterval;
            tmrCallBgWorker.Start();
        }
 
        #region Private function
        //Load data from database 172.0.16.12, tabble m_ERPMQC
        private void SettingTimeFromDateTodate ()
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
                    dateTimeTo = new DateTime(DateTime.Now.Year + 1,  1, 1, 6, 0, 0);
                }
            }
            dateTimeFrom = DateTime.MinValue;
        }
        private void LoadcbInUI()
        {
            lb_dept.Text = deptFull;
            lb_process.Text = mQCItem.process;
            lb_product.Text = mQCItem.product;
            dept = mQCItem.department;
            process = lb_process.Text;
             product = lb_product.Text;
            po = lb_po.Text ;
        }
        private void LoadDataERPMQCToShow()
        {
            //Load data from m_ERPMQC
            LoadDataMQC dataMQC = new LoadDataMQC();
           
            mQCItem = dataMQC.GetQCCItemOK(dateTimeFrom, dateTimeTo, product, po, dept, process);
           
        }

        #endregion


        private void MQCShowForm_Resize(object sender, EventArgs e)
        {

            if (IsStartup)
            {

                if (this.WindowState == FormWindowState.Maximized)
                {
                    Lb_OutputLable.Font = new Font("Times New Roman", Lb_OutputLable.Font.SizeInPoints + 20, FontStyle.Bold);
                    Lb_SFTLable.Font = new Font("Times New Roman", Lb_SFTLable.Font.SizeInPoints + 20, FontStyle.Bold);
                    Lb_SFTNotLable.Font = new Font("Times New Roman", Lb_SFTNotLable.Font.SizeInPoints + 20, FontStyle.Bold);
                    Lb_TotalNGLabel.Font = new Font("Times New Roman", Lb_TotalNGLabel.Font.SizeInPoints + 20, FontStyle.Bold);
                    Lb_reworkLB.Font = new Font("Times New Roman", Lb_reworkLB.Font.SizeInPoints + 20, FontStyle.Bold);
                  

                    lb_outputTotal.Font = new Font("Times New Roman", lb_outputTotal.Font.SizeInPoints + 20, FontStyle.Bold);
                    lb_TotalInputSFT.Font = new Font("Times New Roman", lb_TotalInputSFT.Font.SizeInPoints + 20, FontStyle.Bold);
                    lb_NotyetSFT.Font = new Font("Times New Roman", lb_NotyetSFT.Font.SizeInPoints + 20, FontStyle.Bold);
                    lb_percentNG.Font = new Font("Times New Roman", lb_percentNG.Font.SizeInPoints + 20, FontStyle.Bold);
                    lb_TotalNG.Font = new Font("Times New Roman", lb_TotalNG.Font.SizeInPoints + 20, FontStyle.Bold);
                    lb_TotalRW.Font = new Font("Times New Roman", lb_TotalRW.Font.SizeInPoints + 20, FontStyle.Bold);
                    lb_percentRW.Font = new Font("Times New Roman", lb_percentRW.Font.SizeInPoints + 20, FontStyle.Bold);

                    //  lb_percentNG.Font = new Font("Times New Roman", lb_percentNG.Font.SizeInPoints + 10);
                }
                else if (this.WindowState == FormWindowState.Normal && this.Size.Width == widthWindow)
                {
                    Lb_OutputLable.Font = new Font("Times New Roman", Lb_OutputLable.Font.SizeInPoints - 20, FontStyle.Bold);
                    Lb_SFTLable.Font = new Font("Times New Roman", Lb_SFTLable.Font.SizeInPoints - 20, FontStyle.Bold);
                    Lb_SFTNotLable.Font = new Font("Times New Roman", Lb_SFTNotLable.Font.SizeInPoints - 20, FontStyle.Bold);
                    Lb_TotalNGLabel.Font = new Font("Times New Roman", Lb_TotalNGLabel.Font.SizeInPoints - 20, FontStyle.Bold);
                    Lb_reworkLB.Font = new Font("Times New Roman", Lb_reworkLB.Font.SizeInPoints - 20, FontStyle.Bold);

                    lb_outputTotal.Font = new Font("Times New Roman", lb_outputTotal.Font.SizeInPoints - 20, FontStyle.Bold);
                    lb_TotalInputSFT.Font = new Font("Times New Roman", lb_TotalInputSFT.Font.SizeInPoints - 20, FontStyle.Bold);
                    lb_NotyetSFT.Font = new Font("Times New Roman", lb_NotyetSFT.Font.SizeInPoints - 20, FontStyle.Bold);
                    lb_percentNG.Font = new Font("Times New Roman", lb_percentNG.Font.SizeInPoints - 20, FontStyle.Bold);
                    lb_TotalNG.Font = new Font("Times New Roman", lb_TotalNG.Font.SizeInPoints - 20, FontStyle.Bold);
                    lb_TotalRW.Font = new Font("Times New Roman", lb_TotalRW.Font.SizeInPoints - 20, FontStyle.Bold);
                    lb_percentRW.Font = new Font("Times New Roman", lb_percentRW.Font.SizeInPoints - 20, FontStyle.Bold);
                }
                else
                {
                    widthWindow = this.Size.Width;
                    heightWindow = this.Size.Height;
                }
            }
            else
            {
                widthWindow = this.Size.Width;
                heightWindow = this.Size.Height;
            }
           
        }

        private void HScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
         
         //   nGPanel.Left = nGPanel.Left - hScrollBar1.Value;
        }
        int oldHScrollBar = 0;
        int oldscrollbarRW = 0;
        private void HScrollBar1_ValueChanged(object sender, EventArgs e)
        {
            
            nGPanel.Left = nGPanel.Left - ( hScrollBar1.Value - oldHScrollBar);
            oldHScrollBar = hScrollBar1.Value;
        }

    
        private void HScrollbarRework_Scroll(object sender, ScrollEventArgs e)
        {
            nGRework.Left = nGRework.Left - (hScrollbarRework.Value - oldscrollbarRW);
            oldscrollbarRW = hScrollbarRework.Value;
        }
    }
}
