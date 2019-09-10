﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApplication1.Class;

namespace WindowsFormsApplication1.ERPShowOrder
{
    public partial class ERP_KPI_Report : CommonForm
    {
        DataTable dtshow;
        DataTable dt;
        string PathFoler = @"C:\ERP_Temp\";
        Dictionary<string, DeliveryStatus> ListclientsDeliveryStatus = new Dictionary<string, DeliveryStatus>();
        public ERP_KPI_Report()
        {
            InitializeComponent();
            AcceptButton = btn_search;
            bool exists = System.IO.Directory.Exists(PathFoler);
            if (!exists)
                System.IO.Directory.CreateDirectory(PathFoler);
           
            this.WindowState = FormWindowState.Maximized;
        }

        private void Btn_search_Click(object sender, EventArgs e)
        {
            datashow();
              GenarateReport(ref dgv_show, dtshow);
          //  dgv_show.DataSource = null;
            //  dgv_show = new DataGridView();
            //   dgv_show.DataSource = dtshow;
            dgv_show.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgv_show.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgv_show.AutoGenerateColumns = true;
            dgv_show.DefaultCellStyle.Font = new Font("Verdana", 8, FontStyle.Regular);
            dgv_show.ColumnHeadersDefaultCellStyle.Font = new Font("Verdana", 8, FontStyle.Bold);
            dgv_show.AllowUserToAddRows = false;
            
          
           
        }
        void datashow()
        {
            dtshow = new DataTable();
            DateTime dateto = dtp_todate.Value;
            DateTime datefrom = dtp_from.Value;
           
            StringBuilder sql = new StringBuilder();
            sql.Append(@"select 
CONVERT(date,t_octcs.TC01) as Create_Date, 
t_octcs.TC02 + '-'+ t_octcs.TC03 as code ,
t_octcs.TC05 as Clients,  
avg(CAST(t_octcs.TC32 as float)) as Shipping_Percent ,
max(CONVERT(date,t_octcs.TC17)) as Delivery_Date , 
max(CONVERT(date,t_octcs.TC12)) as Client_Request_Date
from   t_OCTC t_octcs 
left join  t_OCTB t_octbs  on t_octcs.TC02 = t_octbs.TB02 and t_octcs.TC03 = t_octbs.TB03 
where 1=1

");

 
                sql.Append(" and CONVERT(date,t_octcs.TC01)  >= '" + datefrom + "' ");
                sql.Append(" and CONVERT(date,t_octcs.TC01) <= '" + dateto + "' ");

            sql.Append(@" group by t_octcs.TC01, t_octcs.TC02 + '-'+ t_octcs.TC03, t_octcs.TC05");
            sql.Append(" order by t_octcs.TC02 + '-'+ t_octcs.TC03 ");
            sqlCON con = new sqlCON();
            con.sqlDataAdapterFillDatatable(sql.ToString(), ref dtshow);

        }
        private void GenarateReport(ref DataGridView dtgv, DataTable dataTable)
        {
            ListclientsDeliveryStatus = new Dictionary<string, DeliveryStatus>();
           
            for (int i = 0; i < dataTable.Rows.Count - 1; i++)
            {
                DataRow row = dataTable.Rows[i];

                double ShippingPercent = Convert.ToDouble(row["Shipping_Percent"].ToString());
                string clients = row["Clients"].ToString();
                string deadline = row["Client_Request_Date"].ToString();
                DateTime Deadline = DateTime.MinValue;
                DateTime DeliveryDate = DateTime.MinValue;
                if (row["Client_Request_Date"] != null && row["Client_Request_Date"].ToString().Length > 8)
                {
                    Deadline = Convert.ToDateTime(row["Client_Request_Date"].ToString());
                }
                if (row["Delivery_Date"] != null && row["Delivery_Date"].ToString().Length > 8)
                {
                    DeliveryDate = Convert.ToDateTime(row["Delivery_Date"].ToString());
                }
                if (ListclientsDeliveryStatus != null && Deadline > DateTime.MinValue)
                {
                    if (ListclientsDeliveryStatus.ContainsKey(clients) == false)
                    {
                        DeliveryStatus status = new DeliveryStatus();
                        if (ShippingPercent < 100 && DeliveryDate < Deadline)
                        {
                            status.OrderOT = 1;
                        }
                        if (ShippingPercent >= 100 && DeliveryDate == Deadline)
                        {
                            status.OrderOT = 1;
                        }
                        if (ShippingPercent >= 100 && DeliveryDate < Deadline)
                        {
                            status.OrderEarly = 1;
                        }
                        if (DeliveryDate > Deadline)
                        {
                            status.OrderLate = 1;
                        }

                        ListclientsDeliveryStatus.Add(clients, status);
                    }
                    else
                    {
                        if (ShippingPercent < 100 && DeliveryDate < Deadline)
                        {
                            ListclientsDeliveryStatus[clients].OrderOT += 1;
                        }
                        if (ShippingPercent >= 100 && DeliveryDate == Deadline)
                        {
                            ListclientsDeliveryStatus[clients].OrderOT += 1;
                        }
                        if (ShippingPercent >= 100 && DeliveryDate < Deadline)
                        {
                            ListclientsDeliveryStatus[clients].OrderEarly += 1;
                        }
                        if (DeliveryDate > Deadline)
                        {
                            ListclientsDeliveryStatus[clients].OrderLate += 1;
                        }

                    }
                }

            }
            foreach (KeyValuePair<string, DeliveryStatus> items in ListclientsDeliveryStatus)
            {

                items.Value.clients = items.Key;
                items.Value.Order = items.Value.OrderEarly + items.Value.OrderOT + items.Value.OrderLate;
                items.Value.Reliability = (100.0 - Math.Round((double)items.Value.OrderLate / items.Value.Order, 2)*100);
            }
         

            dtgv.DataSource = ListclientsDeliveryStatus.Values.ToList<DeliveryStatus>();
            dtgv.Columns[0].HeaderText = "Clients";
            dtgv.Columns[1].HeaderText = "Reliability [%]";
            dtgv.Columns[2].HeaderText = "Order";
            dtgv.Columns[3].HeaderText = "Order OT";
            dtgv.Columns[4].HeaderText = "Order Early";
            dtgv.Columns[5].HeaderText = "Order Late";
            dtgv.Columns[6].HeaderText = "OTS [Days]";
            dtgv.Columns[7].HeaderText = "Req OTS [Days]";
            dtgv.Columns[8].HeaderText = "SO-MO";
        }

        private void Btn_toExcel_Click(object sender, EventArgs e)
        {
            string pathsave = "";
            try
            {
                System.Windows.Forms.SaveFileDialog saveFileDialog = new SaveFileDialog();

                saveFileDialog.Title = "Browse Excel Files";
                saveFileDialog.DefaultExt = "Excel";
                saveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx";

                saveFileDialog.CheckPathExists = true;

                if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    pathsave = saveFileDialog.FileName;
                }
                saveFileDialog.RestoreDirectory = true;
                ToolSupport tool = new ToolSupport();
                tool.dtgvExport2Excel(dgv_show, pathsave + DateTime.Now.ToString("yyyyMMdd HHmmss") + ".xls");
            }
            catch (Exception ex)
            {

                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }
    }
        }
    
    public class DeliveryStatus
    {
        public string clients { get; set; }
        public double Reliability { get; set; }
        public int Order { get; set; }

        public int OrderOT { get; set; }
        public int OrderEarly { get; set; }
        public int OrderLate { get; set; }        
        public double OTS { get; set; }
        public double ReqOTS { get; set; }
        public double SOMO{ get; set; }


    }
