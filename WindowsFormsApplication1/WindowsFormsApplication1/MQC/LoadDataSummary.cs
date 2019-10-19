using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace WindowsFormsApplication1.MQC
{
    class LoadDataSummary
    {
        public List<MQCItemSummary> GetMQCItemSummaries(DateTime from, DateTime to, string site, string process)
        {
            List<MQCItemSummary> qCItemSummaries = new List<MQCItemSummary>();
     
            try
            {
                LoadDataMQC dataMQC = new LoadDataMQC();
                List<MQCDataItems> mQCDataItems = dataMQC.listMQCDataItemsbySite(from, to, site, process);
                //Nhom theo san pham
                var ListMQCbyProduct = mQCDataItems
                     .GroupBy(u => u.model)
                     .Select(grp => grp.ToList())
                    .ToList();
                foreach (var qCDataItems in ListMQCbyProduct)
                {
                    MQCItemSummary itemSummary = new MQCItemSummary();
                    itemSummary.product = qCDataItems[0].model;             
                    itemSummary.defectItems = new List<DefectItem>();
                       var ListItemsData = qCDataItems
                    .GroupBy(u => u.item)
                    .Select(grp => grp.ToList())
                   .ToList();

                    var ListItemsTime = qCDataItems
                .GroupBy(u => u.inspecttime)
               .ToList();
                    //Khi thay doi ngay can phai chinh lai thoi gian
                    itemSummary.Time_from = ListItemsTime.Min(d => d.Key).ToString();
                    itemSummary.Time_To = ListItemsTime.Max(d => d.Key).ToString();

                    foreach (var itemData in ListItemsData)
                    {
                        DefectItem item = new DefectItem();
                        item.DefectCode = itemData[0].item;
                        item.Quantity = itemData.Select(d => d.data).Sum();
                   
                       if(itemData[0].remark =="OP")
                        {
                            itemSummary.OutputQty += item.Quantity;
                        }
                       else if (itemData[0].remark == "NG")
                        {
                            LoadDefectMapping defectMapping = new LoadDefectMapping();
                            NGItemsMapping nGItemsMapping = defectMapping.GetNGMapping(site, process, item.DefectCode);
                            item.DefectSFT = nGItemsMapping.NGCode_SFT;
                            item.DefectSFTName = nGItemsMapping.NGCodeName_SFT;
                            itemSummary.defectItems.Add(item);
                            itemSummary.NGQty+= item.Quantity;
                        }
                    }
                    itemSummary.QuantityTotal = itemSummary.OutputQty + itemSummary.NGQty;
                    itemSummary.DefectRate = (itemSummary.QuantityTotal != 0) ? (itemSummary.NGQty / itemSummary.QuantityTotal) : 0;
                    qCItemSummaries.Add(itemSummary);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return qCItemSummaries;
        }
    }
}
