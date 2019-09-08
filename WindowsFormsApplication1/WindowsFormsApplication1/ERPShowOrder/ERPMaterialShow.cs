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
    public partial class ERPMaterialShow : CommonForm
    {
        public ERPMaterialShow()
        {
            InitializeComponent();
            AcceptButton = btn_search;

        }
        DataTable dt;
        DataTable dtShow;
        private void ERPMaterialShow_Load(object sender, EventArgs e)
        {

            string sql_cmb_COPTC_TC001 = @"select distinct
moctas.TA026 as MaDDH
from MOCTA moctas
where moctas.TA026 != '' and moctas.TA027 != '' and moctas.TA013 = 'Y'";
            sqlERPCON conERP = new sqlERPCON();
            conERP.getComboBoxData(sql_cmb_COPTC_TC001, ref cmb_COPTC_TC001);
            if (cmb_COPTC_TC001.Items != null)
            {
                cmb_COPTC_TC001.SelectedIndex = 0;

            }
            //string sql_cmb_MOCTA_TA001 = "select distinct TA001 from MOCTA where TA001 != '' order by TA001";
            // conERP = new sqlERPCON();
            //conERP.getComboBoxData(sql_cmb_MOCTA_TA001, ref cmb_MOCTA_TA001);
        }
        private void cmd_COPTC_TC001_SelectedIndexChanged(object sender, EventArgs e)
        {

            string sql = "select distinct  TA027 from MOCTA where TA027 !='' and TA013 ='Y' and TA026 ='" + cmb_COPTC_TC001.Text + "' order by TA027";
            sqlERPCON conERP = new sqlERPCON();
            conERP.getComboBoxData(sql, ref cmb_COPTC_TC002);
            if (cmb_COPTC_TC002.Items != null)
            {
                cmb_COPTC_TC002.SelectedIndex = 0;
                datashow();

            }
            
        }
        private void cmb_COPTC_TC002_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sql = "select distinct  TA001, TA002 from MOCTA where  TA013 ='Y' and  TA026 ='" + cmb_COPTC_TC001.Text + "' and  TA027 ='" + cmb_COPTC_TC002.Text + "'  order by TA001";
            sqlERPCON conERP = new sqlERPCON();
            conERP.getComboBoxData(sql, ref cmb_MOCTA_TA001, ref cmb_MOCTA_TA002);
            if (cmb_MOCTA_TA001.Items != null)
            {
                cmb_MOCTA_TA001.Items.Add("");
                cmb_MOCTA_TA001.SelectedIndex = 0;

            }
            if (cmb_MOCTA_TA002.Items != null)
            {
                cmb_MOCTA_TA002.Items.Add("");
                cmb_MOCTA_TA002.SelectedIndex = 0;

            }


        }
        private void cmb_MOCTA_TA001_SelectedIndexChanged(object sender, EventArgs e)
        {
            //string sql = "select distinct  TA002 from MOCTA where TA001 ='" + cmb_MOCTA_TA001.Text + "' order by TA002";
            //sqlERPCON conERP = new sqlERPCON();
            //conERP.getComboBoxData(sql, ref cmb_MOCTA_TA002);
        }
        private void btn_search_Click(object sender, EventArgs e)
        {
            getERPdata();
            // dtShow = new DataTable();
            //datashow();

            dtgv_material.DataSource = dt;
            dtgv_material.AutoGenerateColumns = true;
            dtgv_material.DefaultCellStyle.Font = new Font("Verdana", 8, FontStyle.Regular);
            dtgv_material.ColumnHeadersDefaultCellStyle.Font = new Font("Verdana", 10, FontStyle.Bold);
        }
        void getERPdata()
        {
            int intdateto = int.Parse(dtp_to.Value.ToString("yyyyMMdd"));
            int intdatefrom = int.Parse(dtp_from.Value.ToString("yyyyMMdd"));
            dt = new DataTable();
            StringBuilder sql = new StringBuilder();
            sql.Append(@"
                            select
cast (moctas.CREATE_DATE as int) as NgayTaoLenh,
moctas.TA026 as DonDatHang,
moctas.TA027 as SoDonDatHang,
moctas.TA001 as LenhSanXuat,
moctas.TA002 as SoLenhSanXuat,
moctas.TA003 as NgayLapDon,
moctas.TA006 as MaSanPham,
moctas.TA034 as TenSanPham,
moctas.TA009 as DuTinhBatdauSx,
moctas.TA010 as DuTinhHoanThanh,
moctas.TA012 as ThucTeSanXuat,
moctas.TA013 as xacnhan,
moctas.TA015 as SoluongDuTinh,
moctas.TA015 as SoluongThuclanh,
moctbs.TB003 as MaVatLieu,
moctbs.TB012 as TenSpNguyenVatLieu,
moctbs.TB009 as MaKho,
--invmcs.MC002 as VitriKho,
moctbs.TB015 as NgayDuTinhLanhVL,

moctbs.TB004 as SLVatLieuCanLanh,
moctbs.TB005 as SLVatLieuDaDung,
sum(invmcs.MC007) as SLVatLIeuTrongKho,
moctbs.TB007 as DonviVatLieu

from MOCTA moctas
left join MOCTB moctbs on moctas.TA001 = moctbs.TB001 and moctas.TA002 = moctbs.TB002
left join INVMB invmbs on moctbs.TB003 = invmbs.MB001
left join INVMC invmcs on moctbs.TB003 = invmcs.MC001
                            where 1=1 ");
            if ((string)cmb_MOCTA_TA001.SelectedItem != "")
            {
                sql.Append(" and moctas.TA001   = '" + (string)cmb_MOCTA_TA001.SelectedItem + "'");
            }
            if ((string)cmb_MOCTA_TA002.SelectedItem != "")
            {
                sql.Append(" and moctas.TA002   = '" + (string)cmb_MOCTA_TA002.SelectedItem + "'");
            }
            if ((string)cmb_COPTC_TC001.SelectedItem != "")
            {
                sql.Append(" and moctas.TA026   = '" + (string)cmb_COPTC_TC001.SelectedItem + "'");
            }
            if ((string)cmb_COPTC_TC002.SelectedItem != "")
            {
                sql.Append(" and moctas.TA027   = '" + (string)cmb_COPTC_TC002.SelectedItem + "'");
            }
            //  else
            {
                sql.Append(" and moctas.CREATE_DATE >=" + intdatefrom);
                sql.Append(" and moctas.CREATE_DATE <=" + intdateto);
            }

            sql.Append(@"group by 
                                    moctas.CREATE_DATE,
                                    moctas.TA001 ,
                                    moctas.TA002 ,
                                    moctas.TA003,
                                    moctas.TA006,
                                    moctas.TA009 ,
                                    moctas.TA010 ,
                                    moctas.TA012 ,
                                    moctas.TA013 ,
                                    moctas.TA015,
                                    moctas.TA015,
                                    moctas.TA024 ,
                                    moctas.TA025,
                                    moctas.TA026 ,
                                    moctas.TA027,
                                    moctas.TA034,
                                    moctbs.TB003,
                                    moctbs.TB012,
                                    moctbs.TB009,
                                   -- invmcs.MC002,
                                    moctbs.TB015,
                                    moctbs.TB004,
                                    moctbs.TB005,
                                    moctbs.TB007
");

            sql.Append("order by moctas.TA002");
            sqlERPCON con = new sqlERPCON();
            con.sqlDataAdapterFillDatatable(sql.ToString(), ref dt);
            //checkdata
            if (dt.Rows.Count > 0)
            {
                try
                {


                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string sqlcheck = "";

                        string MaDDH = dt.Rows[i]["DonDatHang"].ToString();
                        string SoDDH = dt.Rows[i]["SoDonDatHang"].ToString();
                        string MaLSX = dt.Rows[i]["LenhSanXuat"].ToString();
                        string SoLSX = dt.Rows[i]["SoLenhSanXuat"].ToString();
                        string codeSanPham = dt.Rows[i]["MaSanPham"].ToString();
                        string MaVatLieu = dt.Rows[i]["MaVatLieu"].ToString();

                        double SoNVLCanLanh = double.Parse(dt.Rows[i]["SLVatLieuCanLanh"].ToString());
                        double SoNVLTrongKho = double.Parse(dt.Rows[i]["SLVatLIeuTrongKho"].ToString());


                        sqlcheck = @"select COUNT(*) from t_OCTD where TD02 = '" + MaDDH + "' and TD03 ='" + SoDDH + "' and TD04='" + MaLSX + "' and TD05='" + SoLSX
                         + "' and TD07 ='" + codeSanPham + "' and TD15 ='" + MaVatLieu + "'";
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
                            sqlinsert.Append("insert into t_OCTD ");
                            sqlinsert.Append(@"(TD01,TD02,TD03,TD04,TD05,TD06,TD07,TD08,TD09,TD10,TD11,TD12,TD13,TD14,TD15,TD16,TD17,TD18,TD19,TD20,TD21,TD22,TD31,TD32,TD33,UserName,datetimeRST) values ( ");
                            sqlinsert.Append(list);
                            if (SoNVLTrongKho > SoNVLCanLanh)
                            {
                                sqlinsert.Append("'OK', 'OK' , '0' " + ",");
                            }
                            else if (SoNVLTrongKho == SoNVLCanLanh)
                            {
                                sqlinsert.Append("'OK', 'OK' , '1' " + ",");
                            }
                            else if (SoNVLTrongKho < SoNVLCanLanh)
                            {
                                sqlinsert.Append("'NG', 'NG' , '2' " + ",");

                            }
                            sqlinsert.Append("'" + Class.valiballecommon.GetStorage().UserName + "',GETDATE())");
                            sqlCON insert = new sqlCON();
                            insert.sqlExecuteNonQuery(sqlinsert.ToString(), false);
                        }
                        else //update
                        {

                            StringBuilder sqlupdate = new StringBuilder();
                            sqlupdate.Append("update t_OCTD set ");
                            sqlupdate.Append(@"TD19 = '" + dt.Rows[i]["SLVatLieuCanLanh"].ToString() + "',");
                            sqlupdate.Append(@"TD20 = '" + dt.Rows[i]["SLVatLieuDaDung"].ToString() + "',");
                            sqlupdate.Append(@"TD21 = '" + dt.Rows[i]["SLVatLIeuTrongKho"].ToString() + "'");
                            if (SoNVLTrongKho > SoNVLCanLanh)
                            {
                                sqlupdate.Append(@", TD31 = 'OK' ,");
                                sqlupdate.Append(@" TD32 = 'OK' ,");
                                sqlupdate.Append(@"TD33 = '0', ");
                            }
                            else if (SoNVLTrongKho == SoNVLCanLanh)
                            {
                                sqlupdate.Append(@", TD31 = 'OK' ,");
                                sqlupdate.Append(@" TD32 = 'OK' ,");
                                sqlupdate.Append(@"TD33 = '1' ,");
                            }
                            else if (SoNVLTrongKho < SoNVLCanLanh)
                            {
                                sqlupdate.Append(@", TD31 = 'NG' ,");
                                sqlupdate.Append(@" TD32 = 'NG' ,");
                                sqlupdate.Append(@"TD33 = '2' ,");
                            }
                            sqlupdate.Append(@" UserName = '" + Class.valiballecommon.GetStorage().UserName + "' ,");
                            sqlupdate.Append(@" datetimeRST = GETDATE()");

                            sqlupdate.Append(@" where TD02 = '" + MaDDH + "' and TD03 ='" + SoDDH + "' and TD04='" + MaLSX + "' and TD05='" + SoLSX
                         + "' and TD07 ='" + codeSanPham + "' and TD15 ='" + MaVatLieu + "'");

                            sqlCON update = new sqlCON();
                            update.sqlExecuteNonQuery(sqlupdate.ToString(), false);
                        }
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show("Update or Insert to database fail: " + ex.Message, "Error");
                }
            }
        }
        void datashow()
        {
            int intdateto = int.Parse(dtp_to.Value.ToString("yyyyMMdd"));
            int intdatefrom = int.Parse(dtp_from.Value.ToString("yyyyMMdd"));
            dtShow = new DataTable();
            StringBuilder sql = new StringBuilder();
           
            sql.Append(@"select
cast (TD01 as int) as NgayTaoDon,
TD02 as DonDatHang,
TD03  as SoDonDatHang,
TD04 as LenhSanXuat,
TD05 as SoLenhSanXuat,
TD06 as NgayLapDon,
TD07 as MaSanPham,
TD08 as TenSanPham,
TD09 as DuTinhBatdauSx,
TD10 as DuTinhHoanThanh,
TD11 as ThucTeSanXuat,
TD12 as xacnhan,
TD13 as SoluongDuTinh,
TD14 as SoluongThuclanh,
TD15 as MaVatLieu,
TD16 as TenSpNguyenVatLieu,
TD17 as MaKho,
TD18 as NgayDuTinhLanhVL,
TD19 as SLVatLieuCanLanh,
TD20 as SLVatLieuDaDung,
TD21 as SLVatLIeuTrongKho,
TD22 as DonviVatLieu
from t_OCTD
where 1=1");
            if ((string)cmb_COPTC_TC001.SelectedItem != "")
            { 
                sql.Append(" and TD02   = '" + (string)cmb_COPTC_TC001.SelectedItem + "'");
            }
            if ((string) cmb_COPTC_TC002.SelectedItem != "")
            {
                sql.Append(" and TD03   = '" + (string)cmb_COPTC_TC002.SelectedItem + "'");
            }
           // else
            {
                sql.Append(" and TD01 >=" + intdatefrom);
                sql.Append(" and TD01 <=" + intdateto);
            }
            sql.Append("order by TD02");
            sqlCON con = new sqlCON();
            con.sqlDataAdapterFillDatatable(sql.ToString(), ref dtShow);
            //FormShowTest form = new FormShowTest((string)cmb_COPTC_TC001.SelectedItem, (string)cmb_COPTC_TC002.SelectedItem, dtShow);
            //form.ShowDialog();
        }
    }


}

