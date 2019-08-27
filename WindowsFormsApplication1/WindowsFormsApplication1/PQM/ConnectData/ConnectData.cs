using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1.PQM.ConnectData
{
    public partial class ConnectData : CommonForm
    {
        public ConnectData()
        {
            InitializeComponent();
        }

        private void ConnectData_Load(object sender, EventArgs e)
        {

            sqlCON connect = new sqlCON();
            string sql = "select distinct model from m_pqmdata order by model";
            connect.getComboBoxData(sql, ref cmb_modelcode);

        }

        void Inspect_Tree(string model)
        {
            if (model != "")
            {
                DataTable dt = new DataTable();
                model = cmb_modelcode.Text;
                sqlCON connect = new sqlCON();

                connect.sqlDataAdapterFillDatatable("select distinct item from m_pqmdata where model = '" + model + "' and  process = '" + cmb_processcode.Text + "'", ref dt);
                //dgvData.DataSource = dt;
                string str = "";
                TreeNode root = new TreeNode() { Text = model };
                tv_model.Nodes.Add(root);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    str = dt.Rows[i]["item"].ToString();
                    TreeNode nodes = new TreeNode() { Text = str };
                    root.Nodes.Add(nodes);
                }
            }
        }


        private void cmb_modelcode_SelectedIndexChanged(object sender, EventArgs e)
        {
            sqlCON connect = new sqlCON();
            string sql = "select distinct process from m_pqmdata where model = '" + cmb_modelcode.Text + "' order by process";
            connect.getComboBoxData(sql, ref cmb_processcode);
        }

        private void cmb_processcode_SelectedIndexChanged(object sender, EventArgs e)
        {
            tv_model.Nodes.Clear();
            Inspect_Tree(cmb_modelcode.Text);
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            listserno();
            showdata();
        }
        void listserno()
        {
            sqlCON connect = new sqlCON();
            StringBuilder sql = new StringBuilder();
            sql.Append("select distinct serno from m_pqmdata where model = '" + cmb_modelcode.Text + "' and process = '" + cmb_processcode.Text + "' and ");
            sql.Append(" cast(inspectdate as datetime) + CAST (inspecttime as datetime) >= '" + dtp_to.Value + "' and  cast(inspectdate as datetime) + CAST (inspecttime as datetime) <'" + dtp_from.Value + "' order by serno");
            connect.getComboBoxData(sql.ToString(), ref comboBox1);
        }
        List<string> list = new List<string>();
        private List<string> Check_list(TreeNodeCollection root)
        {
            foreach (TreeNode node in root)
            {
                if (node.Checked == true)
                {
                    list.Add(node.Text);
                }
                Check_list(node.Nodes);
            }
            if (root.ToString() == cmb_processcode.Text)
            {
                list.Remove(cmb_processcode.Text);
            }

            return list.OrderBy(x => x).ToList();
        }
        List<string> list2 = new List<string>();
        void showdata()
        {
            list2 = Check_list(tv_model.Nodes);
            string inspect = "''";
            string serno = "";

            //   string[] sernoList = comboBox1.DataSource.ToString() ;
            // Array.Sort(sernoList);

            for (int i = 0; i < comboBox1.Items.Count; i++)
            {
                if (i > 0) { serno += ","; }
                serno += "'" + comboBox1.Items[i].ToString() + "'";
            }
            for (int i = 0; i < list2.Count(); i++)
            {
                if (i == 0) { inspect = "'" + list2[i].ToString() + "'"; }
                if (i > 0)
                {
                    inspect += ",'" + list2[i].ToString() + "'";
                }
            }

            DataTable DataSerno = new DataTable();
            sqlCON connect = new sqlCON();
            connect.sqlDataAdapterFillDatatable("select * from m_pqmdata where serno in (" + serno + ") and item in (" + inspect + ") order by inspectdate, serno, item", ref DataSerno);

            var distinctInspcec = DataSerno.DefaultView.ToTable(true, "item");
            var distinctSerno = DataSerno.DefaultView.ToTable(true, "serno", "inspectdate");

            int countInspect = distinctInspcec.Rows.Count;
            DataTable DtData = new DataTable();

            for (int i = 0; i < DataSerno.Columns.Count - 2; i++)
            {
                DtData.Columns.Add(DataSerno.Columns[i].ColumnName);
            }
            foreach (DataRow row in distinctInspcec.Rows)
            {
                DtData.Columns.Add(row[0].ToString());
            }
            DataRow dr;
            int k = 0;
            for (int i = 0; i < distinctSerno.Rows.Count; i++)
            {
                string se_rno = distinctSerno.Rows[i]["serno"].ToString();
                string inspectDate = distinctSerno.Rows[i]["inspectdate"].ToString();

                dr = DtData.NewRow();
                for (int j = 0; j < DataSerno.Columns.Count - 2; j++)
                {
                    if (se_rno == DataSerno.Rows[k]["serno"].ToString())
                    {
                        dr[j] = DataSerno.Rows[k][j].ToString();
                    }
                }
                int t = 0;
                for (int j = 0; j < DataSerno.Rows.Count; j++)
                {
                    if (se_rno == DataSerno.Rows[j]["serno"].ToString() && inspectDate == DataSerno.Rows[j]["inspectdate"].ToString())
                    {
                        if (DtData.Columns[t + 8].ColumnName == DataSerno.Rows[j]["item"].ToString())
                        {
                            dr[t + 8] = DataSerno.Rows[j]["data"].ToString();
                            t++;
                        }
                    }
                }

                k = k + countInspect;
                DtData.Rows.Add(dr);
            }
            dataGridView1.DataSource = DtData;
            list = new List<string>();
            list2 = new List<string>();

        }
    }
}
