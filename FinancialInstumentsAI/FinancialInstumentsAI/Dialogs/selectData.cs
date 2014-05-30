using System;
using System.Windows.Forms;

namespace FinancialInstumentsAI.Dialogs
{
    public partial class SelectData : Form
    {
        public SelectData()
        {
            InitializeComponent();
        }

        public DateTime DateFrom { get; private set; }
        public DateTime DateTo { get; private set; }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (dateTimePicker1.Value.Date >= dateTimePicker2.Value.Date) return;

            DateFrom = dateTimePicker1.Value.Date;
            DateTo = dateTimePicker2.Value.Date;

            DialogResult = DialogResult.OK;
        }
    }
}