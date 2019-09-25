using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace WindowsFormsApplication1.ChartDrawing
{
    class ChartDrawing
    {
        public static void DrawingColumsChart(string[] XValues, double[] YValues, ref Chart Chart, string Tiltle)
        {
            if (XValues.Count() != YValues.Count())
                return;
            Chart.Series.Clear();
            Chart.Titles.Clear();
            Title title = new Title();
            title.Font = new Font("Arial", 12, FontStyle.Bold);

            title.Text = Tiltle;

            Chart.Titles.Add(title);

            Series series = Chart.Series.Add("chart_series");
            series.YAxisType = AxisType.Primary;
            series.ChartType = SeriesChartType.Column;
            series.AxisLabel = "chart_series";
            series.IsValueShownAsLabel = true;
            series.IsVisibleInLegend = false;
            Chart.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            Chart.ChartAreas[0].AxisX.MinorGrid.Enabled = false;
            Chart.ChartAreas[0].AxisX.LabelStyle.Font = new Font("Verdana", 8, FontStyle.Bold);
            double Max = YValues.Max();
            Chart.ChartAreas[0].AxisY.Maximum = Max + 1;
            Chart.ChartAreas[0].AxisX.IsLabelAutoFit = true;
            Chart.ChartAreas[0].AxisY.IsLabelAutoFit = true;
            Chart.ChartAreas[0].AxisX.Interval = 1;
            //chart_shipping.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
            //chart_shipping.ChartAreas[0].AxisY.MinorGrid.Enabled = false;

            for (int i = 0; i < XValues.Count(); i++)
            {
                series.Points.AddXY(XValues[i], YValues[i]);

            }
         
        }
        public static void DrawingColumsChartRightYAXis(string[] XValues, double[] YValues, ref Chart Chart, string Tiltle)
        {
            if (XValues.Count() != YValues.Count())
                return;
            Chart.Series.Clear();
            Chart.Titles.Clear();
            Title title = new Title();
            title.Font = new Font("Arial", 12, FontStyle.Bold);

            title.Text = Tiltle;

            Chart.Titles.Add(title);
            System.Windows.Forms.DataVisualization.Charting.Series ChartSeries2 = (new System.Windows.Forms.DataVisualization.Charting.Series());
            ChartSeries2.ChartArea = "ChartArea0";
            ChartSeries2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            ChartSeries2.YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary;        //<Select secondary Axis Y2
            ChartSeries2.Legend = "Legend1";
            ChartSeries2.Name = "Power";
            ChartSeries2.Points.DataBindXY(XValues, YValues);
            

        }
      public static void DrawTwoChartInside(string[] XValues, double[] YValues, double[] YValues2, double TargetOutput, double targetScrap, ref Chart Chart, string Tiltle)
        {
           
           System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = (new System.Windows.Forms.DataVisualization.Charting.ChartArea());
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = (new System.Windows.Forms.DataVisualization.Charting.Legend());

            chartArea1.AxisX.TitleFont = new System.Drawing.Font("Arial", 12);
            chartArea1.AxisY.TitleFont = new System.Drawing.Font("Arial", 12);
            chartArea1.AxisY2.TitleFont = new System.Drawing.Font("Arial", 12);

           
            chartArea1.AxisX.Title = "Date";
        //    chartArea1.AxisX.Minimum = 0;
          //  chartArea1.AxisX.Maximum = 
            //chartArea1.AxisX.Interval = 60;                 //Axis marker every #
            //chartArea1.AxisX.LabelStyle.Interval = 300;     //Label the axis every #
            //chartArea1.AxisX.MajorGrid.Interval = 60;
            double Max = YValues.Max();//Grid line every #
            double min = YValues.Min();//Grid line every #
            double count = YValues.Count();//Grid line every #
            chartArea1.AxisX.MinorGrid.Interval = 1;
            //chartArea1.AxisX.MajorGrid.LineColor = System.Drawing.Color.DarkGray;
            //chartArea1.AxisX.MinorGrid.LineColor = System.Drawing.Color.DarkGray;
            chartArea1.AxisX.Interval = 1;
            chartArea1.AxisX.MajorGrid.Enabled = false;




            chartArea1.AxisY.Title = "Output Quantity";
            chartArea1.AxisY.TextOrientation = System.Windows.Forms.DataVisualization.Charting.TextOrientation.Rotated270;
            chartArea1.AxisY.Minimum = 0;
            chartArea1.AxisY.Maximum =(Max > TargetOutput) ?  Max + 1 : TargetOutput+1;
            chartArea1.AxisY.MajorGrid.Enabled = false;
            //  chartArea1.AxisY.Interval =(int) (Max / count);                 //Axis marker every #
            //chartArea1.AxisY.LabelStyle.Interval = 10;      //Label the axis every #
            //chartArea1.AxisY.MajorGrid.Interval = 5;        //Grid line every #
            //chartArea1.AxisY.MinorGrid.Interval = 1;
            //chartArea1.AxisY.MajorGrid.LineColor = System.Drawing.Color.DarkGray;
            //chartArea1.AxisY.MinorGrid.LineColor = System.Drawing.Color.DarkGray;

            chartArea1.AxisY2.Title = "% Scrap";
            chartArea1.AxisY2.TextOrientation = System.Windows.Forms.DataVisualization.Charting.TextOrientation.Rotated270;
            chartArea1.AxisY2.Minimum = 0;
            chartArea1.AxisY2.Maximum =(YValues2.Max()  > targetScrap) ? YValues2.Max()  + 0.1 : targetScrap+0.1;
            //chartArea1.AxisY2.Interval = (double)(YValues2.Max() / YValues2.Count());              //Axis marker every #
            //chartArea1.AxisY2.LabelStyle.Interval = 0.01;    //Label the axis every #
            //chartArea1.AxisY2.MajorGrid.Interval = 100;     //Grid line every #
            //chartArea1.AxisY2.MinorGrid.Interval = 10;

            chartArea1.AxisY2.MajorGrid.Enabled = false;    //Hide the grid lines across the chart area
            //chartArea1.AxisY2.MajorGrid.LineColor = System.Drawing.Color.DarkGray;
            //chartArea1.AxisY2.MinorGrid.LineColor = System.Drawing.Color.DarkGray;


           Chart.ChartAreas.RemoveAt(0);
            chartArea1.Name = "ChartArea1";
            Chart.ChartAreas.Add(chartArea1);

            Chart.Legends.RemoveAt(0);
            legend1.Name = "Legend1";
            Chart.Legends.Add(legend1);

            Chart.Text = "chart1";

            while (Chart.Series.Count > 0)
                Chart.Series.RemoveAt(0);

            System.Windows.Forms.DataVisualization.Charting.Series ChartSeries1 = (new System.Windows.Forms.DataVisualization.Charting.Series());
            ChartSeries1.ChartArea = "ChartArea1";
            ChartSeries1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            ChartSeries1.YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Primary;      //<Select primary Axis Y
            ChartSeries1.Legend = "Legend1";
            ChartSeries1.Name = "Output Quantity" ;
            ChartSeries1.Font = new System.Drawing.Font("Arial", 12);
           ChartSeries1.LabelForeColor = Color.Blue;
            ChartSeries1.IsValueShownAsLabel = true;
          
            ChartSeries1.Points.DataBindXY(XValues, YValues);
            Chart.Series.Add(ChartSeries1);

            //}
            System.Windows.Forms.DataVisualization.Charting.Series ChartSeries2 = (new System.Windows.Forms.DataVisualization.Charting.Series());
            ChartSeries2.ChartArea = "ChartArea1";
            ChartSeries2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            
            ChartSeries2.YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary;        //<Select secondary Axis Y2
            ChartSeries2.Legend = "Legend1";
            ChartSeries2.Name = "% Scrap";
            ChartSeries2.BorderWidth = 5;
            ChartSeries2.IsValueShownAsLabel = true;
            ChartSeries2.LabelFormat = "#.#' %'";
            ChartSeries2.Font = new System.Drawing.Font("Arial", 12);

            ChartSeries2.LabelForeColor = Color.OrangeRed;


            ChartSeries2.Points.DataBindXY(XValues, YValues2);
            Chart.Series.Add(ChartSeries2);



            System.Windows.Forms.DataVisualization.Charting.Series ChartSeries3 = (new System.Windows.Forms.DataVisualization.Charting.Series());
            ChartSeries3.ChartArea = "ChartArea1";
            ChartSeries3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;

            ChartSeries3.YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Primary;        //<Select secondary Axis Y2
            ChartSeries3.Legend = "Legend1";
            ChartSeries3.Name = "Output target";
            ChartSeries3.BorderWidth = 3;
       //     ChartSeries2.IsValueShownAsLabel = true;
           // ChartSeries3.LabelFormat = "#.#' %'";
            ChartSeries3.Font = new System.Drawing.Font("Arial", 12);

            //     ChartSeries2.LabelForeColor = Color.OrangeRed;

            for (int i = 0; i < XValues.Count(); i++)
            {
                ChartSeries3.Points.AddXY(XValues[i], TargetOutput);

            }

            Chart.Series.Add(ChartSeries3);
            System.Windows.Forms.DataVisualization.Charting.Series ChartSeries4 = (new System.Windows.Forms.DataVisualization.Charting.Series());
            ChartSeries4.ChartArea = "ChartArea1";
            ChartSeries4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;

            ChartSeries4.YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary;        //<Select secondary Axis Y2
            ChartSeries4.Legend = "Legend1";
            ChartSeries4.Name = "%Scap target";
            ChartSeries4.BorderWidth = 3;
         //   ChartSeries3.LabelFormat = "#.#' %'";
            ChartSeries4.Font = new System.Drawing.Font("Arial", 12);

            for (int i = 0; i < XValues.Count(); i++)
            {
                ChartSeries4.Points.AddXY(XValues[i], targetScrap*100);
            }

            Chart.Series.Add(ChartSeries4);

            Chart.Visible = true;

            return;
        }

        public static void DrawCrisisReport(string[] XValues, int[] YValues, string[] XValues2, int[] YValues2,
            string[] XValues3, int[] YValues3 ,ref Chart Chartref, string Tiltle)
        {
            Chartref.Titles.Clear();
            Chartref.Series.Clear();
            Title title = new Title();
            title.Font = new Font("Arial", 14, FontStyle.Bold);
            title.Text = Tiltle;
            Chartref.Titles.Add(title);

            Dictionary<string, int> dicMonthConvert;
            if (XValues[0] != null)
            {
                //  Chartref.ChartAreas.Clear();

               
                System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = (new System.Windows.Forms.DataVisualization.Charting.ChartArea());
                System.Windows.Forms.DataVisualization.Charting.Legend legend1 = (new System.Windows.Forms.DataVisualization.Charting.Legend());

                chartArea1.AxisX.TitleFont = new System.Drawing.Font("Arial", 12);
                chartArea1.AxisY.TitleFont = new System.Drawing.Font("Arial", 12);
                chartArea1.AxisY2.TitleFont = new System.Drawing.Font("Arial", 12);

                chartArea1.AxisX.Title = "Month";
                chartArea1.AxisX.LabelStyle.Font = new Font("Arial", 12);
                chartArea1.AxisX.TitleForeColor = Color.Blue;
                chartArea1.AxisX.LabelStyle.ForeColor = Color.Blue;

                chartArea1.AxisX.MinorGrid.Interval = 1;
                chartArea1.AxisX.Interval = 1;
                chartArea1.AxisX.MajorGrid.Enabled = false;

                chartArea1.AxisY.Title = "Quantity (pcs)";
                chartArea1.AxisY.TitleForeColor = Color.Blue;
                chartArea1.AxisY.LabelStyle.ForeColor = Color.Blue;
                chartArea1.AxisY.LabelStyle.Font = new Font("Arial", 12);
                chartArea1.AxisY.TextOrientation = System.Windows.Forms.DataVisualization.Charting.TextOrientation.Rotated270;
                chartArea1.AxisY.Minimum = 0;
                chartArea1.AxisY.Maximum = Math.Max(Math.Max(YValues.Max(), YValues2.Max()), YValues3.Max()) * 1.2;

                chartArea1.AxisY.MajorGrid.Enabled = true;

                Chartref.ChartAreas.RemoveAt(0);
                chartArea1.Name = "ChartArea1";
                Chartref.ChartAreas.Add(chartArea1);

                Chartref.Legends.RemoveAt(0);
                legend1.Name = "Legend1";
                Chartref.Legends.Add(legend1);

                Chartref.Text = "chart1";

                while (Chartref.Series.Count == 2)
                {
                    Chartref.Series.RemoveAt(0);
                    Chartref.Series.RemoveAt(1);
                }

                System.Windows.Forms.DataVisualization.Charting.Series ChartSeries1 = (new System.Windows.Forms.DataVisualization.Charting.Series());
                ChartSeries1.ChartArea = "ChartArea1";
                ChartSeries1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
                ChartSeries1.YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Primary;
                ChartSeries1.XAxisType = AxisType.Primary;//<Select primary Axis X
                ChartSeries1.Legend = "Legend1";
                ChartSeries1.Name = "Late";
                ChartSeries1.Font = new System.Drawing.Font("Arial", 10);
                //  ChartSeries1.LabelForeColor = Color.Red;
                ChartSeries1.IsValueShownAsLabel = true;
                ChartSeries1.Color = Color.Red;

                 dicMonthConvert = new Dictionary<string, int>();
                dicMonthConvert = DicConvertMonthInYears(XValues, YValues);
                for (int i = 0; i < dicMonthConvert.Count(); i++)
                {
                    string tem = dicMonthConvert.ToList()[i].Key;
                    int intTem = dicMonthConvert.ToList()[i].Value;
                    ChartSeries1.Points.AddXY(tem, intTem);
                    ChartSeries1.Points[i].Color = Color.Red;
                  
                }


                Chartref.Series.Add(ChartSeries1);
            }
            if (XValues2[0] != null)
            {

                //}
                System.Windows.Forms.DataVisualization.Charting.Series ChartSeries2 = (new System.Windows.Forms.DataVisualization.Charting.Series());
                ChartSeries2.ChartArea = "ChartArea1";
                ChartSeries2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
                ChartSeries2.XAxisType = AxisType.Primary;
                ChartSeries2.YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Primary;        //<Select secondary Axis Y2
                ChartSeries2.Legend = "Legend1";
                ChartSeries2.Name = "Back Log";
                ChartSeries2.BorderWidth = 5;
                ChartSeries2.IsValueShownAsLabel = true;
                // ChartSeries2.LabelFormat = "#.#' %'";
                ChartSeries2.Font = new System.Drawing.Font("Arial", 10);

                ChartSeries2.Color = Color.Yellow;
                dicMonthConvert = DicConvertMonthInYears(XValues2, YValues2);
                for (int i = 0; i < dicMonthConvert.Count(); i++)
                {
                    string tem = dicMonthConvert.ToList()[i].Key;
                    int intTem = dicMonthConvert.ToList()[i].Value;
                    ChartSeries2.Points.AddXY(tem, intTem);


                }

                Chartref.Series.Add(ChartSeries2);
            }
            if (XValues3[0] != null)
            {
                System.Windows.Forms.DataVisualization.Charting.Series ChartSeries3 = (new System.Windows.Forms.DataVisualization.Charting.Series());
                ChartSeries3.ChartArea = "ChartArea1";
                ChartSeries3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
                ChartSeries3.XAxisType = AxisType.Primary;//<Select primary Axis X
                ChartSeries3.YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Primary;        //<Select secondary Axis Y2
                ChartSeries3.Legend = "Legend1";
                ChartSeries3.Name = "Open Order";
                ChartSeries3.BorderWidth = 5;
                ChartSeries3.IsValueShownAsLabel = true;
                // ChartSeries2.LabelFormat = "#.#' %'";
                ChartSeries3.Font = new System.Drawing.Font("Arial", 10);

                ChartSeries3.Color = Color.Blue;
                dicMonthConvert = DicConvertMonthInYears(XValues3, YValues3);
                for (int i = 0; i < dicMonthConvert.Count(); i++)
                {
                    string tem = dicMonthConvert.ToList()[i].Key;
                    int intTem = dicMonthConvert.ToList()[i].Value;
                    ChartSeries3.Points.AddXY(tem, intTem);


                }

                Chartref.Series.Add(ChartSeries3);
            }

            Chartref.Visible = true;

            return;
        }
        public static void DrawCrisisReportShipped(string[] XValues, int[] YValues, string[] XValues2, int[] YValues2 ,ref Chart Chartref, string Tiltle)
        {
            Chartref.Titles.Clear();
            Chartref.Series.Clear();
           // Chartref.ChartAreas.Clear();

            Title title = new Title();
            title.Font = new Font("Arial", 14, FontStyle.Bold);
            title.Text = Tiltle;
            Chartref.Titles.Add(title);
            Dictionary<string, int> dicMonthConvert;
            if (XValues[0] != null)
            {

                System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = (new System.Windows.Forms.DataVisualization.Charting.ChartArea());
                System.Windows.Forms.DataVisualization.Charting.Legend legend1 = (new System.Windows.Forms.DataVisualization.Charting.Legend());

                chartArea1.AxisX.TitleFont = new System.Drawing.Font("Arial", 12);
                chartArea1.AxisY.TitleFont = new System.Drawing.Font("Arial", 12);
                chartArea1.AxisY2.TitleFont = new System.Drawing.Font("Arial", 12);

                chartArea1.AxisX.Title = "Month";
                chartArea1.AxisX.LabelStyle.Font = new Font("Arial", 12);
                chartArea1.AxisX.TitleForeColor = Color.Blue;
                chartArea1.AxisX.LabelStyle.ForeColor = Color.Blue;

                chartArea1.AxisX.MinorGrid.Interval = 1;
                chartArea1.AxisX.Interval = 1;
                chartArea1.AxisX.MajorGrid.Enabled = false;

                chartArea1.AxisY.Title = "Quantity (pcs)";
                chartArea1.AxisY.TitleForeColor = Color.Blue;
                chartArea1.AxisY.LabelStyle.ForeColor = Color.Blue;
                chartArea1.AxisY.LabelStyle.Font = new Font("Arial", 12);
                chartArea1.AxisY.TextOrientation = System.Windows.Forms.DataVisualization.Charting.TextOrientation.Rotated270;
                chartArea1.AxisY.Minimum = 0;
                chartArea1.AxisY.Maximum = Math.Max(YValues.Max(), YValues2.Max()) * 1.2;

                chartArea1.AxisY.MajorGrid.Enabled = true;

                Chartref.ChartAreas.RemoveAt(0);
                chartArea1.Name = "ChartArea1";
                Chartref.ChartAreas.Add(chartArea1);

                Chartref.Legends.RemoveAt(0);
                legend1.Name = "Legend1";
                Chartref.Legends.Add(legend1);

                Chartref.Text = "chart1";

                while (Chartref.Series.Count == 2)
                {
                    Chartref.Series.RemoveAt(0);

                }

                System.Windows.Forms.DataVisualization.Charting.Series ChartSeries1 = (new System.Windows.Forms.DataVisualization.Charting.Series());
                ChartSeries1.ChartArea = "ChartArea1";
                ChartSeries1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
                ChartSeries1.YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Primary;      //<Select primary Axis Y
                ChartSeries1.Legend = "Legend1";
                ChartSeries1.Name = "Shipped-Late";
                ChartSeries1.Font = new System.Drawing.Font("Arial", 10);
                //  ChartSeries1.LabelForeColor = Color.Red;
                ChartSeries1.IsValueShownAsLabel = true;
                ChartSeries1.Color = Color.Orange;

                 dicMonthConvert = new Dictionary<string, int>();
                dicMonthConvert = DicConvertMonthInYears(XValues, YValues);
                for (int i = 0; i < dicMonthConvert.Count(); i++)
                {
                    string tem = dicMonthConvert.ToList()[i].Key;
                    int intTem = dicMonthConvert.ToList()[i].Value;
                    ChartSeries1.Points.AddXY(tem, intTem);
                }


                Chartref.Series.Add(ChartSeries1);
            }
            if (XValues2[0] != null)
            {

                //}
                System.Windows.Forms.DataVisualization.Charting.Series ChartSeries2 = (new System.Windows.Forms.DataVisualization.Charting.Series());
                ChartSeries2.ChartArea = "ChartArea1";
                ChartSeries2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;

                ChartSeries2.YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Primary;        //<Select secondary Axis Y2
                ChartSeries2.Legend = "Legend1";
                ChartSeries2.Name = "Shipped-On Time";
                ChartSeries2.BorderWidth = 5;
                ChartSeries2.IsValueShownAsLabel = true;
                // ChartSeries2.LabelFormat = "#.#' %'";
                ChartSeries2.Font = new System.Drawing.Font("Arial", 10);

                ChartSeries2.Color = Color.Green;
                dicMonthConvert = DicConvertMonthInYears(XValues2, YValues2);
                for (int i = 0; i < dicMonthConvert.Count(); i++)
                {
                    string tem = dicMonthConvert.ToList()[i].Key;
                    int intTem = dicMonthConvert.ToList()[i].Value;
                    ChartSeries2.Points.AddXY(tem, intTem);
                }
                Chartref.Series.Add(ChartSeries2);

            }

            Chartref.Visible = true;

            return;
        }
        public static Dictionary<string, int> DicConvertMonthInYears(string[] months,int[] Axis)
        {
            Dictionary<string, int> dic = new Dictionary<string, int>();

            dic.Add("Jan", 0);
            dic.Add("Feb", 0);
            dic.Add("Mar", 0);
            dic.Add("Apr", 0);
            dic.Add("May", 0);
            dic.Add("Jun", 0);
            dic.Add("Jul", 0);
            dic.Add("Aug", 0);
            dic.Add("Sep", 0);
            dic.Add("Oct", 0);
            dic.Add("Nov", 0);
            dic.Add("Dec", 0);
           
                for (int i = 0; i < months.Count(); i++)
                {
                    if (months[i] != null && dic.ContainsKey(months[i]))
                    {
                        dic[months[i]] = Axis[i];
                    }
                }
         
            return dic;
        }


    }
}
