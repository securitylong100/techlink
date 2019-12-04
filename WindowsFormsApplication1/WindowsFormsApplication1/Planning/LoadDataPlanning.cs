using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using WindowsFormsApplication1.Class;
namespace WindowsFormsApplication1.Planning
{
    class LoadDataPlanning
    {
        public List<OrderVariable> LoadOrderInformationbyDate(DateTime from, DateTime to, string Dept)
        {
            List<OrderVariable> orderVariables = new List<OrderVariable>();
            try
            {
                DataTable dt = new DataTable();
                sqlERPCON sqlERPCON = new sqlERPCON();
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append(@"select smes.ME002 as Dept,TD001 + '-' + TD002 as DDH, TD004 as product ,TD005 as productName,
sum(TD008) as QuantityOfOrder, sum(TD009) as QuantityOfDelivery, TD013 as ClientRequestDate from COPTD ptd
inner join COPTC ptc on ptc.TC001 = ptd.TD001 and ptc.TC002 = ptd.TD002
left join CMSME smes on smes.ME001 = ptc.TC005
where  ptc.TC027 ='Y' and TD001 like '%B%' and ( TD004 like '%BMH%' or  TD004 like '%BWTX%')
");
                stringBuilder.Append(" and CONVERT(date,ptd.TD013)  >= '" + from.ToString("yyyyMMdd") + "' ");
                stringBuilder.Append(" and CONVERT(date,ptd.TD013) <= '" + to.ToString("yyyyMMdd") + "' ");
                stringBuilder.Append(@" group by TD001,TD002,TD005, TD013,smes.ME002,TD004
order by TD013 ");

                sqlERPCON.sqlDataAdapterFillDatatable(stringBuilder.ToString(), ref dt);
                orderVariables = (from DataRow dr in dt.Rows
                                  select new OrderVariable()
                                  {
                                      Dept = (dr["Dept"] != null) ? dr["Dept"].ToString().Trim() : "",
                                      DDH = (dr["DDH"] != null) ? dr["DDH"].ToString().Trim() : "",
                                      Product = (dr["product"] != null) ? dr["product"].ToString().Trim() : "",
                                      ProductName = (dr["productName"] != null) ? dr["productName"].ToString().Trim() : "",
                                      ClientOrderQty = (dr["QuantityOfOrder"] != null && dr["QuantityOfOrder"].ToString() != "") ? (double.Parse(dr["QuantityOfOrder"].ToString().Trim())) : 0,
                                      DeliveryQty = (dr["QuantityOfDelivery"] != null && dr["QuantityOfDelivery"].ToString() != "") ? (double.Parse(dr["QuantityOfDelivery"].ToString())) : 0,
                                      ClientRequestDate = (dr["ClientRequestDate"] != null && dr["ClientRequestDate"].ToString() != "") ? DateTime.Parse(dr["ClientRequestDate"].ToString().Trim().Insert(4, "-").Insert(7, "-")) : DateTime.MinValue
                                  }).ToList();
                
            }
            catch (Exception)
            {

                return null;
            }
            return orderVariables;
        }
        public Dictionary<string, PlanningItem> GetPlanningReport (List<OrderVariable> orders )
        {
            Dictionary<string, PlanningItem> keyValuePairs = new Dictionary<string, PlanningItem>();
            List<List<OrderVariable>> listOders = new List<List<OrderVariable>>();
            listOders = ListOrdervariables(orders);
            List<SettingBOM> listSettingBoms = LoadingSettingBOMs();
            List<SettingManufacture> lisSettingManufactures = LoadingSettingManufacture();
            SFT_WIP sFT_WIP = new SFT_WIP();
            
            try
            {

                foreach (var order in listOders)
                {
                    PlanningItem pLanning = new PlanningItem();
                    pLanning.KeyProduct = order[0].ProductName;
                    // base on product to defind Dept
                    pLanning.Client = "";
                    // Define Shiment Plan of production
                    pLanning.shipmentPlans = new List<ShipmentPlan>();
                    pLanning.needProduceQties = new List<NeedProduceQty>();
                    foreach (var or in order)
                    {
                        ShipmentPlan shipment = new ShipmentPlan();
                        shipment.ClientRequestDate = or.ClientRequestDate;
                        shipment.DeliveryPlanQty = or.ClientOrderQty;
                        pLanning.shipmentPlans.Add(shipment);

                        NeedProduceQty need = new NeedProduceQty();
                        need.ClientRequestDate = or.ClientRequestDate;
                        need.NeedQty = or.ClientOrderQty;
                        need.RemainDay =( or.ClientRequestDate - DateTime.Now.Date).Days;
                        need.NeedQtyPerDay = need.NeedQty / need.RemainDay;
                        pLanning.needProduceQties.Add(need);
                    }
                    pLanning.TotalQty = pLanning.shipmentPlans.Select(d => d.DeliveryPlanQty).Sum();
                    pLanning._bom = new BOM();
                    List<string> ListHENN = ListHENNofProduct(pLanning.KeyProduct);
                   if(ListHENN != null)
                    {
                       
                        pLanning._bom.HEN = ListHENN[0];

                    }
                   else
                    {
                        pLanning._bom.HEN = "";
                    }
                    var _bom = listSettingBoms.Where(d => d.ProductNo == pLanning.KeyProduct).ToList();
                    if (_bom.Count > 0)
                    {
                        pLanning._bom.QtyUnit = _bom[0].QtyInBox;
                        pLanning._bom.ToolQty = _bom[0].QtyTool;
                    }
                    pLanning.wip = new Wip();
                    pLanning.wip.Warehouse = StockOfProduct(order[0].Product);
                    pLanning.TotalShortage = pLanning.TotalQty - pLanning.wip.Warehouse;

                    pLanning.production = new Production();
                    var _productItem = lisSettingManufactures.Where(d => d.ProductNo == pLanning.KeyProduct).ToList();
                    if(_productItem.Count > 0)
                    {
                        pLanning.production.PeopleQty = _productItem[0].WorkerQty;
                        pLanning.production.targetPeople = _productItem[0].Workertarget;
                        pLanning.production.ProductionQty = pLanning.production.PeopleQty * pLanning.production.targetPeople;
                    }
                    sFT_WIP = GetSFT_WIPofProducts(order[0].Product);

                    pLanning.wip.MQCQty = sFT_WIP.MQC_Out_Available;
                    pLanning.wip.PQCQty = sFT_WIP.PQC_In_Available + sFT_WIP.PQC_Out_Available;
                    pLanning.wip.StockInWHQTy = sFT_WIP.StockIntoWH;
                    pLanning.wip.TotalInWip = pLanning.wip.MQCQty + pLanning.wip.PQCQty + pLanning.wip.StockInWHQTy + pLanning.wip.Warehouse;
                    keyValuePairs.Add(pLanning.KeyProduct, pLanning);
                }
            }
            catch (Exception)
            {

                return null;
            }
            return keyValuePairs;
        }
        public List<List<OrderVariable>> ListOrdervariables (List<OrderVariable> orders)
        {
            List<List<OrderVariable>> orderVariables = new List<List<OrderVariable>>();
            try
            { if (orders != null)
                {
                     orderVariables = orders
                    .GroupBy(u => u.ProductName)
            .Select(grp => grp.ToList())
            .ToList();
                }
            }
            catch (Exception)
            {

                return null;
            }
            return orderVariables;
        }
        public List<string> ListHENNofProduct(string ProductNO)
        {
          

            try
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append(@" select distinct TE017 from MOCTA 
 left join MOCTE on TA001 = TE011 and TA002 = TE012
 where  (TE018 like '%HENN%' ) and TE019 ='Y' 
");
                stringBuilder.Append(" and TA034   = '"+ ProductNO+"' ");
                sqlERPCON sqlERPCON = new sqlERPCON();
                DataTable dt = new DataTable();
                sqlERPCON.sqlDataAdapterFillDatatable(stringBuilder.ToString(), ref dt);
                if (dt != null && dt.Rows.Count > 0)
                    return dt.AsEnumerable()
                            .Select(r => r.Field<string>("TE017"))
                            .ToList();
                else return null;

            }
            catch (Exception ex)
            {

                return null;
            }
          
        }
public double StockOfProduct (string ProducNo)
        {
            try
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append(@" select MB064 from INVMB where 1=1 
");
                stringBuilder.Append(" and MB001   = '" + ProducNo + "' ");
                sqlERPCON sqlERPCON = new sqlERPCON();
                string data = sqlERPCON.sqlExecuteScalarString(stringBuilder.ToString());
                if (data != string.Empty)
                {
                    try
                    {
                        return double.Parse(data);
                    }
                    catch (Exception)
                    {

                        return 0;
                    }
                }
            }
            catch (Exception)
            {

                return 0;
            }
            return 0;
        }

        public List<SettingBOM> GetSettingBOMs()
        {
            List<SettingBOM> settingBOMs = new List<SettingBOM>();
            try
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append(@" select distinct ProductName, productNo 
from (
select smes.ME002 as Dept,TD001, TD002, TD004 as ProductName,TD005 as productNo,
sum(TD008) as ClientRequestQty,sum(TD009) as DeliveryQty, TD013 as ClientRequestDate from COPTD ptd
inner join COPTC ptc on ptc.TC001 = ptd.TD001 and ptc.TC002 = ptd.TD002
left join CMSME smes on smes.ME001 = ptc.TC005
where  ptc.TC027 ='Y' and TD001 like '%B%' and ( TD004 like '%BMH%' or  TD004 like '%BWTX%')
group by TD001,TD002,TD005, TD013,smes.ME002,TD004

) DDH  
");
              
                sqlERPCON sqlERPCON = new sqlERPCON();
                DataTable dt = new DataTable();
                sqlERPCON.sqlDataAdapterFillDatatable(stringBuilder.ToString(), ref dt);
                settingBOMs = (from DataRow dr in dt.Rows
                                  select new SettingBOM()
                                  {
                                      ProductName = (dr["ProductName"] != null) ? dr["ProductName"].ToString().Trim() : "",
                                      ProductNo = (dr["productNo"] != null) ? dr["productNo"].ToString().Trim() : "",
                                    
                                  }).ToList();
            }
            catch (Exception)
            {

                return null;
            }
            return settingBOMs;
        }
        public bool InsertToBOMSettingTableIntilizer(List<SettingBOM> settingBOMs)
        {
            
            foreach (var item in settingBOMs)
            {
                string sqlQuerry = "";
                sqlQuerry += "insert into t_settingBOM (productName, productNo, QTyinBox, QtyTool, Update_Date ) values( '";
                sqlQuerry += item.ProductName + "', '" + item.ProductNo  +"', '" + item.QtyInBox + "', '" + item.QtyTool + "',GETDATE() )" ;
                sqlCON sqlCON = new sqlCON();
                sqlCON.sqlExecuteNonQuery(sqlQuerry, false);


            }
            return true;
        }

        public List<SettingBOM> LoadingSettingBOMs()
        {
            List<SettingBOM> settingBOMs = new List<SettingBOM>();
            try
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append(" select productName, productNo,QTyinBox, QtyTool  from t_settingBOM ");
                sqlCON sqlCON = new sqlCON();
                DataTable dt = new DataTable();
                sqlCON.sqlDataAdapterFillDatatable(stringBuilder.ToString(), ref dt);
                settingBOMs = (from DataRow dr in dt.Rows
                               select new SettingBOM()
                               {
                                   ProductName = (dr["ProductName"] != null) ? dr["ProductName"].ToString().Trim() : "",
                                   ProductNo = (dr["productNo"] != null) ? dr["productNo"].ToString().Trim() : "",
                                   QtyInBox = (dr["QTyinBox"].ToString() != null) ? int.Parse(dr["QTyinBox"].ToString().Trim()) : 0,
                                   QtyTool = (dr["QtyTool"].ToString() != null) ? int.Parse(dr["QtyTool"].ToString().Trim()) : 0
                               }).ToList();

            }
            catch (Exception)
            {

                return null;
            }
            return settingBOMs;
        }
        public bool UpdateToDatabase(string productNo,int QtyinPacking, int QtyTool)
        {
            try
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append(" update t_settingBOM ");
                stringBuilder.Append(" set  QTyinBox = '" + QtyinPacking.ToString() + "' , " );
                stringBuilder.Append("  QtyTool = '" + QtyTool.ToString() + "' ");
                stringBuilder.Append(" where  productNo = '" + productNo + "'");
                sqlCON sqlCON = new sqlCON();
            return    sqlCON.sqlExecuteNonQuery(stringBuilder.ToString(), false);
            }
            catch (Exception)
            {

                return false;
            }

           
        }

