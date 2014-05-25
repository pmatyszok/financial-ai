using System.Windows.Forms;

namespace FinancialInstumentsAI.Dialogs
{
    public partial class AISettings : Form
    {
        public AISettings()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.OK;;
        }
    }
}
