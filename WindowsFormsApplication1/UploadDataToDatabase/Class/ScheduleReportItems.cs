using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UploadDataToDatabase.Class
{
  public  class ScheduleReportItems
    {
        private static readonly ScheduleReportItems storage = new ScheduleReportItems();
        public static ScheduleReportItems GetStorage()
        {
            return storage;
        }
        public string ReportName { get; set; }
        public string ReportType { get; set; }
        public string Hours { get; set; }
        public string Day { get; set; }
        public string Date { get; set; }
        public string Month { get; set; }
    }
}
