using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace FinancialInstumentsAI.Controls
{
    public partial class ChartTabPage : TabPage
    {
        public ChartTabPage(string text = null)
            : base(text)
        {
            InitializeComponent();

            Margin = new Padding(0);
            Padding = new Padding(0);

            Controls.Add(chartControl);

            chartControl.Location = new Point(0, 0);

            chartControl.Dock = DockStyle.Fill;

            chartControl.SendToBack();
        }

        public double[] Data { get; set; }

        public KeyValuePair<DateTime, double>[] FixedValues { get; set; }
        public KeyValuePair<DateTime, double>[] PredictedValues { get; set; }

        public void UpdateFixedSeries(string name)
        {
            chartControl.FixedSeries.Name = name;
            chartControl.FixedSeries.Enabled = true;

            chartControl.FixedSeries.Points.Clear();

            foreach (var value in FixedValues)
            {
                chartControl.FixedSeries.Points.AddXY(value.Key, value.Value);
            }
        }

        public void UpdatePredictedSeries(string name)
        {
            chartControl.PredictedSeries.Name = name;
            chartControl.PredictedSeries.Enabled = true;

            chartControl.PredictedSeries.Points.Clear();

            foreach (var value in PredictedValues)
            {
                chartControl.PredictedSeries.Points.AddXY(value.Key, value.Value);
            }
        }

        [Obsolete("Draw is used only for draw sinus, please use UpdateFixedSeries instead.")]
        public void Draw(string name)
        {
            Series seria = chartControl.chart.Series.Add(name);
            seria.ChartType = SeriesChartType.Line;
            foreach (double elem in Data)
            {
                seria.Points.Add(elem);
            }
        }

        [Obsolete("Draw is used only for draw sinus predictions, please use UpdatePredictedSeries instead.")]
        public void DrawPred(string name, double[] points)
        {
            if (chartControl.chart.Series.Count > 1)
                chartControl.chart.Series.RemoveAt(1);
            Series seria = chartControl.chart.Series.Add(name);
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