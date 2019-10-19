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

namespace WindowsFormsApplication1.MQC
{
   
    public partial class LineUI : UserControl
    {
        public MQCItem1 mQCItem1 = new MQCItem1();
        string Dept;
        public ImageList imageList = new ImageList();
        public string PathResource = Environment.CurrentDirectory + @"\Resources\";
        public  LineUI(MQCItem1 mQC,string dept)
        {
            InitializeComponent();
            mQCItem1 = mQC;
            Dept = dept;
           
         
            UpdateUI(mQC);
        }
     
        public void UpdateUI(MQCItem1 mQC)
        {
            try
            {

         
            if (mQC != null && mQC.department != null)
            {
                lb_model.Text = mQC.product;
           //     lb_Dept.Text = mQC.department;
                lb_output.Text = mQC.TotalOutput.ToString("N0");

                lb_targetvalue.Text = mQC.TotalOutput.ToString("N0");
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

                throw;
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
            listMQC = loadDataMQC.listMQCDataItems(DateTime.Now.Date, DateTime.Now.Date.AddDays(1), mQCItem1.product, "", mQCItem1.department, mQCItem1.process);
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
    }
}
