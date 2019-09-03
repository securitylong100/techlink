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
    public partial class ERPShowShipping : CommonForm
    {
        public ERPShowShipping()
        {
            InitializeComponent();
        }
        DataTable dtmain;
        DataTable dt;
        private void cmd_COPTC_TC001_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sql = "select distinct  TC002 from COPTC where TC001 ='" + cmd_COPTC_TC001.Text + "' order by TC002";
            sqlERPCON conERP = new sqlERPCON();
            conERP.getComboBoxData(sql, ref cmd_COPTC_TC002);
        }

        private void ERPShowShipping_Load(object sender, EventArgs e)
        {
            string sql = "select distinct TC001 from COPTC where TC001 != '' order by TC001";
            sqlERPCON conERP = new sqlERPCON();
            conERP.getComboBoxData(sql, ref cmd_COPTC_TC001);
            if (Class.valiballecommon.GetStorage().value1 != null)
            {
                Class.valiballecommon va = Class.valiballecommon.GetStorage();
                cmd_COPTC_TC002.Text = Class.valiballecommon.GetStorage().value2;
                cmd_COPTC_TC001.Text = Class.valiballecommon.GetStorage().value1;
                va.value1 = null;
                va.value2 = null;
                btn_search_Click(sender, e);
            }
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            getERPdata();
            // dtShow = new DataTable();
            //datashow();

            dgv_show.DataSource = dt;
            //  getupdateMAINFORM();
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
coptcs.TC012 as makh, 
coptds.TD004 as codesp,
coptds.TD005 as nametensp,
coptds.TD008 as soluongdathang,
coptds.TD010 as donvi,
coptcs.TC005 as bophan,
coptds.TD047 as deadline,
sum(copths.TH008) as SLdagiao,
copths.TH004 as spduocgiao,
copths.TH001 as maDongiaohang,
copths.TH005 as namespdcgiao,
max(coptgs.TG003) as ngayGiaohang,
coptjs.TJ008 as SLDatralai,
coptis.TI003 as ngaytrahang,
 (sum(copths.TH008)/coptds.TD008)*100 as ShippingPercent
 from COPTC coptcs
left join COPTD  coptds on coptcs.TC002 = coptds.TD002  and coptcs.TC001 = coptds.TD001 -- cong doan tao don
left join MOCTB  moctbs on coptcs.TC002 = moctbs.TB002  and coptcs.TC001 = moctbs.TB001
left join COPTH copths on coptcs.TC002 = copths.TH015 and  coptcs.TC001 = copths.TH014--cong doan giao hang
left join COPTG coptgs on copths.TH002  = coptgs.TG002 and copths.TH001  = coptgs.TG001 --cong doan giao hang
left join COPTJ coptjs on coptcs.TC002 = coptjs.TJ019 and coptcs.TC001 = coptjs.TJ018-- cong doan tra hang
left join COPTI coptis on coptjs.TJ002 = coptis.TI002 and coptjs.TJ001 = coptis.TI001 --cong doan tra hang
where 1=1  
and copths.TH004  = coptds.TD004 ");
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
            con.sqlDataAdapterFillDatatable(sql.ToString(), ref dt);
            //checkdata
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string sqlcheck = "";

                    string MaTaoDon = dt.Rows[i]["malamdon"].ToString();
                    string codeDon = dt.Rows[i]["code"].ToString();
                    string codeSanPham = dt.Rows[i]["codesp"].ToString();
                    string ShippingPercent = dt.Rows[i]["ShippingPercent"].ToString();

                    sqlcheck = "select COUNT(*) from t_OCTC where TC02 = '" + MaTaoDon + "' and TC03 ='" + codeDon + "' and TC05='" + codeSanPham + "'";
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
                        sqlinsert.Append("insert into t_OCTC ");
                        sqlinsert.Append(@"(TC01,TC02,TC03,TC04,TC05,TC06,TC07,TC08,TC09,TC10,TC11,TC12,TC13,TC14,TC15,TC16,TC17,TC32,TC31,UserName,datetimeRST) values ( ");
                        sqlinsert.Append(list);
                        if (ShippingPercent != "")
                        {
                            if (double.Parse(ShippingPercent) >= 100)
                                sqlinsert.Append("'OK',");
                            else
                            {
                                sqlinsert.Append("'WAITING',");
                            }
                        }
                        else { sqlinsert.Append("'WAITING',"); }
                        sqlinsert.Append("'" + Class.valiballecommon.GetStorage().UserName + "',GETDATE())");
                        sqlCON insert = new sqlCON();
                        insert.sqlExecuteNonQuery(sqlinsert.ToString(), false);
                    }
                    else //update
                    {

                        StringBuilder sqlupdate = new StringBuilder();
                        sqlupdate.Append("update t_OCTC set ");
                        sqlupdate.Append(@"TC11 = '" + dt.Rows[i]["SLdagiao"].ToString() + "',");
                        sqlupdate.Append(@"TC15 = '" + dt.Rows[i]["ngayGiaohang"].ToString() + "',");
                        sqlupdate.Append(@"TC32 = '" + ShippingPercent + "',");
                        if (ShippingPercent != "")
                        {
                            if (double.Parse(ShippingPercent) >= 100)
                                sqlupdate.Append(@"TC31 = 'OK'");
                            else
                            {
                                sqlupdate.Append(@"TC31 = 'WAITING'");
                            }
                        }
                        else { sqlupdate.Append(@"TC31 = 'WAITING'"); }
                        sqlupdate.Append(@" where TC02 = '" + MaTaoDon + "' and TC03 ='" + codeDon + "' and TC05='" + codeSanPham + "'");

                        sqlCON update = new sqlCON();
                        update.sqlExecuteNonQuery(sqlupdate.ToString(), false);
                    }
                }
            }
        }

    }
}
