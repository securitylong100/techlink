using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using WindowsFormsApplication1.CrisisReport.Classvalue;

namespace WindowsFormsApplication1
{
    public partial class ShippingReport : CommonForm
    {
        DataTable dt;
        DataTable dtShipping;
        DataTable dtshow;
        List<StatusSumary> ShippingSummary = new List<StatusSumary>();
        public ShippingReport()
        {
            InitializeComponent();
        }

        private void Cb_yearly_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void LoadTreeviewDeptment()
        {

            TreeNode trnode = new TreeNode("ALL DEPARTMENTS");
            trnode.Name = "Node_Depts";


            trv_department.Nodes.Clear();
            dt = new DataTable();
            sqlERPCON conERP = new sqlERPCON();
            conERP.sqlDataAdapterFillDatatable("select distinct b.TC005,a.ME002 from CMSME a inner join COPTC b on a.ME001 = b.TC005 order by b.TC005 ", ref dt);
            TreeNode child = new TreeNode();

            foreach (DataRow row in dt.Rows)
            {
                child = new TreeNode(row[0].ToString() + ": " + row[1].ToString());
                trnode.Nodes.Add(child);

            }

            trv_department.Nodes.Add(trnode);

            trv_department.AfterCheck += Trv_department_AfterCheck;
            trnode.Checked = true;
        }

