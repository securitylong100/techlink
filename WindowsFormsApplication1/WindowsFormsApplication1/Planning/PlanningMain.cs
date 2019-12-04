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
    public partial class PlanningMain : CommonForm
    {
        DateTime from = DateTime.MinValue;
        DateTime to = DateTime.MinValue;
        List<OrderVariable> orderVariables;
        public PlanningMain()
        {
            InitializeComponent();
            dtp_from.Value = DateTime.Now.AddDays(1);
            dtp_to.Value = DateTime.Now.AddDays(21);
            from = dtp_from.Value;
            to = dtp_to.Value;
       //     CreateDatagridview(ref dtgv_header);
        }
        public void CreateDatagridview(ref DataGridView gridView)

        {
            gridView.ColumnCount = 19;

            gridView.Columns[0].Name = "Product";
            gridView.Columns[1].Name = "Client";
            gridView.Columns[2].Name = "HENN";
            gridView.Columns[3].Name = "QtyinBox";
            gridView.Columns[4].Name = "ToolQty";
            gridView.Columns[5].Name = "MO";
            gridView.Columns[6].Name = "WorkerQty";
            gridView.Columns[7].Name = "WorkerTarget";
            gridView.Columns[8].Name = "ShiftA";
            gridView.Columns[9].Name = "ShiftB";
            gridView.Columns[10].Name = "QtyYes";
            gridView.Columns[10].HeaderText = "Quantity's yesterday";
            gridView.Columns[11].Name = "TotalShipment";
            gridView.Columns[12].Name = "Stock";
            gridView.Columns[13].Name = "Remain";
            gridView.Columns[14].Name = "NotHENN";
            gridView.Columns[15].Name = "MQCQty";
            gridView.Columns[16].Name = "PQC";
            gridView.Columns[17].Name = "SemiGoods";
            gridView.Columns[18].Name = "FieldStock";
            

            //   gridView.Columns[0].MinimumWidth = 50;

        }

        private void PlanningMain_Load(object sender, EventArgs e)
        {
            LoadDataPlanning loadData = new LoadDataPlanning();
            orderVariables = new List<OrderVariable>();
            orderVariables = loadData.LoadOrderInformationbyDate(from, to, "");
            List<DataHeader> dataHeaders = new List<DataHeader>();
            Dictionary<string, PlanningItem> keyValuePairs = loadData.GetPlanningReport(orderVariables);
            foreach (var item in keyValuePairs)
            {
                DataHeader data = new DataHeader();
                data.products = item.Value.KeyProduct;
                data.clients = item.Value.Client;
                data.HENN = item.Value._bom.HEN;
                data.QtyInBox = item.Value._bom.QtyUnit;
                data.ToolPcs = item.Value._bom.ToolQty;
                data.StockWH = item.Value.wip.Warehouse;
                data.WIPQty = item.Value.wip.TotalInWip;
                data.MQCStock = item.Value.wip.MQCQty;
                data.PQCStock = item.Value.wip.PQCQty;
                data.IntoWH = item.Value.wip.StockInWHQTy;
                data.SemiStock = item.Value.wip.SemiFinished;
                dataHeaders.Add(data);
            }
            dtgv_header.DataSource = dataHeaders;
        }
    }
}
