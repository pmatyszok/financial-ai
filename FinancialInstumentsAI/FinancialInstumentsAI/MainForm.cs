using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FinancialInstumentsAI.Controls;
using FinancialInstumentsAI.Dialogs;

namespace FinancialInstumentsAI
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            foreach (TabPage theTabPage in tabControl1.TabPages)
            {
                var currentUserControl = new ChartControl();

                theTabPage.Margin = new Padding(0);
                theTabPage.Padding = new Padding(0);

                theTabPage.Controls.Add(currentUserControl);

                currentUserControl.Location = new Point(0, 0);

                currentUserControl.Dock = DockStyle.Fill;

                currentUserControl.SendToBack();
            }
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var settings = new AISettings())
            {
                if (settings.ShowDialog(this) == DialogResult.OK)
                {
                    //...
                }
            }
        }
    }
}
