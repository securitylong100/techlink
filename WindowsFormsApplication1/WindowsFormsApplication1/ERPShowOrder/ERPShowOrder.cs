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
    public partial class ERPShowOrder : CommonForm
    {
        public ERPShowOrder()
        {
            InitializeComponent();
            AcceptButton = btn_search;
        }
        DataTable dt;
        DataTable dtShow;
      
        private void btn_search_Click(object sender, EventArgs e)
        {
            getERPdata();
            dtShow = new DataTable();
            datashow();

            dgv_show.DataSource = dtShow;
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
            sql.Append(@"
                            select
                            cast (moctas.CREATE_DATE as int) as NgayTaoLenh,
                            moctas.TA001 as MaTaoDon,
                            moctas.TA002 as codeTaoLenh,
                            moctas.TA003 as NgayLapDon,
                            moctas.TA006 as MaSanPham, 
                            moctas.TA009 as DuTinhBatdauSx,
                            moctas.TA010 as DuTinhHoanThanh,
                            moctas.TA012 as ThucTeSanXuat,
                            moctas.TA013 as xacnhan,
                            moctas.TA015 as SoluongDuTinh,
                            moctas.TA015 as SoluongThuclanh,
                            moctas.TA024 as LenhSanxuatcaptren,
                            moctas.TA025 as MaLenhSanXuatCapTren,
                            moctas.TA026 as LoaiDonHang,
                            moctas.TA027 as MaDonHang,
                            moctas.TA034 as TenSanPham,
                            moctgs.TG007 as DonViNhapKho, 

                            sum(moctgs.TG011) as SoluongNhapKho,
                            sum(moctgs.TG012) as SoluongBaoPhe,
                            sum(moctgs.TG013) as SoluongNghiemThu,

                            --moctgs.TG034 as VitriLuuKhoTP,
                            max(moctfs.TF003) as NgayNhapKhoTP
                            from MOCTA moctas
                            left join MOCTG moctgs on moctgs.TG014 = moctas.TA001 and moctgs.TG015 = moctas.TA002
                            left join MOCTF moctfs on moctfs.TF001 = moctgs.TG001 and moctfs.TF002 = moctgs.TG002
                            where 1=1 ");
            if (cmd_MOCTA_TA001.Text != "")
            {
                sql.Append(" and moctas.TA001   = '" + cmd_MOCTA_TA001.Text + "'");
            }
            if (cmd_MOCTA_TA002.Text != "")
            {
                sql.Append(" and moctas.TA002   = '" + cmd_MOCTA_TA002.Text + "'");
            }
            else
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
                                    moctas.TA034 ,
                                    moctgs.TG007 ");

            sql.Append("order by moctas.TA002");
            sqlERPCON con = new sqlERPCON();
            con.sqlDataAdapterFillDatatable(sql.ToString(), ref dt);
            //checkdata
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    string MaTaoDon = dt.Rows[i][1].ToString();
                    string codeTaoLenh = dt.Rows[i][2].ToString();
                    string sqlcheck = "select COUNT(*) from t_OCTB where TB02 = '" + MaTaoDon + "' and TB03 ='" + codeTaoLenh + "'";
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
                        sqlinsert.Append("insert into t_OCTB ");
                        sqlinsert.Append(@"(TB01,TB02,TB03,TB04,TB05,TB06,TB07,TB08,TB09,TB10,TB11,TB12,TB13,TB14,TB15,TB16,TB17,TB18,TB19,TB20,TB21,UserName,datetimeRST) values ( ");
                        sqlinsert.Append(list);
                        sqlinsert.Append("'" + Class.valiballecommon.GetStorage().UserName + "',GETDATE())");
                        sqlCON insert = new sqlCON();
                        insert.sqlExecuteNonQuery(sqlinsert.ToString(), false);
                    }
                    else //update
                    {
                        StringBuilder sqlupdate = new StringBuilder();
                        sqlupdate.Append("update t_OCTB set ");
                        sqlupdate.Append(@"TB18 = '" + dt.Rows[i][17].ToString() + "',");
                        sqlupdate.Append(@"TB19 = '" + dt.Rows[i][18].ToString() + "',");
                        sqlupdate.Append(@"TB20 = '" + dt.Rows[i][19].ToString() + "',");
                        sqlupdate.Append(@"TB21 = '" + dt.Rows[i][20].ToString() + "'");
                        sqlupdate.Append(@" where TB02 = '" + MaTaoDon + "' and TB03 ='" + codeTaoLenh + "'");

                        sqlCON update = new sqlCON();
                        update.sqlExecuteNonQuery(sqlupdate.ToString(), false);
                    }
                }
            }
        }
        void datashow()
        {
            int intdateto = int.Parse(dtp_to.Value.ToString("yyyyMMdd"));
            int intdatefrom = int.Parse(dtp_from.Value.ToString("yyyyMMdd"));
            StringBuilder sql = new StringBuilder();
            sql.Append(@"select cast (TB01 as int) as NgayTaoLenh,TB02 as MaTaoDon,TB03 as codeTaoLenh,TB04 as NgayLapDon,TB05 as MaSanPham,TB06 as DuTinhBatdauSx,");
            sql.Append("TB07 as DuTinhHoanThanh,TB08 as ThucTeSanXuat,TB09 as xacnhan,TB10 as SoluongDuTinh,TB11 as SoluongThuclanh,TB12 as LenhSanxuatcaptren,");
            sql.Append("TB13 as MaLenhSanXuatCapTren,TB14 as LoaiDonHang,TB15 as MaDonHang,TB16 as TenSanPham,TB17 as DonViNhapKho,TB18 as SoluongNhapKho,");
            sql.Append("TB19 as SoluongBaoPhe,TB20 as SoluongNghiemThu,TB21 as NgayNhapKhoTP, ");
            sql.Append("TB30, TB31, TB32, TB33 ");
                sql.Append("from t_OCTB where 1 = 1 ");
            if (cmd_MOCTA_TA001.Text != "")
            {
                sql.Append(" and TB02   = '" + cmd_MOCTA_TA001.Text + "'");
            }
            if (cmd_MOCTA_TA002.Text != "")
            {
                sql.Append(" and TB03   = '" + cmd_MOCTA_TA002.Text + "'");
            }
            else
            {
                sql.Append(" and TB01 >=" + intdatefrom);
                sql.Append(" and TB01 <=" + intdateto);
            }
            sql.Append("order by TB02");
            sqlCON con = new sqlCON();
            con.sqlDataAdapterFillDatatable(sql.ToString(), ref dtShow);
        }
        private void ERPShowOrder_Load(object sender, EventArgs e)
        {
            string sql = "select distinct TA001 from MOCTA where TA001 != '' order by TA001";
            sqlERPCON conERP = new sqlERPCON();
            conERP.getComboBoxData(sql, ref cmd_MOCTA_TA001);
        }

        private void cmd_MOCTA_TA001_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sql = "select distinct  TA002 from MOCTA where TA001 ='" + cmd_MOCTA_TA001.Text + "' order by TA002";
            sqlERPCON conERP = new sqlERPCON();
            conERP.getComboBoxData(sql, ref cmd_MOCTA_TA002);
        }


    }
}
