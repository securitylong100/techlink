using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1.CrisisReport.Classvalue
{
    class ShippingItems
    {
        public DateTime CreateTime { get; set; }
        public string OrderCode { get; set; }
        public string Clients { get; set; }
        public string Clients_OrderCode { get; set; }
        public DateTime ClientsRequestDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public double ShippingPercents { get; set; }
        public string Status { get; set; }  

    }
    class StatusSumary
    {
        public string Status { get; set; }
        public int Quantity { get; set; }

    }
}
