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
                ChartSeries4.Points.AddXY(XValues[i], targetScrap);
            }

            Chart.Series.Add(ChartSeries4);

            Chart.Visible = true;

            return;
        }
        public static void DrawCrisisReport(string[] XValues, int[] YValues, string[] XValues2, int[] YValues2, string[] XValues3, int[] YValues3, ref Chart Chartref, string Tiltle)
        {
            Chartref.Titles.Clear();

              Title title = new Title();
            title.Font = new Font("Arial", 14, FontStyle.Bold);
            title.Text = Tiltle;
            Chartref.Titles.Add(title);

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

            chartArea1.AxisY.Title = "Quantity (unit)";
            chartArea1.AxisY.TitleForeColor = Color.Blue; 
            chartArea1.AxisY.LabelStyle.ForeColor = Color.Blue;
            chartArea1.AxisY.LabelStyle.Font = new Font("Arial", 12);
            chartArea1.AxisY.TextOrientation = System.Windows.Forms.DataVisualization.Charting.TextOrientation.Rotated270;
            chartArea1.AxisY.Minimum = 0;
            chartArea1.AxisY.Maximum = Math.Max(Math.Max(YValues.Max(), YValues2.Max()), YValues3.Max())+1;

            chartArea1.AxisY.MajorGrid.Enabled = true;

            Chartref.ChartAreas.RemoveAt(0);
            chartArea1.Name = "ChartArea1";
            Chartref.ChartAreas.Add(chartArea1);

            Chartref.Legends.RemoveAt(0);
            legend1.Name = "Legend1";
            Chartref.Legends.Add(legend1);

            Chartref.Text = "chart1";

            while (Chartref.Series.Count > 0)
                Chartref.Series.RemoveAt(0);

            System.Windows.Forms.DataVisualization.Charting.Series ChartSeries1 = (new System.Windows.Forms.DataVisualization.Charting.Series());
            ChartSeries1.ChartArea = "ChartArea1";
            ChartSeries1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            ChartSeries1.YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Primary;      //<Select primary Axis Y
            ChartSeries1.Legend = "Legend1";
            ChartSeries1.Name = "Late";
            ChartSeries1.Font = new System.Drawing.Font("Arial", 12);
          //  ChartSeries1.LabelForeColor = Color.Red;
            ChartSeries1.IsValueShownAsLabel = true;
            ChartSeries1.Color = Color.Red;
            //      ChartSeries1.LabelBackColor = Color.Red;
            for (int i = 0; i < XValues.Count(); i++)
            {
                ChartSeries1.Points.AddXY(XValues[i], YValues[i]);
                ChartSeries1.Points[i].Color = Color.Red;

                

            }

            Chartref.Series.Add(ChartSeries1);

            //}
            System.Windows.Forms.DataVisualization.Charting.Series ChartSeries2 = (new System.Windows.Forms.DataVisualization.Charting.Series());
            ChartSeries2.ChartArea = "ChartArea1";
            ChartSeries2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;

            ChartSeries2.YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Primary;        //<Select secondary Axis Y2
            ChartSeries2.Legend = "Legend1";
            ChartSeries2.Name = "BackLog";
            ChartSeries2.BorderWidth = 5;
            ChartSeries2.IsValueShownAsLabel = true;
            // ChartSeries2.LabelFormat = "#.#' %'";
            ChartSeries2.Font = new System.Drawing.Font("Arial", 12);

            ChartSeries2.Color = Color.Yellow;
            for (int i = 0; i < XValues2.Count(); i++)
            {
                ChartSeries2.Points.AddXY(XValues2[i], YValues2[i]);
                ChartSeries2.Points[i].Color = Color.Yellow;
            }
            Chartref.Series.Add(ChartSeries2);

            System.Windows.Forms.DataVisualization.Charting.Series ChartSeries3 = (new System.Windows.Forms.DataVisualization.Charting.Series());
            ChartSeries3.ChartArea = "ChartArea1";
            ChartSeries3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;

            ChartSeries3.YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Primary;        //<Select secondary Axis Y2
            ChartSeries3.Legend = "Legend1";
            ChartSeries3.Name = "OpenOrder";
            ChartSeries3.BorderWidth = 5;
            ChartSeries3.IsValueShownAsLabel = true;
            // ChartSeries2.LabelFormat = "#.#' %'";
            ChartSeries3.Font = new System.Drawing.Font("Arial", 12);

            ChartSeries3.Color = Color.Green;
            for (int i = 0; i < XValues.Count(); i++)
            {
                for (int j = 0; j < XValues3.Count(); j++)
                {
                    if (XValues[i] == XValues3[j])
                    {
                        ChartSeries3.Points.AddXY(XValues[i], YValues3[j]);
                      //  ChartSeries3.Points[].Color = Color.Green;
                    }
                    else
                    {
                        ChartSeries3.Points.AddXY(XValues[i],0);
                    }
                }
            }
          
            Chartref.Series.Add(ChartSeries3);

            Chartref.Visible = true;

            return;
        }
    }
}
