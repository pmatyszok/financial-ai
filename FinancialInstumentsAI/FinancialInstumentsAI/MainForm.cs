using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using FinancialInstumentsAI.Controls;
using FinancialInstumentsAI.Dialogs;
using AI;
using AI.Neurons;
using AI.Functions;
using System.Collections.Generic;

namespace FinancialInstumentsAI
{
    enum Initialization {Random , OptimalRange, Const };
    enum Activation { Sigmoid, Bipolar };

    public partial class MainForm : Form
    {
        private Network network;
        private List<int> layer;
        private INeuronInitilizer init;
        private IActivationFunction activ;
        private Teacher learner;
        private double rate, momentum;
        private int eraCount;

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
                activ = AISettings.activ;
                init = AISettings.init;
                layer = AISettings.layer;
                network = new Network(layer[0], layer.Count , layer, activ,init);
                learner = new Teacher(network);
                rate = AISettings.learnerRate;
                momentum = AISettings.learnerMomentum;                
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

        private void teach()
        {
            double[] readedData = new double[50];
            double[] data;
            double min, max;
            double range;
            using (var reader = new StreamReader(File.OpenRead("sinusoid.csv")))
            {
                int i = 0;
                readedData[i] = double.Parse(reader.ReadLine(), System.Globalization.CultureInfo.InvariantCulture);
                min = readedData[i];
                max = readedData[i];

                try
                {
                    for (i = 1; i < 50; i++)
                    {
                        readedData[i] = double.Parse(reader.ReadLine(), System.Globalization.CultureInfo.InvariantCulture);
                        if (min > readedData[i])
                            min = readedData[i];
                        if (max < readedData[i])
                            max = readedData[i];
                    }
                }
                catch (Exception ex)
                {
                    ;
                }
                if (i > 0)
                {
                    data = new double[i];
                    Array.Copy(readedData, data, i);
                }
                else return;
            }
            range = (max - min);
            int learningSamples = data.Length - layer[0] - 1;

            double[][] input = new double[learningSamples][];
            double[][] output = new double[learningSamples][];



            for (int i = 0; i < learningSamples; i++)
            {
                input[i] = new double[layer[0]];
                output[i] = new double[1];

                for (int j = 0; j < layer[0]; j++)
                {
                    input[i][j] = TransformData(data[i + j], min, range);
                }
                output[i][0] = TransformData(data[i + layer[0]], min, range);
            }
            //double[] input = new double[] { 1,2,3,4,5,6,7,8,9 };
            //double[] output = new double[]{ 0,4,7,9,10,9,7,4,0 };
            // learner = new Teacher(network);
            
            learner.Rate = rate;
            learner.Momentum = momentum;
            var solution = new double[data.Length - layer[0], 2];
            var netInput = new double[layer[0]];

            for (int i = 0; i < eraCount; i++)
            {
                var ins = new List<double[]>();
                var outs = new List<double[]>();

                for (int ii = 0; ii < input.Length; ii++)
                {
                    ins.Add(input[ii]);
                    outs.Add(output[ii]);
                }
                learner.TeachOnSamples(ins, outs);               
            }            
        }

        private void predict()
        {
            double[] readedData = new double[50];
            double[] data;
            double min, max;
            double range;

            using (var reader = new StreamReader(File.OpenRead("sinusoid.csv")))
            {
                int i = 0;
                readedData[i] = double.Parse(reader.ReadLine(), System.Globalization.CultureInfo.InvariantCulture);
                min = readedData[i];
                max = readedData[i];

                try
                {
                    for (i = 1; i < 50; i++)
                    {
                        readedData[i] = double.Parse(reader.ReadLine(), System.Globalization.CultureInfo.InvariantCulture);
                        if (min > readedData[i])
                            min = readedData[i];
                        if (max < readedData[i])
                            max = readedData[i];
                    }
                }
                catch (Exception ex)
                {
                    ;
                }
                if (i > 0)
                {
                    data = new double[i];
                    Array.Copy(readedData, data, i);
                }
                else return;
            }
            range = (max - min);

            var solution = new double[data.Length - layer[0], 2];
            var netInput = new double[layer[0]];


            for (int j = 0; j < data.Length - layer[0]; j++)
            {
                for (int k = 0; k < layer[0]; k++)
                {
                    netInput[k] = TransformData(data[j + k], min, range);
                }
                solution[j, 1] = TransformBack(network.ComputeOutputVector(netInput)[0], min, max);//(network.ComputeOutputVector(netInput)[0]) / range + min;
            }
            double[] aproximated = new double[data.Length - layer[0]];
            Console.WriteLine("Try those Values in Excel or whatever");
            using (var writer = new StreamWriter("data_dump_time_series.txt"))
            {
                for (int i = 0; i < aproximated.Length; i++)
                {
                    aproximated[i] = solution[i, 1];
                    writer.WriteLine((aproximated[i]).ToString());
                }
            }
        }

        private double TransformData(double input, double min, double range)
        { //scale to -1..1
            return (((input - min) * 2.0) / range) - 1.0;
        }

        private double TransformBack(double input, double min, double max)
        { //scale back to original data from -1..1
            return ((input + 1) * (max - min) / 2.0) + min;
        }         

        private void runButton_Click(object sender, EventArgs e)
        {
            if (network != null)
            {
                teach();
            }
        }
        
        private void eraCountText_TextChanged(object sender, EventArgs e)
        {
            eraCount = int.Parse(eraCountText.Text);
        }

        private void runToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (network != null)
            {
                predict();
            }
        }

    }
}