using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace WindowsFormsApplication1
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
           //Application.Run(new LoginFr());
            //  Application.Run(new ERPShowOrder.ERPShowMain());
            // Application.Run(new ERPShowOrder.ERPShowShipping());
            // Application.Run(new ERPShowOrder.ERPMaterialShow());
            //  Application.Run(new ERPShowOrder.ERPShowOrder());
            // Application.Run(new ShippingReport());
            //    Application.Run(new CrisisReport.ProductionMonitoring());

            //  Application.Run(new ERPShowOrder.ERP_KPI_Report());
           Application.Run(new MQC.MQCShowForm());
        }
    }
}
