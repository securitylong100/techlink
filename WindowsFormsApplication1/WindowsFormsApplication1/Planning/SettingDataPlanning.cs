using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1.Planning
{
    public partial class SettingDataPlanning : CommonForm
    {
        List<SettingBOM> settingBOMs;
        LoadDataPlanning loadData;
        List<SettingManufacture> settingManufactures;
        public SettingDataPlanning()
        {
            InitializeComponent();
            LoadingSettingBOM();
         //   IntilizialBOMSetting();
        }
       private void IntilizialBOMSetting()
        {
            settingBOMs = new List<SettingBOM>();
           loadData = new LoadDataPlanning();

            settingBOMs = loadData.GetSettingBOMs();
            loadData.InsertToManufactureSettingTableIntilizer(settingBOMs);
        }
        private void LoadingSettingBOM()
        {
            settingBOMs = new List<SettingBOM>();
        loadData = new LoadDataPlanning();

            settingBOMs = loadData.LoadingSettingBOMs();
            dtgv_SettingBom.DataSource = settingBOMs;
        }

        private void Dtgv_SettingBom_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            int RowIndex = e.RowIndex;
            int ColumnsIndex = e.ColumnIndex;
            if (RowIndex >= 0 && RowIndex >= 0)
            {
                txt_productName.Text = dtgv_SettingBom.Rows[e.RowIndex].Cells[0].Value.ToString();
                txt_productNo.Text = dtgv_SettingBom.Rows[e.RowIndex].Cells[1].Value.ToString();
                nmr_QtyPackage.Value = int.Parse(dtgv_SettingBom.Rows[e.RowIndex].Cells[2].Value.ToString());
                nmr_toolpcs.Value = int.Parse(dtgv_SettingBom.Rows[e.RowIndex].Cells[2].Value.ToString());
            }
        }

        private void Btn_Update_Click(object sender, EventArgs e)
        {
            if(txt_productNo.Text != "")
            {
                try
                {
                    int intQtyPacking = (int)nmr_QtyPackage.Value;
                    int intQtyTool = (int)nmr_toolpcs.Value;
                    loadData.UpdateToDatabase(txt_productNo.Text.Trim(), intQtyPacking, intQtyTool);
                    settingBOMs = loadData.LoadingSettingBOMs();
                    dtgv_SettingBom.DataSource = settingBOMs;
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        private void Btn_delete_Click(object sender, EventArgs e)
        {
            if (txt_productNo.Text != "")
            {
                try
                {
                    int intQtyPacking = (int)nmr_QtyPackage.Value;
                    int intQtyTool = (int)nmr_toolpcs.Value;
                    loadData.DeleteRowofProduct(txt_productNo.Text.Trim());
                    settingBOMs = loadData.LoadingSettingBOMs();
                    dtgv_SettingBom.DataSource = settingBOMs;
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        private void Btn_Add_Click(object sender, EventArgs e)
        {
            if (txt_productNo.Text != "" && txt_productName.Text != "")
            {
                try
                {
                    int intQtyPacking = (int)nmr_QtyPackage.Value;
                    int intQtyTool = (int)nmr_toolpcs.Value;
                    loadData.InsertRowofProduct(txt_productName.Text.Trim(), txt_productNo.Text.Trim(), intQtyPacking, intQtyTool);
                    settingBOMs = loadData.LoadingSettingBOMs();
                    dtgv_SettingBom.DataSource = settingBOMs;
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        private void Dtgv_SettingBom_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int RowIndex = e.RowIndex;
            int ColumnsIndex = e.ColumnIndex;
            if (RowIndex >= 0 && ColumnsIndex >= 0)
            {
                txt_productName.Text = dtgv_SettingBom.Rows[e.RowIndex].Cells[0].Value.ToString();
                txt_productNo.Text = dtgv_SettingBom.Rows[e.RowIndex].Cells[1].Value.ToString();
                nmr_QtyPackage.Value = int.Parse(dtgv_SettingBom.Rows[e.RowIndex].Cells[2].Value.ToString());
                nmr_toolpcs.Value = int.Parse(dtgv_SettingBom.Rows[e.RowIndex].Cells[3].Value.ToString());
            }
        }


        private void Btn_Search_Click(object sender, EventArgs e)
        {

            settingBOMs = new List<SettingBOM>();
            loadData = new LoadDataPlanning();
            settingBOMs = loadData.LoadingSettingBOMsFilter(txt_productFilter.Text.Trim());
            dtgv_SettingBom.DataSource = settingBOMs;
        }
        private void LoadingSettingManufacture()
        {
            settingManufactures = new List<SettingManufacture>();
            loadData = new LoadDataPlanning();

            settingManufactures = loadData.LoadingSettingManufacture();
            dtgv_manufacture.DataSource = settingManufactures;
        }
        private void Tabcontrol_Selected(object sender, TabControlEventArgs e)
        {
            int intTabIndex = e.TabPageIndex;
            if(intTabIndex == 0)
            {
                LoadingSettingBOM();
            }
            else if (intTabIndex == 1)
            {
                LoadingSettingManufacture();
            }
        }

        private void Dtgv_manufacture_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int RowIndex = e.RowIndex;
            int ColumnsIndex = e.ColumnIndex;
            if (RowIndex >= 0 && ColumnsIndex >= 0)
            {
                txt_productsInput2.Text = dtgv_manufacture.Rows[e.RowIndex].Cells[0].Value.ToString();
                txt_productNoInput2.Text = dtgv_manufacture.Rows[e.RowIndex].Cells[1].Value.ToString();
                nmr_workersQty.Value = int.Parse(dtgv_manufacture.Rows[e.RowIndex].Cells[2].Value.ToString());
                nmr_WorkerPerformance.Value = int.Parse(dtgv_manufacture.Rows[e.RowIndex].Cells[3].Value.ToString());
            }
        }

        private void Btn_Add2_Click(object sender, EventArgs e)
        {
            if (txt_productNoInput2.Text != "" && txt_productsInput2.Text != "")
            {
                try
                {
                    int intWorkerQty = (int)nmr_workersQty.Value;
                    int intWorkerTarget = (int)nmr_WorkerPerformance.Value;
                    loadData.InsertRowofManufacture(txt_productsInput2.Text.Trim(), txt_productNoInput2.Text.Trim(), intWorkerQty, intWorkerTarget);
                    settingManufactures = loadData.LoadingSettingManufacture();
                    dtgv_manufacture.DataSource = settingManufactures;
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        private void Btn_Update2_Click(object sender, EventArgs e)
        {
            if (txt_productNoInput2.Text != "")
            {
                try
                {
                    int intWorkerQty = (int)nmr_workersQty.Value;
                    int intWorkertarget= (int)nmr_WorkerPerformance.Value;
                    loadData.UpdateToManufacture(txt_productNoInput2.Text.Trim(), intWorkerQty, intWorkertarget);
                    settingManufactures = loadData.LoadingSettingManufacture();
                    dtgv_manufacture.DataSource = settingManufactures;
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        private void Btn_Delete2_Click(object sender, EventArgs e)
        {
            if (txt_productNoInput2.Text != "")
            {
                try
                {
                 
                    loadData.DeleteRowofManufaccture(txt_productNoInput2.Text.Trim());
                    settingManufactures = loadData.LoadingSettingManufacture();
                    dtgv_manufacture.DataSource = settingManufactures;
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        private void Txt_Search2_Click(object sender, EventArgs e)
        {
            settingManufactures = new List<SettingManufacture>();
            loadData = new LoadDataPlanning();
            settingManufactures = loadData.LoadingSettingManufactureFilter(txt_productFilter2.Text.Trim());
            dtgv_manufacture.DataSource = settingManufactures;
        }

       
    }
}
