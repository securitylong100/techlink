using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using LiveCharts;
using LiveCharts.WinForms;
using LiveCharts.Wpf;
using CartesianChart = LiveCharts.WinForms.CartesianChart;

namespace WindowsFormsApplication1.ChartDrawing
{
  public  class LiveChartDrawing
    {
public void DrawingLiveChart(List<chartdatabyDate> chartdatabyDates, List<chartdatabyDate> chartdataDefect, ref CartesianChart chart)
        {
            //xu ly data de chon cac thong so cua chart
            double YmaxValue = 0;
            double YminValue = 0;
            Dictionary<string, double> ValueConvert = new Dictionary<string, double>();
            Dictionary<string, double> ValueConvertDefect = new Dictionary<string, double>();
            if (chartdatabyDates != null && chartdatabyDates.Count > 0)
            {
                chart.Series.Clear();
                chart.AxisX.Clear();
                chart.AxisY.Clear();
                chart.Controls.Clear();
                
               
                ValueConvert = DicChangeTime(chartdatabyDates);
                ValueConvertDefect = DicChangeTime(chartdataDefect);
                string[] TimeChanged = ValueConvert.Keys.ToArray();
                double[] OutputChanged = ValueConvert.Values.ToArray();
                double[] OutputChangedDefect = ValueConvertDefect.Values.ToArray();
                YmaxValue = OutputChanged.Max();
               
                YminValue = OutputChanged.Min();
                ChartValues<double> values = new ChartValues<double>();
                ChartValues<double> valuesTarget = new ChartValues<double>();
                ChartValues<double> valuesDefect = new ChartValues<double>();

                for (int i = 0; i < OutputChanged.Count(); i++)
                {
                    values.Add(OutputChanged[i]);
                    //  valuesTarget.Add(100);
                    if ((OutputChangedDefect[i] + OutputChanged[i]) != 0)
                        valuesDefect.Add(OutputChangedDefect[i] / (OutputChangedDefect[i] + OutputChanged[i]));
                    else valuesDefect.Add(0);
                    valuesTarget.Add(OutputChangedDefect[i]);
                }
                YmaxValue = values.Max();
               // YmaxValue = (values.Max() > valuesTarget.Max())? values.Max() : valuesTarget.Max();
                List<String> lables = new List<string>();
                for (int i = 0; i < TimeChanged.Count(); i++)
                {

                    lables.Add(TimeChanged[i]);
                }
                chart.Series = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Output Quantity",
                    Values = values,
                    DataLabels = true,
                    Fill = System.Windows.Media.Brushes.Green,
                   ScalesYAt =0,
                   FontSize = 15
                }

            };
                chart.Series.Add(
                new ColumnSeries
                {
                    Title = "Defect Quantity",
                    Values = valuesTarget,
                    DataLabels = true,
                    Fill = System.Windows.Media.Brushes.Red,
                    ScalesYAt = 0,
                    FontSize = 15

                }
            );
                chart.Series.Add(
                new LineSeries
                {
                    Title = "Defect Rate (%)",
                    Values = valuesDefect,
                    DataLabels = true,
                    Foreground = System.Windows.Media.Brushes.Orange,
                    PointForeground = System.Windows.Media.Brushes.Red,
                    ScalesYAt = 1,
                    FontSize = 15

                }
            ) ;

                chart.DefaultLegend.FontSize = 20;
                chart.LegendLocation = LegendLocation.Top;
              

                //x axis labels
                chart.AxisX.Add(new Axis
                {
                    Title = "Hours",
                    Labels = lables,
                    Unit = 1,
                    FontSize = 14,
                   
                    FontFamily = new System.Windows.Media.FontFamily("Times New Roman"),
                    Foreground = System.Windows.Media.Brushes.Black,
                    Separator = new LiveCharts.Wpf.Separator
                    {
                        Step = 1,
                        IsEnabled = false //disable it to make it invisible.
                    },
                    LabelsRotation = 0,
                });


