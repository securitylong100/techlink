using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace WindowsFormsApplication1.Class
{
    public  class ToolSupport
    {
        public void Datagridview2Excel(DataGridView dataGrid, string pathSaveExcel)

        {
            string stOutput = "";
            // Export titles:
            string sHeaders = "";

            for (int j = 0; j < dataGrid.Columns.Count; j++)
                sHeaders = sHeaders.ToString() + Convert.ToString(dataGrid.Columns[j].HeaderText) + "\t";
            stOutput += sHeaders + "\r\n";
            // Export data.
            for (int i = 0; i < dataGrid.RowCount - 1; i++)
            {
                string stLine = "";
                for (int j = 0; j < dataGrid.Rows[i].Cells.Count; j++)
                    stLine = stLine.ToString() + Convert.ToString(dataGrid.Rows[i].Cells[j].Value) + "\t";
                stOutput += stLine + "\r\n";
            }
            Encoding utf16 = Encoding.GetEncoding(1254);
            byte[] output = utf16.GetBytes(stOutput);
            FileStream fs = new FileStream(pathSaveExcel, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(output, 0, output.Length); //write the encoded file
            bw.Flush();
            bw.Close();
            fs.Close();

        }
        public void DatagridviewToExcel(DataGridView dataGrid, string pathSaveExcel)
        {
            try
            {
                Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
                excel.Visible = true;
                Microsoft.Office.Interop.Excel.Workbook workbook = excel.Workbooks.Add(System.Reflection.Missing.Value);
                Microsoft.Office.Interop.Excel.Worksheet sheet1 = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Sheets[1];
                int StartCol = 1;
                int StartRow = 1;
                int j = 0, i = 0;

                //Write Headers
                for (j = 0; j < dataGrid.Columns.Count; j++)
                {
                    Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[StartRow, StartCol + j];
                    myRange.Value2 = dataGrid.Columns[j].HeaderText;
                }

                StartRow++;

                //Write datagridview content
                for (i = 0; i < dataGrid.Rows.Count; i++)
                {
                    for (j = 0; j < dataGrid.Columns.Count; j++)
                    {
                        try
                        {
                            Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[StartRow + i, StartCol + j];
                            myRange.Value2 = dataGrid[j, i].Value == null ? "" : dataGrid[j, i].Value;
                        }
                        catch
                        {
                            ;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
        public void dtgvExport2Excel(DataGridView dataGrid, string pathSaveExcel)
        {
            try
            {

          
            Excel.Application xlapp;
            Excel.Workbook xlworkbook;
            Excel.Worksheet xlworsheet;
            object misValue = System.Reflection.Missing.Value;

            xlapp = new Excel.Application();
            xlworkbook = xlapp.Workbooks.Add(misValue);
            xlworsheet = (Excel.Worksheet)xlworkbook.Worksheets.get_Item(1);
            // int i = 0;
            // int j = 0;


            for (int k = 0; k <= dataGrid.ColumnCount - 1; k++)
            {
                string cell = dataGrid.Columns[k].HeaderText;
                xlworsheet.Cells[1, k+ 1] = cell;
            }



            for (int i = 0; i <= dataGrid.RowCount-1 ; i++)
            {
                for (int j = 0; j <= dataGrid.ColumnCount - 1; j++)
                {
                    DataGridViewCell cell = dataGrid[j, i];
                    xlworsheet.Cells[i + 2, j + 1] = cell.Value;
                }
            }
            xlworkbook.SaveAs(pathSaveExcel, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            xlworkbook.Close(true, misValue, misValue);
            xlapp.Quit();
            reOject(xlworsheet);
            reOject(xlworkbook);
            reOject(xlapp);
            MessageBox.Show("Export to excel sucessful !" , "Information",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {

                MessageBox.Show("Export to excel fail: " + ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void reOject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Export to excel fail: " + ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                GC.Collect();
            }
        }
    }
}
