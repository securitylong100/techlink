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
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
          
        }

        private void ERPShowOrder_Load(object sender, EventArgs e)
        {
            string sql = "select distinct TA002 from ACPTA order by TA002 ";
            sqlERPCON conERP = new sqlERPCON();
            conERP.getComboBoxData(sql, ref cmb_test);
        }
    }
}
