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
            string sql = "select distinct modelcode from m_model order by modelcode";
            connect.getComboBoxData(sql, ref cmb_modelcode);
            treeview();
        }
        void treeview()
        {

        }
    }
}
