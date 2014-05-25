using System.Drawing;
using System.Windows.Forms;

namespace FinancialInstumentsAI.Controls
{
    public partial class ChartTabPage : TabPage
    {
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
    }
}