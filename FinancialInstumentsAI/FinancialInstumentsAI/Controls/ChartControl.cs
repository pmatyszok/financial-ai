using System;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace FinancialInstumentsAI.Controls
{
    public partial class ChartControl : UserControl
    {
        public readonly Series FixedSeries;
        public readonly Series PredictedSeries;

        public ChartControl()
        {
            InitializeComponent();
            FixedSeries = new Series();
            PredictedSeries = new Series();

            FixedSeries.Enabled = PredictedSeries.Enabled = false;

            FixedSeries.XValueType = PredictedSeries.XValueType = ChartValueType.DateTime;
            FixedSeries.ChartType = PredictedSeries.ChartType = SeriesChartType.Line;

            chart.Series.Add(FixedSeries);
            chart.Series.Add(PredictedSeries);
        }

        private void cbFixedSeries_CheckedChanged(object sender, EventArgs e)
        {
            FixedSeries.Enabled = cbFixedSeries.Checked && FixedSeries.Points.Any();
        }

        private void cbPredictedSeries_CheckedChanged(object sender, EventArgs e)
        {
            PredictedSeries.Enabled = cbPredictedSeries.Checked && PredictedSeries.Points.Any();
        }
    }
}