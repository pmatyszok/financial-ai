using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace FinancialInstumentsAI.Controls
{
    public partial class ChartTabPage : TabPage
    {
        public double[] data { get; set; }
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

        public void draw(string name, double[] data)
        {
            if(chartControl._chart.Series.Count >1)
                chartControl._chart.Series.RemoveAt(1);
            Series seria = chartControl._chart.Series.Add(name);
            seria.ChartType = SeriesChartType.Line;
            foreach (double elem in data)
            {
                seria.Points.Add(elem);                               
            }
        }
    }
}