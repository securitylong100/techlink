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
            dgv_show.DataSource = dtshow;
            alram();
            dgv_show.AutoGenerateColumns = true;
            dgv_show.DefaultCellStyle.Font = new Font("Verdana", 8, FontStyle.Regular);
            dgv_show.ColumnHeadersDefaultCellStyle.Font = new Font("Verdana", 10, FontStyle.Bold);
        }
        void getERPdata()
        {
            int intdateto = int.Parse(dtp_to.Value.ToString("yyyyMMdd"));
            int intdatefrom = int.Parse(dtp_from.Value.ToString("yyyyMMdd"));
            dt = new DataTable();
            StringBuilder sql = new StringBuilder();
            sql.Append(@"select
cast(coptcs.CREATE_DATE as int) as ngaytaodon,
coptcs.TC001 as malamdon, 
coptcs.TC002 as code,
coptcs.TC012 as makh
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
            else
            {
                sql.Append(" and coptcs.CREATE_DATE >=" + intdatefrom);
                sql.Append(" and coptcs.CREATE_DATE <=" + intdateto);
            }
            sql.Append(@"group by 
                                   coptcs.CREATE_DATE,
                                    coptcs.TC001 ,
                                    coptcs.TC002 ,
                                   coptcs.TC012
                                    ");
            sql.Append("order by coptcs.TC001, coptcs.TC002");
            sqlERPCON con = new sqlERPCON();
            con.sqlDataAdapterFillDatatable(sql.ToString(), ref dt);
            //checkdata
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string sqlcheck = "";
                    string NgayTaoDon = dt.Rows[i]["ngaytaodon"].ToString();
                    string MaTaoDon = dt.Rows[i]["malamdon"].ToString();
                    string codeDon = dt.Rows[i]["code"].ToString();
                    string MAHK = dt.Rows[i]["makh"].ToString();
                    sqlcheck = "select COUNT(*) from t_OCTM where TM01 = '" + NgayTaoDon + "' and TM02 ='" + MaTaoDon + "' and TM03= '" + codeDon + "' and TM04 = '" + MAHK + "'";
                    sqlCON check = new sqlCON();
                    if (int.Parse(check.sqlExecuteScalarString(sqlcheck)) == 0) //insert
                    {
                        string list = "";
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            list += "'";
                            list += dt.Rows[i][j].ToString() + "',";
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
            int intdateto = int.Parse(dtp_to.Value.ToString("yyyyMMdd"));
            int intdatefrom = int.Parse(dtp_from.Value.ToString("yyyyMMdd"));
            StringBuilder sql = new StringBuilder();
            sql.Append(@"select max(cast(a.TC01 as int)) as ngaytaodon, a.TC02 as malamdon, a.TC03 as Code, avg(CAST(a.TC32 as float)) as ShippingPercent , max(cast(a.TC15 as int)) as NgayGiaoHang, max(cast(a.TC10 as int)) as Deadline from t_OCTC a ");
            sql.Append("left join   t_OCTM b  on a.TC02 = b.TM02 and a.TC03 = b.TM03 where 1=1");
            if (cmd_COPTC_TC001.Text != "")
            {
                sql.Append(" and a.TC02   = '" + cmd_COPTC_TC001.Text + "'");
            }
            if (cmd_COPTC_TC002.Text != "")
            {
                sql.Append(" and a.TC03   = '" + cmd_COPTC_TC002.Text + "'");
            }
            else
            {
                sql.Append(" and a.TC01 >=" + intdatefrom);
                sql.Append(" and a.TC01 <=" + intdateto);
            }
            sql.Append(@" group by a.TC03 , a.TC02");
            sql.Append(" order by a.TC02,  a.TC03");
            sqlCON con = new sqlCON();
            con.sqlDataAdapterFillDatatable(sql.ToString(), ref dtshow);



            for (int i = 0; i < dtshow.Rows.Count; i++) ///update code and alram
            {
                StringBuilder sqlupdate = new StringBuilder();
                sqlupdate.Append("update t_OCTM set ");
                sqlupdate.Append(@"TM12 = '" + dtshow.Rows[i]["ShippingPercent"].ToString() + "',"); //percent
                if (double.Parse(dtshow.Rows[i]["ShippingPercent"].ToString()) >= 100)
                {
                    sqlupdate.Append(@"TM11 = 'OK'");
                }
                else
                {
                    sqlupdate.Append(@"TM11 = 'NG'");
                }
                sqlupdate.Append(@" where TM02 = '" + dtshow.Rows[i]["malamdon"].ToString() + "' and TM03 ='" + dtshow.Rows[i]["Code"].ToString() + "'");

                sqlCON update = new sqlCON();
                update.sqlExecuteNonQuery(sqlupdate.ToString(), false);
            }         
        }
        void alram()
        {
            if (dgv_show.Rows.Count > 0)
            {
                DateTime datetimeNow = DateTime.Now;
                for (int i = 0; i < dgv_show.Rows.Count; i++) // waring
                {
                    string strdateget = dgv_show.Rows[i].Cells["Deadline"].ToString().Insert(4, "-").Insert(7, "-");
                    
                    double shipingpercent =( dgv_show.Rows[i].Cells["ShippingPercent"].Value.ToString() == "") ?0: double.Parse(dgv_show.Rows[i].Cells["ShippingPercent"].ToString());
                    DateTime datetimeGet = Convert.ToDateTime(strdateget);

                    if (shipingpercent < 100 && shipingpercent < 90 && datetimeNow.AddDays(+7) < datetimeGet)// > 90 nho 100 con 1 tuan vàng
                    {
                        dgv_show.Rows[i].Cells["ShippingPercent"].Style.BackColor = Color.Yellow;
                    }
                    else if (shipingpercent < 100 && datetimeNow.AddDays(+7) < datetimeGet)// > 90 nho 100 con 1 tuan vàng
                    {
                        dgv_show.Rows[i].Cells["ShippingPercent"].Style.BackColor = Color.LightYellow;
                    }
                    else if (datetimeNow > datetimeGet && shipingpercent < 100) // <100. thoi gian qua, do
                    {
                        dgv_show.Rows[i].Cells[12].Style.BackColor = Color.Red;
                    }



                }
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
            va.value1 = dgv_show.Rows[dgv_show.SelectedCells[0].RowIndex].Cells["malamdon"].Value.ToString();
            va.value2 = dgv_show.Rows[dgv_show.SelectedCells[0].RowIndex].Cells["Code"].Value.ToString();
            if (dgv_show.Rows[i].Cells["ShippingPercent"].Selected)
            {
                ERPShowShipping shipping = new ERPShowShipping();
                shipping.ShowDialog();
            }

        }
    }
}
