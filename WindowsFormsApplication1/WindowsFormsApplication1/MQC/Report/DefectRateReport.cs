using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace WindowsFormsApplication1.MQC.Report
{
  public  class DefectRateReport
    {

    public DefectRateData GetDefectRateReport(DateTime from, DateTime to, string Dept, string codeProcess)
        {
            DefectRateData defectRate = new DefectRateData();
            try
            {

                //code lay tren ERP va SFT
           //     StringBuilder sql = new StringBuilder();
           //     sql.Append("select sum(TA011) as outputQty, sum(TA012) as DefectQTy, sum(TA011)+ sum(TA012) as TotalQty ");
           //     sql.Append("from SFCTA ");
           //     sql.Append("where 1=1 ");
           //     sql.Append("and TA004 = '" + Dept + "'");
           //     sql.Append("and TA003 = '" + codeProcess + "'");
           //     sql.Append("and CREATE_DATE >= '" + from.ToString("yyyyMMdd") + "'");
           //     sql.Append("and CREATE_DATE <= '" + to.ToString("yyyyMMdd") + "'");
           //     sqlERPCON sqlERPCON = new sqlERPCON();
           //     DataTable dt = new DataTable();
           //     sqlERPCON.sqlDataAdapterFillDatatable(sql.ToString(), ref dt);
               
           //var     defectItems = (from DataRow dr in dt.Rows
           //                        select new DefectRateData()
           //                        {
           //                           TotalQuantity  =double.Parse( dr["TotalQty"].ToString()),
           //                            DefectQuantity = double.Parse( dr["DefectQTy"].ToString()),
           //                            OutputQuantity = double.Parse(dr["outputQty"].ToString())

           //                        }).ToList();

           //     defectRate = defectItems[0];
           //     defectRate.DateTime_from = from;
           //     defectRate.DateTime_to = to;
           //     defectRate.DefectRate = (defectRate.TotalQuantity != 0) ? (defectRate.DefectQuantity / defectRate.TotalQuantity) : 0;

               
                LoadDataSummary loadData = new LoadDataSummary();
        MQCItemSummary mQCItems =  loadData.GetMQCItemSummary(from, to, Dept, "MQC");
                defectRate.TotalQuantity = mQCItems.QuantityTotal;
                defectRate.DefectQuantity = mQCItems.NGQty;
                defectRate.OutputQuantity = mQCItems.OutputQty;
                defectRate.DefectRate = (defectRate.TotalQuantity != 0) ? (defectRate.DefectQuantity / defectRate.TotalQuantity) : 0;
                LoadDefectMapping loadDefectTop5 = new LoadDefectMapping();
       List<NGItemsMapping> listTop5 =  loadDefectTop5.listNGMappingGetReport(Dept, "MQC");
                List<DefectItem> listDefectTop5 = new List<DefectItem>();
                for (int i = 0; i < listTop5.Count; i++)
                {
                    var getlist = mQCItems.defectItems.Where(d => d.DefectSFT == listTop5[i].NGCode_SFT).ToList();
                    
                    DefectItem defect = new DefectItem();
                    if (getlist != null && getlist.Count > 0)
                    {
                        defect = getlist[0];
                        defect.Quantity = getlist.Select(s => s.Quantity).Sum();

                     }
                    defect.Note = listTop5[i].Note ;
                    listDefectTop5.Add(defect);
                }
                var listDefectTop5Groupby = listDefectTop5.OrderBy(d => d.Note).ToList();
                defectRate.defectItems = listDefectTop5Groupby;

            }
            catch (Exception ex)
            {
                Log.Logfile.Output(Log.StatusLog.Error, "GetDefectRateReport(DateTime from, DateTime to, string Dept, string codeProcess)", ex.Message);


            }
            return defectRate;
        }

        public DefectRateData GetDefectRateReportByLot(DateTime from, DateTime to, string Dept, string codeProcess,string lot)
        {
            DefectRateData defectRate = new DefectRateData();
            try
            {

                //code lay tren ERP va SFT
                //     StringBuilder sql = new StringBuilder();
                //     sql.Append("select sum(TA011) as outputQty, sum(TA012) as DefectQTy, sum(TA011)+ sum(TA012) as TotalQty ");
                //     sql.Append("from SFCTA ");
                //     sql.Append("where 1=1 ");
                //     sql.Append("and TA004 = '" + Dept + "'");
                //     sql.Append("and TA003 = '" + codeProcess + "'");
                //     sql.Append("and CREATE_DATE >= '" + from.ToString("yyyyMMdd") + "'");
                //     sql.Append("and CREATE_DATE <= '" + to.ToString("yyyyMMdd") + "'");
                //     sqlERPCON sqlERPCON = new sqlERPCON();
                //     DataTable dt = new DataTable();
                //     sqlERPCON.sqlDataAdapterFillDatatable(sql.ToString(), ref dt);

                //var     defectItems = (from DataRow dr in dt.Rows
                //                        select new DefectRateData()
                //                        {
                //                           TotalQuantity  =double.Parse( dr["TotalQty"].ToString()),
                //                            DefectQuantity = double.Parse( dr["DefectQTy"].ToString()),
                //                            OutputQuantity = double.Parse(dr["outputQty"].ToString())

                //                        }).ToList();

                //     defectRate = defectItems[0];
                //     defectRate.DateTime_from = from;
                //     defectRate.DateTime_to = to;
                //     defectRate.DefectRate = (defectRate.TotalQuantity != 0) ? (defectRate.DefectQuantity / defectRate.TotalQuantity) : 0;


                LoadDataSummary loadData = new LoadDataSummary();
                MQCItemSummary mQCItems = loadData.GetMQCItemSummarybyLot(from, to, Dept, "MQC",lot);
                defectRate.Product = mQCItems.product;
                defectRate.Lot = lot;
                defectRate.TotalQuantity = mQCItems.QuantityTotal;
                defectRate.DefectQuantity = mQCItems.NGQty;
                defectRate.OutputQuantity = mQCItems.OutputQty;
                defectRate.DateTime_from = mQCItems.Time_from;
                defectRate.DateTime_to = mQCItems.Time_To;
                defectRate.DefectRate = (defectRate.TotalQuantity != 0) ? (defectRate.DefectQuantity / defectRate.TotalQuantity) : 0;
                LoadDefectMapping loadDefectTop16 = new LoadDefectMapping();
                List<NGItemsMapping> listTop16 = loadDefectTop16.listNGMappingGetReportTop16(Dept, "MQC");
                List<DefectItem> listDefectTop16 = new List<DefectItem>();
                for (int i = 0; i < listTop16.Count; i++)
                {
                    var getlist = mQCItems.defectItems.Where(d => d.DefectSFT == listTop16[i].NGCode_SFT).ToList();

                    DefectItem defect = new DefectItem();
                    if (getlist != null && getlist.Count > 0)
                    {
                        defect = getlist[0];
                        defect.Quantity = getlist.Select(s => s.Quantity).Sum();

                    }
                    defect.Note = listTop16[i].Note;
                    listDefectTop16.Add(defect);
                }
                var listDefectTop16Groupby = listDefectTop16.OrderBy(d => d.Note).ToList();
                defectRate.defectItems = listDefectTop16Groupby;

            }
            catch (Exception ex)
            {
                Log.Logfile.Output(Log.StatusLog.Error, "GetDefectRateReport(DateTime from, DateTime to, string Dept, string codeProcess)", ex.Message);


            }
            return defectRate;
        }

    }
   
}
