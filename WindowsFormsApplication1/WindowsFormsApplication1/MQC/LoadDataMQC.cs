using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace WindowsFormsApplication1.MQC
{
    class LoadDataMQC
    {
       public MQCItem1 GetQCCItemOK(DateTime from, DateTime to, string model, string lot, string site, string process)
        {
            LoadDefectMapping defectMapping = new LoadDefectMapping();
            List<NGItemsMapping> nGItemsMappings = defectMapping.listNGMapping("B01");
            MQCItem1 mQCItem = new MQCItem1();
            List<MQCDataItems> listMQC = new List<MQCDataItems>();
            listMQC= listMQCDataItems(from, to, model, lot, site, process);
            //Load MQCItem to show
            mQCItem.process = process;
            mQCItem.department = site;
            mQCItem.product = model;
            mQCItem.PO = lot;
            var TotalOutputQty = listMQC
                .Where(d => d.remark == "OP")
                .Select((s=> s.data))
                .ToList();
            mQCItem.TotalOutput = TotalOutputQty.Sum();
            var TotalNGQty = listMQC
                 .Where(d => d.remark == "NG")
                 .Select((s => s.data))
                 .ToList();
            mQCItem.TotalNG = TotalNGQty.Sum();
            var TotalRework = listMQC
               .Where(d => d.remark == "RW")
               .Select((s => s.data))
               .ToList();
            mQCItem.TotalRework= TotalRework.Sum();
            mQCItem.percentNG = (mQCItem.TotalOutput + mQCItem.TotalNG + mQCItem.TotalRework)!= 0 ? mQCItem.TotalNG / (mQCItem.TotalOutput + mQCItem.TotalNG + mQCItem.TotalRework) :0;
            mQCItem.percentRework = (mQCItem.TotalOutput + mQCItem.TotalNG + mQCItem.TotalRework) != 0 ? mQCItem.TotalRework / (mQCItem.TotalOutput + mQCItem.TotalNG + mQCItem.TotalRework) : 0;
            var listNGItem = listMQC
             .Where(d => d.remark== "NG")
             .Select((s => new {s.item,s.data}))
             .ToList();
            mQCItem.listNGItems = new List<NGItems>();
            foreach (var item in listNGItem)
            {
                NGItems nGItems = new NGItems();
                var _NG_SFT = nGItemsMappings.Where(d => d.NGCode_Process == item.item).Select(s => s.NGCode_SFT).ToArray();
                string NG_SFT = (_NG_SFT != null && _NG_SFT.Count() > 0) ?_NG_SFT[0] : "";
                nGItems.NGType = NG_SFT;
                var _NGName_SFT = nGItemsMappings.Where(d => d.NGCode_Process == item.item).Select(s => s.NGCodeName_SFT).ToArray();
                string NGName_SFT = (_NGName_SFT != null && _NGName_SFT.Count() > 0) ? _NGName_SFT[0] : "";
                nGItems.NGName = NGName_SFT;

                nGItems.NGKey = item.item;
                nGItems.NGQuantity = item.data;
                mQCItem.listNGItems.Add(nGItems);
            }
            var listRWItem = listMQC
             .Where(d => d.item.Contains("RW"))
             .Select((s => new { s.item, s.data }))
             .ToList();
            mQCItem.listRWItems = new List<NGItems>();
            foreach (var item in listRWItem)
            {
                NGItems nGItems = new NGItems();
                nGItems.NGKey = item.item;
                nGItems.NGQuantity = item.data;
                mQCItem.listRWItems.Add(nGItems);
            }
            List<MQCDataItems> listMQC_Error = new List<MQCDataItems>();
            listMQC_Error = listMQCData_ErrorItems(from, to, model, lot, site, process);
            mQCItem.InputMaterialNotYet = listMQC_Error.Count;
            mQCItem.InputSFT = mQCItem.TotalOutput - mQCItem.InputMaterialNotYet;
            // sql.Append()
            return mQCItem;
            
        }
        public List<MQCDataItems> listMQCDataItems(DateTime from, DateTime to, string model,string lot, string site, string process)
        {
            List<MQCDataItems> listMQCDataItems = new List<MQCDataItems>();
            StringBuilder sql = new StringBuilder();
            sql.Append("select distinct serno, lot, model, site, factory, line, process, item, inspectdate, inspecttime, data, judge, status, remark ");
            sql.Append("from m_ERPMQC ");
            sql.Append("where 1=1 ");
            //sql.Append("and model = '" + model + "' " );
            //sql.Append("and lot like '%" + lot+ "%' ");
            //sql.Append("and site = '" + site + "' ");
            //sql.Append("and process = '" + process + "' ");
            //sql.Append("and inspectdate > '" + from + "' ");
            //sql.Append("and inspecttime > '" + to.TimeOfDay + "' ");
            sqlCON sql12 = new sqlCON();
            DataTable dt = new DataTable();
            sql12.sqlDataAdapterFillDatatable(sql.ToString(), ref dt);
            listMQCDataItems = (from DataRow dr in dt.Rows
                           select new MQCDataItems()
                           {
                               serno = dr["serno"].ToString(),
                               lot = dr["lot"].ToString(),
                               model = dr["model"].ToString(),
                               site = dr["site"].ToString(),
                               factory = dr["factory"].ToString(),
                               line = dr["line"].ToString(),
                               process = dr["process"].ToString(),
                               item = dr["item"].ToString(),
                               inspectdate = DateTime.Parse (dr["inspectdate"].ToString()),
                               inspecttime = TimeSpan.Parse(dr["inspecttime"].ToString()),
                               data =double.Parse( dr["data"].ToString()),
                               judge = dr["judge"].ToString(),
                               status = dr["status"].ToString(),
                               remark = dr["remark"].ToString()

                           }).ToList();

            return listMQCDataItems;
        }
        public List<MQCDataItems> listMQCData_ErrorItems(DateTime from, DateTime to, string model, string lot, string site, string process)
        {
            List<MQCDataItems> listMQCDataItems = new List<MQCDataItems>();
            StringBuilder sql = new StringBuilder();
            sql.Append("select distinct serno, lot, model, site, factory, line, process, item, inspectdate, inspecttime, data, judge, status, remark ");
            sql.Append("from m_ERPMQC_Error ");
            sql.Append("where 1=1 ");
            //sql.Append("and model = '" + model + "' ");
            //sql.Append("and lot like '%" + lot + "%' ");
            //sql.Append("and site = '" + site + "' ");
            //sql.Append("and process = '" + process + "' ");
            //sql.Append("and inspectdate > '" + from + "' ");
            //sql.Append("and inspecttime > '" + to.TimeOfDay + "' ");
            sqlCON sql12 = new sqlCON();
            DataTable dt = new DataTable();
            sql12.sqlDataAdapterFillDatatable(sql.ToString(), ref dt);
            listMQCDataItems = (from DataRow dr in dt.Rows
                                select new MQCDataItems()
                                {
                                    serno = dr["serno"].ToString(),
                                    lot = dr["lot"].ToString(),
                                    model = dr["model"].ToString(),
                                    site = dr["site"].ToString(),
                                    factory = dr["factory"].ToString(),
                                    line = dr["line"].ToString(),
                                    process = dr["process"].ToString(),
                                    item = dr["item"].ToString(),
                                    inspectdate = DateTime.Parse(dr["inspectdate"].ToString()),
                                    inspecttime = TimeSpan.Parse(dr["inspecttime"].ToString()),
                                    data = double.Parse(dr["data"].ToString()),
                                    judge = dr["judge"].ToString(),
                                    status = dr["status"].ToString(),
                                    remark = dr["remark"].ToString()

                                }).ToList();

            return listMQCDataItems;
        }
        


    }
}
