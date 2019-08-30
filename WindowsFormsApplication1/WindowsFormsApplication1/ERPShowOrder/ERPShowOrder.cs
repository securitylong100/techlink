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
        void sqlmain()
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"
select
cast (moctas.CREATE_DATE as int) as NgayTaoLenh,
moctas.TA001 as MaTaoDon,
moctas.TA002 as codeTaoLenh,
moctas.TA003 as NgayLapDon,
moctas.TA006 as MaSanPham, --ED-TFR-I479A-BLACK -ED-TFR-I479A-BLACK
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
moctas.TA034 as TenSanPham,--1504138169 4138169 JOINT CV D245 ESSENTIAL
--
moctbs.TB003 as MaNguyenVatLieu,
moctbs.TB004 as SLCanLenhLyThuyet,
moctbs.TB005 as SLDaDungThucTe,
moctbs.TB007 as DV_NVL,
moctbs.TB009 as MaKhoNVL,
moctbs.TB012 as TenNVL,
moctbs.TB014 as MaCodeChaCuaNVL,
moctbs.TB015 as NgayDuTinhLanhVL,
--Tim trong kho con SL vat lieu bao nhieu
--invmbs.MB001 AS MaNguyenVatLieu,
--invmbs.MB002 as TenNguyenVatLieu,
--tim vi tri kho va kho luong
invmcs.MC002 as VitriKhoNVL,
invmcs.MC007 as SoluongTrongKhoNVL,
-- tim so luong nhap kho
--moctgs.TG004 as MaSanPhamTP, --bo duoc
--moctgs.TG005 as TenSanPhamTp, -- bo duoc

moctgs.TG010 as LoaiKho,
moctgs.TG011 as SoluongNhapKho,
moctgs.TG012 as SoluongBaoPhe,
moctgs.TG013 as SoluongNghiemThu,
moctgs.TG007 as DonViNhapKho,
--moctgs.TG014 as LenhSanXuat,
--moctgs.TG015 as MaLenhSanXuat, 
moctgs.TG034 as VitriLuuKhoTP,
moctfs.TF003 as NgayNhapKhoTP

from MOCTA moctas
left join MOCTB moctbs on moctas.TA001 = moctbs.TB001 and moctas.TA002 = moctbs.TB002
left join INVMB invmbs on moctbs.TB003 = invmbs.MB001
left join INVMC invmcs on moctbs.TB003 = invmcs.MC001
left join MOCTG moctgs on moctgs.TG014 = moctas.TA001 and moctgs.TG015 = moctas.TA002
left join MOCTF moctfs on moctfs.TF001 = moctgs.TG001 and moctfs.TF002 = moctgs.TG002
where 1=1 ");
        }
        private void btn_search_Click(object sender, EventArgs e)
        {
            getERPdata();

            dgv_show.DataSource = dt;
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

            sql.Append(" and moctas.CREATE_DATE >=" + intdatefrom);
            sql.Append(" and moctas.CREATE_DATE <=" + intdateto);
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
            //if (dt.Rows.Count > 1)
            //{
            //    for (int i = 0; i < dt.Rows.Count; i++)
            //    {
            //        string sqlcheck = "";
            //        string a = dt.Rows[i][1].ToString();
            //        string b = dt.Rows[i][2].ToString();
            //        sqlcheck = "select * from t_OCTB where OCT02 = '" + a + "' and OCT03 ='"+ b+ "'";
            //                            sqlCON check = new sqlCON();
            //        check.sqlExecuteScalarString(sqlcheck);
            //    }
            //}
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
