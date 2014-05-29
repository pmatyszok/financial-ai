using System;
using System.Windows.Forms;

namespace FinancialInstumentsAI.Dialogs
{
    public partial class NeuronCounts : Form
    {
        public int Value { get; set; }
        public NeuronCounts()
        {
            InitializeComponent();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            Value = (int)countNumeric.Value;
            DialogResult = DialogResult.OK;
        }
    }
}
