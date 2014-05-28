using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinancialInstumentsAI.Dialogs
{
    public partial class neuronCounts : Form
    {
        public int value { get; set; }
        public neuronCounts()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            value=(int)countNumeric.Value;
            DialogResult = DialogResult.OK;
        }
    }
}