        private void Trv_department_AfterCheck(object sender, TreeViewEventArgs e)
        {

            if (e.Node.Name == "Node_Depts")
            {
                foreach (TreeNode tn in e.Node.Nodes)
                {
                    tn.Checked = e.Node.Checked;

                }
            }

        }
        private void GetDataShsipping()
        {
            DateTime dateto = dtp_to.Value;
            DateTime datefrom = dtp_from.Value;
            dtShipping = new DataTable();
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
            if (ChooseDeptoSQLcommand(trv_department) != "")
            {
                sql.Append(ChooseDeptoSQLcommand(trv_department));

            }
            else
            {
                MessageBox.Show("Please choose departments which you want to search data ! ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

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
            con.sqlDataAdapterFillDatatable(sql.ToString(), ref dtShipping);
        }
        private string ChooseDeptoSQLcommand(TreeView treeView)
        {

            string sqlquerry = "";
            var treeviewNodes = treeView.Nodes["Node_Depts"];
            if (treeviewNodes.GetNodeCount(false) > 0)
            {
                int count = 0;
                int countNodeCheck = 0;
                foreach (TreeNode node in treeviewNodes.Nodes)
                {
                    if (countNodeCheck == 0 && node.Checked == true)
                    {
                        sqlquerry += "and ( ";
                    }
                    else if (countNodeCheck > 0 && node.Checked == true)
                    {
                        sqlquerry += " or ";
                    }
                    if (node.Checked == true)
                    {

                        sqlquerry += " coptcs.TC005 =  '" + node.Text.Split(':')[0] + "'";

                        countNodeCheck++;
                    }
                    if ((count == treeviewNodes.Nodes.Count - 1) && countNodeCheck > 0)
                    {
                        sqlquerry += " ) ";
                    }

                    count++;
                }

            }
            return sqlquerry;
        }
        private void ShippingReport_Load(object sender, EventArgs e)
        {
            LoadTreeviewDeptment();

        }
        private List<ShippingItems> ListItemShowShipping(DataTable dta)
        {
            List<ShippingItems> ListshippingItems = new List<ShippingItems>();
            List<ShippingItems> ListshippingResult = new List<ShippingItems>();
            Dictionary<string, double> keyValuePairs = new Dictionary<string, double>();


            for (int i = 0; i < dta.Rows.Count; i++)
            {
                ShippingItems Items = new ShippingItems();
                object item0 = dta.Rows[i][0];
                object item11 = dta.Rows[i][11];
                object item16 = dta.Rows[i][16];
                object item19 = dta.Rows[i][19];


                Items.CreateTime = DateTime.Parse(dta.Rows[i][0].ToString());
                Items.OrderCode = (string)dta.Rows[i][1] + "-" + (string)dta.Rows[i][2];
                Items.Clients = (string)dta.Rows[i][4];
                Items.Clients_OrderCode = (string)dta.Rows[i][5];
                Items.ClientsRequestDate = DateTime.Parse(dta.Rows[i][11].ToString().Insert(4, "-").Insert(7, "-"));
                Items.DeliveryDate = (item16.ToString().Count() != 8) ? DateTime.MinValue : DateTime.Parse(dta.Rows[i][16].ToString().Insert(4, "-").Insert(7, "-"));
                Items.ShippingPercents = dta.Rows[i][19].ToString() == "" ? 0 : Math.Round(double.Parse(dta.Rows[i][19].ToString()), 2);
                ListshippingItems.Add(Items);



            }
            var groupedListItems = ListshippingItems
     .GroupBy(u => u.OrderCode)
     .Select(grp => grp.ToList())
     .ToList();

            foreach (List<ShippingItems> shippingItems in groupedListItems)
            {

                var average = shippingItems.Average(a => a.ShippingPercents);
                var MaxDelivery = shippingItems.Max(a => a.DeliveryDate);
                shippingItems[0].ShippingPercents = average;
                shippingItems[0].DeliveryDate = MaxDelivery;

                if (shippingItems[0].DeliveryDate > shippingItems[0].ClientsRequestDate && shippingItems[0].ShippingPercents >= 100)
                    shippingItems[0].Status = "Later";
                else if (shippingItems[0].DeliveryDate < shippingItems[0].ClientsRequestDate && shippingItems[0].ShippingPercents >= 100)
                    shippingItems[0].Status = "Early";
                else if (shippingItems[0].DeliveryDate == shippingItems[0].ClientsRequestDate && shippingItems[0].ShippingPercents >= 100)
                    shippingItems[0].Status = "On-Time";
                else if (DateTime.Now < shippingItems[0].ClientsRequestDate && shippingItems[0].ShippingPercents < 100)
                    shippingItems[0].Status = "not complete";
                else if (DateTime.Now > shippingItems[0].ClientsRequestDate && shippingItems[0].ShippingPercents < 100)
                    shippingItems[0].Status = "not complete - Later";
                else shippingItems[0].Status = "Undefined";

                ListshippingResult.Add(shippingItems[0]);


            }

            var ListItems = ListshippingResult
.GroupBy(u => u.Status)
.Select(grp => grp.ToList())
.ToList();
            foreach (var item in ListItems)
            {
                StatusSumary st = new StatusSumary();
                st.Status = item[0].Status;
                st.Quantity = item.Count;
                ShippingSummary.Add(st);
            }
            return ListshippingResult;
        }
        private void Btn_search_Click(object sender, EventArgs e)
        {
            List<ShippingItems> list = new List<ShippingItems>();
            GetDataShsipping();
            if (dtShipping != null && dtShipping.Rows.Count > 0)
            {
                list = ListItemShowShipping(dtShipping);
            }
            if (list != null && list.Count > 0)
            {
                dgv_show.DataSource = list;
                dgv_show.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dgv_show.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                dgv_show.AutoGenerateColumns = true;
                dgv_show.DefaultCellStyle.Font = new Font("Verdana", 8, FontStyle.Regular);
                dgv_show.ColumnHeadersDefaultCellStyle.Font = new Font("Verdana", 8, FontStyle.Bold);
                dgv_show.AllowUserToAddRows = false;
                dgv_show.Columns[0].HeaderText = "Date";
                dgv_show.Columns[1].HeaderText = "Order Code";
                dgv_show.Columns[2].HeaderText = "Clients";
                dgv_show.Columns[3].HeaderText = "Clients Order";
                dgv_show.Columns[4].HeaderText = "Clients Request Date";
                dgv_show.Columns[5].HeaderText = "Delivery Date";
                dgv_show.Columns[6].HeaderText = "Shipping Percernt";
                dgv_show.Columns[7].HeaderText = "Status";
                MakeColorForDatagridview(dgv_show);
                DrawingChartForShipping(ShippingSummary);
            }
            else
            {
                dgv_show.DataSource = null;
            }
        }
        private void MakeColorForDatagridview(DataGridView dtgv)
        {
            if (dtgv.Rows.Count > 0)
            {
                int count = 0;
                foreach (DataGridViewRow row in dtgv.Rows)
                {
                    if (dtgv["Status", count].Value.ToString() == "not complete - Later") //backlog
                    {
                        dtgv["Status", count].Style.BackColor = Color.DarkRed;//to color the row

                    }
                    else if (dtgv["Status", count].Value.ToString() == "Later") //backlog
                    {
                        dtgv["Status", count].Style.BackColor = Color.OrangeRed;//to color the row

                    }
                    else if (dtgv["Status", count].Value.ToString() == "not complete") //backlog
                    {
                        dtgv["Status", count].Style.BackColor = Color.LightYellow;//to color the row

                    }
                    else if (dtgv["Status", count].Value.ToString() == "On-Time") //backlog
                    {
                        dtgv["Status", count].Style.BackColor = Color.Green;//to color the row

                    }
                    else if (dtgv["Status", count].Value.ToString() == "Early") //backlog
                    {
                        dtgv["Status", count].Style.BackColor = Color.LightGreen;//to color the row

                    }
                    else if (dtgv["Status", count].Value.ToString() == "Undefined") //backlog
                    {
                        dtgv["Status", count].Style.BackColor = Color.White;//to color the row

                    }
                    count++;
                }
            }

        }
        private void DrawingChartForShipping(List<StatusSumary> list)
        {
            this.chart_shipping.Series.Clear();

            this.chart_shipping.Titles.Add("Shipping Status");
            
            Series series = this.chart_shipping.Series.Add("Shipping Status");
            series.ChartType = SeriesChartType.Column;
            series.AxisLabel = "Shipping Status";
            series.IsValueShownAsLabel = true;
            series.IsVisibleInLegend = false;
           
            foreach (var item in list)
            {
            
                series.Points.AddXY(item.Status, item.Quantity);
               
                
            }
            foreach (var item in series.Points)
            {
                string i = item.XValue.ToString();

            }
            series.Points[0].Color = System.Drawing.Color.Red;
            series.Points[1].Color = System.Drawing.Color.Green;
            series.Points[2].Color = System.Drawing.Color.Blue;
            //set the member of the chart data source used to data bind to the X-values of the series   

        }

        private void Chart_shipping_Click(object sender, EventArgs e)
        {

        }
    }
}