                //y axis label
                chart.AxisY.Add(new Axis
                {
                    Title = "Quantity (pcs)",
                    LabelFormatter = value => value.ToString("N0"),
                    FontSize = 12,
                    FontFamily = new System.Windows.Media.FontFamily("Times New Roman"),
                    Foreground = System.Windows.Media.Brushes.Black,
                    MaxValue = YmaxValue *1.2,
                    MinValue = 0,
                    Position = AxisPosition.LeftBottom
                }) ;

                chart.AxisY.Add(new Axis
                {
                    Title = "percent (%)",
                    LabelFormatter = value => value.ToString("P1"),
                    FontSize = 12,
                    FontFamily = new System.Windows.Media.FontFamily("Times New Roman"),
                    Foreground = System.Windows.Media.Brushes.Black,
                    MaxValue = 1,
                    MinValue = 0,
                    Position = AxisPosition.RightTop
                });
                // chart.Zoom = ZoomingOptions.Xy;
               
            }

        }
    
        public Dictionary<string, double> DicChangeTime(List<chartdatabyDate> chartdatabyDates)
        {
            string[] time = null;
            double[] Axis = null;
            Axis = chartdatabyDates.Select(v => v.value).ToArray();
            time = chartdatabyDates.OrderBy(o =>o.time).Select(t => t.time.Hours.ToString()).ToArray();
            Dictionary<string, double> dic = new Dictionary<string, double>();

            dic.Add("8-10", 0);
            dic.Add("10-12", 0);
            dic.Add("12-14", 0);
            dic.Add("14-16", 0);
            dic.Add("16-18", 0);
            dic.Add("18-20", 0);
            dic.Add("20-22", 0);
            dic.Add("22-24", 0);
            dic.Add("0-2", 0);
            dic.Add("2-4", 0);
            dic.Add("4-6", 0);
            dic.Add("6-8", 0);

            for (int i = 0; i < time.Count(); i++)
            {
                if (int.Parse(time[i].Split(':')[0]) % 2 == 0)
                {
                    string key = (int.Parse(time[i].Split(':')[0])).ToString() + "-" + (int.Parse(time[i].Split(':')[0]) + 2).ToString();
                    dic[key] += Axis[i];
                }
                else
                {
                    string key = (int.Parse(time[i].Split(':')[0]) - 1).ToString() + "-" + (int.Parse(time[i].Split(':')[0]) + 1).ToString();
                    dic[key] += Axis[i];
                }
            }
            return dic;
        }
        private Dictionary<string, double> DicChangeTime(string[] time, double[] Axis)
        {
            Dictionary<string, double> dic = new Dictionary<string, double>();

            dic.Add("8-10", 0);
            dic.Add("10-12", 0);
            dic.Add("12-14", 0);
            dic.Add("14-16", 0);
            dic.Add("16-18", 0);
            dic.Add("18-20", 0);
            dic.Add("20-22", 0);
            dic.Add("22-24", 0);
            dic.Add("0-2", 0);
            dic.Add("2-4", 0);
            dic.Add("4-6", 0);
            dic.Add("6-8", 0);

            for (int i = 0; i < time.Count(); i++)
            {
                if (int.Parse(time[i].Split(':')[0]) % 2 == 0)
                {
                    string key = (int.Parse(time[i].Split(':')[0])).ToString() + "-" + (int.Parse(time[i].Split(':')[0]) + 2).ToString();
                    dic[key] += Axis[i];
                }
                else
                {
                    string key = (int.Parse(time[i].Split(':')[0]) - 1).ToString() + "-" + (int.Parse(time[i].Split(':')[0]) + 1).ToString();
                    dic[key] += Axis[i];
                }
            }
            return dic;
        }


    }
    public class chartdatabyDate
    {
        public DateTime date { get; set; }
        public TimeSpan time { get; set; }
        public double value { get; set; }

    }
}
