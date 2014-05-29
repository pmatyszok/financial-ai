using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace FinancialInstumentsAI.Controls
{
    public partial class ChartTabPage : TabPage
    {
        public double[] Data { get; set; }
        public ChartTabPage(string text = null) : base(text)
        {
            InitializeComponent();

            Margin = new Padding(0);
            Padding = new Padding(0);

            Controls.Add(chartControl);

            chartControl.Location = new Point(0, 0);

            chartControl.Dock = DockStyle.Fill;

            chartControl.SendToBack();
        }
        
        public void Draw(string name)
        {
            Series seria = chartControl._chart.Series.Add(name);
            seria.ChartType = SeriesChartType.Line;
            foreach (double elem in Data)
            {
                seria.Points.Add(elem);
            }
        }

        public void DrawPred(string name, double[] points)
        {
            if (chartControl._chart.Series.Count > 1)
                chartControl._chart.Series.RemoveAt(1);
            Series seria = chartControl._chart.Series.Add(name);
            seria.ChartType = SeriesChartType.Line;
            var toAdd = new double[Data.Length];
            Array.Copy(Data, 0, toAdd, 0, Data.Length - points.Length);
            Array.Copy(points, 0, toAdd, Data.Length - points.Length, points.Length);
            foreach (double elem in toAdd)
            {
                seria.Points.Add(elem);
            }
        }
    }
}