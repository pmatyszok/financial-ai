using System;
using System.IO;
using System.Linq;
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

            FinancialFileSearchPattern = "*.mst";
        }

        public string FinancialFileSearchPattern { get; set; }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (AISettings.Instance.ShowDialog(this) == DialogResult.OK)
            {
                //...
            }
        }

        private void setSourceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog(this) == DialogResult.OK)
            {
                lbSourceList.Items.Clear();
                foreach (
                    string file in
                        Directory.GetFiles(folderBrowserDialog.SelectedPath, FinancialFileSearchPattern)
                            .Select(Path.GetFileNameWithoutExtension))
                {
                    lbSourceList.Items.Add(file);
                }
            }
        }

        private void lbSourceList_DoubleClick(object sender, EventArgs e)
        {
            if (lbSourceList.SelectedItem == null) return;
            string selectedSource = lbSourceList.SelectedItem.ToString();

            if (tcCharts.TabPages.ContainsKey(selectedSource))
            {
                tcCharts.SelectTab(selectedSource);
            }
            else
            {
                var newTabPage = new ChartTabPage(selectedSource) {Name = selectedSource};

                tcCharts.TabPages.Add(newTabPage);
                tcCharts.SelectTab(newTabPage);
            }
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tcCharts.TabPages.Remove(tcCharts.SelectedTab);
        }

    }
}