        public bool DeleteRowofProduct(string productNo)
        {
            try
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append(" delete from  t_settingBOM ");  
                stringBuilder.Append(" where  productNo = '" + productNo + "'");
                sqlCON sqlCON = new sqlCON();
                return sqlCON.sqlExecuteNonQuery(stringBuilder.ToString(), false);
            }
            catch (Exception)
            {

                return false;
            }


        }
        public bool InsertRowofProduct(string productname, string productNo, int QtyBox, int QtyTool)
        {
            try
            {
                string sqlQuerry = "";
                sqlQuerry += "insert into t_settingBOM (productName, productNo, QTyinBox, QtyTool, Update_Date ) values( '";
                sqlQuerry += productname + "', '" + productNo + "', '" + QtyBox + "', '" + QtyTool + "',GETDATE() )";
                sqlCON sqlCON = new sqlCON();
             return   sqlCON.sqlExecuteNonQuery(sqlQuerry, false);
            }
            catch (Exception)
            {

                return false;
            }


        }
        public List<SettingBOM> LoadingSettingBOMsFilter(string ProductNo)
        {
            List<SettingBOM> settingBOMs = new List<SettingBOM>();
            try
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append(" select productName, productNo,QTyinBox, QtyTool  from t_settingBOM ");
                stringBuilder.Append(" where productNo like '%"+ ProductNo + "%'" );
                sqlCON sqlCON = new sqlCON();
                DataTable dt = new DataTable();
                sqlCON.sqlDataAdapterFillDatatable(stringBuilder.ToString(), ref dt);
                settingBOMs = (from DataRow dr in dt.Rows
                               select new SettingBOM()
                               {
                                   ProductName = (dr["ProductName"] != null) ? dr["ProductName"].ToString().Trim() : "",
                                   ProductNo = (dr["productNo"] != null) ? dr["productNo"].ToString().Trim() : "",
                                   QtyInBox = (dr["QTyinBox"].ToString() != null) ? int.Parse(dr["QTyinBox"].ToString().Trim()) : 0,
                                   QtyTool = (dr["QtyTool"].ToString() != null) ? int.Parse(dr["QtyTool"].ToString().Trim()) : 0
                               }).ToList();

            }
            catch (Exception)
            {

                return null;
            }
            return settingBOMs;
        }

        public bool InsertToManufactureSettingTableIntilizer(List<SettingBOM> settingBOMs)
        {

            foreach (var item in settingBOMs)
            {
                string sqlQuerry = "";
                sqlQuerry += "insert into t_settingManufacture (productName, productNo, WorkerQty, WorkerTarget, Update_Date ) values( '";
                sqlQuerry += item.ProductName + "', '" + item.ProductNo + "', '" + 0 + "', '" + 0 + "',GETDATE() )";
                sqlCON sqlCON = new sqlCON();
                sqlCON.sqlExecuteNonQuery(sqlQuerry, false);


            }
            return true;
        }
        public List<SettingManufacture> LoadingSettingManufacture()
        {
            List<SettingManufacture> settingManuf= new List<SettingManufacture>();
            try
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append(" select productName, productNo,WorkerQty, WorkerTarget  from t_settingManufacture ");
                sqlCON sqlCON = new sqlCON();
                DataTable dt = new DataTable();
                sqlCON.sqlDataAdapterFillDatatable(stringBuilder.ToString(), ref dt);
                settingManuf = (from DataRow dr in dt.Rows
                               select new SettingManufacture()
                               {
                                   ProductName = (dr["ProductName"] != null) ? dr["ProductName"].ToString().Trim() : "",
                                   ProductNo = (dr["productNo"] != null) ? dr["productNo"].ToString().Trim() : "",
                                  WorkerQty = (dr["WorkerQty"].ToString() != null) ? int.Parse(dr["WorkerQty"].ToString().Trim()) : 0,
                                   Workertarget = (dr["WorkerTarget"].ToString() != null) ? int.Parse(dr["WorkerTarget"].ToString().Trim()) : 0
                               }).ToList();

            }
            catch (Exception)
            {

                return null;
            }
            return settingManuf;
        }
        public bool InsertRowofManufacture(string productname, string productNo, int WorkerQty, int Workertarget)
        {
            try
            {
                string sqlQuerry = "";
                sqlQuerry += "insert into t_settingManufacture (productName, productNo, WorkerQty, WorkerTarget, Update_Date ) values( '";
                sqlQuerry += productname + "', '" + productNo + "', '" + WorkerQty + "', '" + Workertarget + "',GETDATE() )";
                sqlCON sqlCON = new sqlCON();
                return sqlCON.sqlExecuteNonQuery(sqlQuerry, false);
            }
            catch (Exception)
            {

                return false;
            }


        }
        public bool UpdateToManufacture(string productNo, int WorkerQty, int Workertarget)
        {
            try
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append(" update t_settingManufacture ");
                stringBuilder.Append(" set  WorkerQty = '" + WorkerQty.ToString() + "' , ");
                stringBuilder.Append("  WorkerTarget = '" + Workertarget.ToString() + "' ");
            //    stringBuilder.Append("  Update_Date = 'GETDATE()' ");
                stringBuilder.Append(" where  productNo = '" + productNo + "'");
                sqlCON sqlCON = new sqlCON();
                return sqlCON.sqlExecuteNonQuery(stringBuilder.ToString(), false);
            }
            catch (Exception)
            {

                return false;
            }


        }

        public bool DeleteRowofManufaccture(string productNo)
        {
            try
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append(" delete from  t_settingManufacture");
                stringBuilder.Append(" where  productNo = '" + productNo + "'");
                sqlCON sqlCON = new sqlCON();
                return sqlCON.sqlExecuteNonQuery(stringBuilder.ToString(), false);
            }
            catch (Exception)
            {

                return false;
            }


        }
        public List<SettingManufacture> LoadingSettingManufactureFilter(string ProductNo)
        {
            List<SettingManufacture> settingManuf = new List<SettingManufacture>();
            try
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append(" select productName, productNo,WorkerQty, WorkerTarget  from t_settingManufacture ");
                stringBuilder.Append(" where productNo like '%" + ProductNo + "%'");
                sqlCON sqlCON = new sqlCON();
                DataTable dt = new DataTable();
                sqlCON.sqlDataAdapterFillDatatable(stringBuilder.ToString(), ref dt);
                settingManuf = (from DataRow dr in dt.Rows
                                select new SettingManufacture()
                                {
                                    ProductName = (dr["ProductName"] != null) ? dr["ProductName"].ToString().Trim() : "",
                                    ProductNo = (dr["productNo"] != null) ? dr["productNo"].ToString().Trim() : "",
                                    WorkerQty = (dr["WorkerQty"].ToString() != null) ? int.Parse(dr["WorkerQty"].ToString().Trim()) : 0,
                                    Workertarget = (dr["WorkerTarget"].ToString() != null) ? int.Parse(dr["WorkerTarget"].ToString().Trim()) : 0
                                }).ToList();

            }
            catch (Exception)
            {

                return null;
            }
            return settingManuf;
        }

        public SFT_WIP GetSFT_WIPofProducts (string product)
        {
            SFT_WIP sFT_WIP = new SFT_WIP();
            try
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append(@"select  sum(LOTSIZE)  from LOT 
where  ERP_OPSEQ = '0010' and STATUS = '0' 
 ");
                stringBuilder.Append(" and ITEMID =  '" + product + "'");
               sqlSFT sqlERPCON = new sqlSFT();
                var Temp = sqlERPCON.sqlExecuteScalarString(stringBuilder.ToString());
                if (Temp != null && Temp != "")
                {
                    sFT_WIP.MQC_In_Available = double.Parse(Temp.ToString());
                }
                else sFT_WIP.MQC_In_Available = 0;

                stringBuilder = new StringBuilder();
                stringBuilder.Append(@"select  sum(LOTSIZE)  from LOT 
where  ERP_OPSEQ = '0010' and STATUS = '50' 
 ");
                stringBuilder.Append(" and ITEMID =  '" + product + "'");
              //  sqlERPCON sqlERPCON = new sqlERPCON();
                Temp = sqlERPCON.sqlExecuteScalarString(stringBuilder.ToString());
                if (Temp != null && Temp != "")
                {
                    sFT_WIP.MQC_Out_Available = double.Parse(Temp.ToString());
                }
                else sFT_WIP.MQC_Out_Available = 0;

                stringBuilder = new StringBuilder();
                stringBuilder.Append(@"select  sum(LOTSIZE)  from LOT 
where  ERP_OPSEQ = '0020' and STATUS = '0' 
 ");
                stringBuilder.Append(" and ITEMID =  '" + product + "'");
                //  sqlERPCON sqlERPCON = new sqlERPCON();
                Temp = sqlERPCON.sqlExecuteScalarString(stringBuilder.ToString());
                if (Temp != null && Temp != "")
                {
                    sFT_WIP.PQC_In_Available = double.Parse(Temp.ToString());
                }
                else sFT_WIP.PQC_In_Available = 0;

                stringBuilder = new StringBuilder();
                stringBuilder.Append(@"select  sum(LOTSIZE)  from LOT 
where  ERP_OPSEQ = '0020' and STATUS = '50' 
 ");
                stringBuilder.Append(" and ITEMID =  '" + product + "'");
                //  sqlERPCON sqlERPCON = new sqlERPCON();
                Temp = sqlERPCON.sqlExecuteScalarString(stringBuilder.ToString());
                if (Temp != null && Temp != "")
                {
                    sFT_WIP.PQC_Out_Available= double.Parse(Temp.ToString());
                }
                else sFT_WIP.PQC_Out_Available = 0;

                stringBuilder = new StringBuilder();
                stringBuilder.Append(@"select  sum(LOTSIZE)  from LOT 
where  ERP_OPSEQ = '0020' and STATUS = '130' 
 ");
                stringBuilder.Append(" and ITEMID =  '" + product + "'");
                //  sqlERPCON sqlERPCON = new sqlERPCON();
                Temp = sqlERPCON.sqlExecuteScalarString(stringBuilder.ToString());
                if (Temp != null && Temp != "")
                {
                    sFT_WIP.StockIntoWH = double.Parse(Temp.ToString());
                }
                else sFT_WIP.StockIntoWH = 0;
            }
            catch (Exception ex)
            {

                return null;
            }
            return sFT_WIP;

        }

    }
}

