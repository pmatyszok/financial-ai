using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace FinancialInstumentsAI.Controls
{
    public partial class ChartControl : UserControl
    {
        public Chart _chart { get { return this.chart; } private set { } }

        public ChartControl()
        {
            InitializeComponent();
        }
    }
}
