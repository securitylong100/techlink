using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApplication1.ChartDrawing;
using System.IO;
using WindowsFormsApplication1.MQC.Report;

namespace WindowsFormsApplication1.MQC
{
   
    public partial class LineUI : UserControl
    {
        public MQCItem1 mQCItem1 = new MQCItem1();
        string Dept;
    
        public string PathResource = Environment.CurrentDirectory + @"\Resources\";
        float SizeText = 0;
        public string MQCForm = Environment.CurrentDirectory + @"\Resources\MQC-PQC_Template.xlsx";
        public  LineUI(MQCItem1 mQC,string dept,float sizeText)
        {
            InitializeComponent();
            mQCItem1 = mQC;
            Dept = dept;
            SizeText = sizeText;


            UpdateUI(mQC);
            Disposed += OnDispose;
        }
        private void OnDispose(object sender, EventArgs e)
        {
            this.btn_chart.Click -= new System.EventHandler(this.Btn_chart_Click);
            this.lb_Lot.Click -= new System.EventHandler(this.Lb_Lot_Click);
            mQCItem1 = null;
            // do stuff on dispose
        }
        public void UpdateUI(MQCItem1 mQC)
        {
            try
            {

         
            if (mQC != null && mQC.department != null)
            {
                lb_model.Text = mQC.product;
                    lb_model.Font = new  Font("Times New Roman", SizeText,FontStyle.Bold);
                    lb_Dept.Font = new Font("Times New Roman", SizeText, FontStyle.Bold);
                    lb_Lot.Text = mQC.PO;
                    lb_Lot.Font = new Font("Times New Roman", SizeText, FontStyle.Bold);
                    lb_output.Font = new Font("Times New Roman", SizeText+5, FontStyle.Bold);
                    lb_targetvalue.Font = new Font("Times New Roman", SizeText+5, FontStyle.Bold);
                    lb_defectValue.Font = new Font("Times New Roman", SizeText+5, FontStyle.Bold);
                    //     lb_Dept.Text = mQC.department;
                    lb_output.Text = mQC.TotalOutput.ToString("N0");

                lb_targetvalue.Text = mQC.TargetMQC.TargetOutput.ToString("N0");
                lb_defectValue.Text = mQC.TotalNG.ToString("N0");
                   
                if(mQC.Status == ProductionStatus.ShortageMaterial.ToString())
                    {
                        pic_status.Image = null;
                       
                    pic_status.Image = global::WindowsFormsApplication1.Properties.Resources.SHORTAGE_MATERIAL;
                    }
                else if (mQC.Status == ProductionStatus.Normal.ToString())
                    {
                        pic_status.Image = null;

                        pic_status.Image = global::WindowsFormsApplication1.Properties.Resources.Normal_s;
                    }
                else if (mQC.Status == ProductionStatus.HighDefect.ToString())
                    {
                        pic_status.Image = null;

                        pic_status.Image = global::WindowsFormsApplication1.Properties.Resources.High_Defect;
                    }
            }
            }
            catch (Exception ex)
            {

                Log.Logfile.Output(Log.StatusLog.Error, "UpdateUI(MQCItem1 mQC)", ex.Message);
            }
        }

        private void Btn_detail_Click(object sender, EventArgs e)
        {
           
            MQCShowForm mQCShowForm = new MQCShowForm(mQCItem1, Dept);
            mQCShowForm.ShowDialog();
        }

        private void Btn_chart_Click(object sender, EventArgs e)
        {
            List<MQCDataItems> listMQC = new List<MQCDataItems>();
            LoadDataMQC loadDataMQC = new LoadDataMQC();
            listMQC = loadDataMQC.listMQCDataItems(DateTime.Now.Date, DateTime.Now.Date.AddDays(1), mQCItem1.product, mQCItem1.PO, mQCItem1.process);
            List<chartdatabyDate> chartdata = new List<chartdatabyDate>();
            List<chartdatabyDate> chartdataDefect = new List<chartdatabyDate>();
            foreach (var item in listMQC)
            {if (item.item == "OUTPUT")
                {
                    chartdata.Add(new chartdatabyDate { date = item.inspectdate, time = item.inspecttime, value = item.data });
                }
             else if (item.remark == "NG")
                {
                    chartdataDefect.Add(new chartdatabyDate { date = item.inspectdate, time = item.inspecttime, value = item.data });
                }
            }
            if (chartdata != null)
            {
                MQCChart mQCChart = new MQCChart(mQCItem1, chartdata, chartdataDefect);
                mQCChart.ShowDialog();
            }
        }

   

        private void Lb_Lot_Click(object sender, EventArgs e)
        {
            DefectRateReport defectRateReport = new DefectRateReport();
            DateTime date_from = DateTime.Now;
            DateTime date_to = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 30, 0, 0, 0);
            DefectRateData defectRateData = new DefectRateData();
            defectRateData = defectRateReport.GetDefectRateReportByLot(date_from, date_to, "B01", "0010", mQCItem1.PO);

            Class.ToolSupport exportExcel = new Class.ToolSupport();
            exportExcel.ExportToTemplateMQCDefectTop16(MQCForm, @"C:\ERP_Temp\MQC_"+ mQCItem1.PO + "-" + DateTime.Now.ToString("yyyyMMdd hhmmss") + ".xlsx", defectRateData);
        }
    }
}
