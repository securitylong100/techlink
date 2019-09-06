using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1.ERPShowOrder
{
    public partial class ERPShowMain : CommonForm
    {
        public ERPShowMain()
        {
            InitializeComponent();
        }
        DataTable dtshow;
        DataTable dt;
        private void ERPShowMain_Load(object sender, EventArgs e)
        {
            string sql = "select distinct TC001 from COPTC where TC001 != '' order by TC001";
            sqlERPCON conERP = new sqlERPCON();
            conERP.getComboBoxData(sql, ref cmd_COPTC_TC001);
        }

        private void cmd_MOCTA_TA001_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmd_COPTC_TC002.Items.Clear();
            string sql = "select distinct  TC002 from COPTC where TC001 ='" + cmd_COPTC_TC001.Text + "' order by TC002";
            sqlERPCON conERP = new sqlERPCON();
            conERP.getComboBoxData(sql, ref cmd_COPTC_TC002);
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            getERPdata();
            datashow();
            DataView dv = dtshow.DefaultView;
            dv.Sort = "Shipping_Percent ASC";
            dgv_show.DataSource = dv;
           // dgv_show.DataSource = dtshow;
            dgv_show.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader;
            dgv_show.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgv_show.AutoGenerateColumns = true;
            dgv_show.DefaultCellStyle.Font = new Font("Verdana", 8, FontStyle.Regular);
            dgv_show.ColumnHeadersDefaultCellStyle.Font = new Font("Verdana", 10, FontStyle.Bold);
            dgv_show.AllowUserToAddRows = false;
            MakeAlarmForWarning(dgv_show);
        }
        private void MakeAlarmForWarning(DataGridView dtgv)
        {
            if (dtgv.Rows.Count > 0)
            {
                int count = 0;
                foreach (DataGridViewRow row in dtgv.Rows)
                {
                    double ShippingPercent = Convert.ToDouble(row.Cells["Shipping_Percent"].Value);
                    DateTime Deadline = DateTime.MinValue;
                    DateTime DeliveryDate = DateTime.MinValue;
                    object test = row.Cells["Client_Request_Date"].Value;
                    object delivery = row.Cells["Delivery_Date"].Value;
                    if (row.Cells["Client_Request_Date"].Value !=null  && row.Cells["Client_Request_Date"].Value.ToString().Length == 8)
                    {
                        Deadline=  Convert.ToDateTime(row.Cells["Client_Request_Date"].Value.ToString().Insert(4, "-").Insert(7, "-"));

                    }
                    if(row.Cells["Delivery_Date"].Value != null && row.Cells["Delivery_Date"].Value.ToString().Length == 8)
                    {
                        DeliveryDate = Convert.ToDateTime(row.Cells["Delivery_Date"].Value.ToString().Insert(4, "-").Insert(7, "-"));
                    }
                   
                  

                    if (ShippingPercent <100 && Deadline <= DateTime.Now)
                    {
                        dtgv["Shipping_Percent", count].Style.BackColor = Color.Red;//to color the row
                        dtgv["Client_Request_Date", count].Style.BackColor = Color.Red;
                       

                    }
                else if (ShippingPercent < 100 && Deadline <= DeliveryDate)
                    {
                        dtgv["Shipping_Percent", count].Style.BackColor = Color.DarkRed;//to color the row
                        dtgv["Client_Request_Date", count].Style.BackColor = Color.DarkRed;

                    }
                    else if (ShippingPercent >= 100 && DeliveryDate <= Deadline)
                    {
                        dtgv["Shipping_Percent", count].Style.BackColor = Color.Green;//to color the row
                        dtgv["Client_Request_Date", count].Style.BackColor = Color.Green;
                    }
                    else if (ShippingPercent >= 100 && DeliveryDate >= Deadline)
                    {
                        dtgv["Delivery_Date", count].Style.BackColor = Color.LightCyan;//to color the row
                        dtgv["Client_Request_Date", count].Style.BackColor = Color.LightCyan;
                    }
                    else if(ShippingPercent < 90 && Deadline >= DateTime.Now.AddDays(7))
                    {
                        dtgv["Shipping_Percent", count].Style.BackColor = Color.Orange;//to color the row
                        dtgv["Client_Request_Date", count].Style.BackColor = Color.Orange;
                    }
                  
                    count++;
                }



            }
        }
        void getERPdata()
        {
            DateTime dateto = dtp_to.Value.Date;
            DateTime datefrom = dtp_from.Value.Date;
            dt = new DataTable();
            StringBuilder sql = new StringBuilder();
            sql.Append(@"select
CONVERT(date,coptcs.CREATE_DATE) as Create_Date,
coptcs.TC001 as Code_Type, 
coptcs.TC002 as Code_No,
coptcs.TC012 as Customer_No

 from COPTC coptcs
where 1=1 ");
            if (cmd_COPTC_TC001.Text != "")
            {
                sql.Append(" and coptcs.TC001   = '" + cmd_COPTC_TC001.Text + "'");
            }
            if (cmd_COPTC_TC002.Text != "")
            {
                sql.Append(" and coptcs.TC002   = '" + cmd_COPTC_TC002.Text + "'");
            }
          //  else
            {
                sql.Append(" and CONVERT(date,coptcs.CREATE_DATE)  >= '" + datefrom + "' ");
                sql.Append(" and CONVERT(date,coptcs.CREATE_DATE) <= '" + dateto + "' ");
            }
            sql.Append(@" group by 
                                   coptcs.CREATE_DATE,
                                    coptcs.TC001 ,
                                    coptcs.TC002 ,
                                   coptcs.TC012
                                    ");
            sql.Append(" order by coptcs.TC001, coptcs.TC002");
            sqlERPCON con = new sqlERPCON();
            con.sqlDataAdapterFillDatatable(sql.ToString(), ref dt);
            //checkdata
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string sqlcheck = "";
                    string NgayTaoDon = dt.Rows[i]["Create_Date"].ToString().Replace("'", "");
                    string MaTaoDon = dt.Rows[i]["Code_Type"].ToString().Replace("'", "");
                    string codeDon = dt.Rows[i]["Code_No"].ToString().Replace("'", "");
                    string MAHK = dt.Rows[i]["Customer_No"].ToString().Replace("'","");
                    sqlcheck = "select COUNT(*) from t_OCTM where TM01 = '" + NgayTaoDon + "' and TM02 ='" + MaTaoDon + "' and TM03= '" + codeDon + "' and TM04 = '" + MAHK + "'";
                    sqlCON check = new sqlCON();
                    if (int.Parse(check.sqlExecuteScalarString(sqlcheck)) == 0) //insert
                    {
                        string list = "";
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            list += "'";
                            list += dt.Rows[i][j].ToString().Replace("'", "") + "',";
                        }
                        StringBuilder sqlinsert = new StringBuilder();
                        sqlinsert.Append("insert into t_OCTM ");
                        sqlinsert.Append(@"(TM01,TM02,TM03,TM04,UserName,datetimeRST) values ( ");
                        sqlinsert.Append(list);
                        sqlinsert.Append("'" + Class.valiballecommon.GetStorage().UserName + "',GETDATE())");
                        sqlCON insert = new sqlCON();
                        insert.sqlExecuteNonQuery(sqlinsert.ToString(), false);
                    }

                }
                
            }

        }

        void datashow()
        {
            dtshow = new DataTable();
            DateTime dateto = dtp_to.Value;
            DateTime datefrom = dtp_from.Value;
            StringBuilder sql = new StringBuilder();
            sql.Append(@"select CONVERT(date,a.TC01) as Create_Date, a.TC02 as Code_Type, a.TC03 as Code_No, a.TC04 as Clients_Code, a.TC05 as  Clients_Order_Code, a.TC06 as Product_Code, a.TC07 as Product_Name, avg(CAST(a.TC32 as float)) as Shipping_Percent , max(CONVERT(date,a.TC16)) as Delivery_Date, max(CONVERT(date,a.TC11)) as Client_Request_Date from t_OCTC a ");
            sql.Append("left join   t_OCTM b  on a.TC02 = b.TM02 and a.TC03 = b.TM03 where 1=1");
            if (cmd_COPTC_TC001.Text != "")
            {
                sql.Append(" and a.TC02   = '" + cmd_COPTC_TC001.Text + "'");
            }
            if (cmd_COPTC_TC002.Text != "")
            {
                sql.Append(" and a.TC03   = '" + cmd_COPTC_TC002.Text + "'");
            }
       //     else
            {
                sql.Append(" and CONVERT(date,a.TC01)  >= '" + datefrom  + "' ");
                sql.Append(" and CONVERT(date,a.TC01) <= '" + dateto  + "' ");
                //sql.Append(" and a.TC01 >=" + intdatefrom);
                //sql.Append(" and a.TC01 <=" + intdateto);
            }
            sql.Append(@" group by a.TC01, a.TC03 , a.TC02, a.TC04, a.TC05, a.TC06, a.TC07");
            sql.Append(" order by a.TC02,  a.TC03");
            sqlCON con = new sqlCON();
            con.sqlDataAdapterFillDatatable(sql.ToString(), ref dtshow);



            for (int i = 0; i < dtshow.Rows.Count; i++) ///update code
            {
                StringBuilder sqlupdate = new StringBuilder();
                sqlupdate.Append("update t_OCTM set ");
                sqlupdate.Append(@"TM12 = '" + dtshow.Rows[i]["Shipping_Percent"].ToString() + "',"); //percent
                if (double.Parse(dtshow.Rows[i]["Shipping_Percent"].ToString()) >= 100)
                {
                    sqlupdate.Append(@"TM11 = 'OK'");
                }
                else
                {
                    sqlupdate.Append(@"TM11 = 'NG'");
                }
                sqlupdate.Append(@" where TM02 = '" + dtshow.Rows[i]["Code_Type"].ToString() + "' and TM03 ='" + dtshow.Rows[i]["Code_No"].ToString() + "'");

                sqlCON update = new sqlCON();
                update.sqlExecuteNonQuery(sqlupdate.ToString(), false);
            }



        }
        private void dgv_show_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv_show.RowCount == 0)
            {
                return;
            }
            int i = e.RowIndex;
            int j = e.ColumnIndex;
            Class.valiballecommon va = Class.valiballecommon.GetStorage();
            va.value1 = dgv_show.Rows[dgv_show.SelectedCells[0].RowIndex].Cells["Code_Type"].Value.ToString();
            va.value2 = dgv_show.Rows[dgv_show.SelectedCells[0].RowIndex].Cells["Code_No"].Value.ToString();
            va.value3 = dtp_from.Value.ToString("yyyy-MM-dd");
            if (dgv_show.Rows[i].Cells["Shipping_Percent"].Selected)
            {
                ERPShowShipping shipping = new ERPShowShipping();
                shipping.ShowDialog();
            }

        }

        private void ShippingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ERPShowShipping shipping = new ERPShowShipping();
            shipping.ShowDialog();
        }

        private void ProductionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ERPShowOrder eRPShowOrder = new ERPShowOrder();
            eRPShowOrder.ShowDialog();
        }

        private void MaterialToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ERPMaterialShow eRPMaterialShow = new ERPMaterialShow();
            eRPMaterialShow.ShowDialog();
        }
    }
}
