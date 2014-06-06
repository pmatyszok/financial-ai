using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using AI;
using AI.Functions;
using AI.Neurons;
using FinancialInstumentsAI.Controls;
using FinancialInstumentsAI.Dialogs;
using FinancialInstumentsAI.FinancialParser;

namespace FinancialInstumentsAI
{
    internal enum Initialization
    {
        Random,
        OptimalRange,
        Const
    };

    internal enum Activation
    {
        Sigmoid,
        BipolarSigmoid
    };
    
    public delegate double[] Indi(double[] data, int period, int predValueIndex);

    public partial class MainForm : Form
    {
        private IActivationFunction activ;
        private int eraCount;
        private INeuronInitilizer init;
        private List<int> layer;
        private Teacher learner;
        private double momentum;
        private Network network;
        private double rate;
        
        private bool teach100;
        private List<KeyValuePair<Indi, int>> indicator = new List<KeyValuePair<Indi, int>>();
       

        public MainForm()
        {
            InitializeComponent();
            oneValue.CheckOnClick = true;
            oneValue.CheckState = CheckState.Checked;

            //----------
            var newTabPage = new ChartTabPage("sin") {Name = "sin"};

            tcCharts.TabPages.Add(newTabPage);
            tcCharts.SelectTab(newTabPage);

            LoadExampleSinusData();

            //---------
        }

        public string FinancialFileSearchPattern { get; set; }

        private void LoadExampleSinusData()
        {
            double min, max;
            double[] data = ReadSinData(out min, out max);

            var chart = tcCharts.SelectedTab as ChartTabPage;
            if (chart == null) return;
            chart.Data = data;
            chart.Draw("sin");
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(AISettings.Instance.Visible == false)
                AISettings.Instance.Show(this);           
        }

        public void SetSettings()
        {
            activ = AISettings.Activ;
            init = AISettings.Init;
            layer = AISettings.Layer;
            network = new Network(layer[0], layer.Count, layer, activ, init);
            learner = new Teacher(network);
            rate = AISettings.LearnerRate;
            momentum = AISettings.LearnerMomentum;
            eraCount = AISettings.IterationsCount;
            indicator = AISettings.Indicator;
        }

        private void setSourceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog(this) == DialogResult.OK)
            {
                lbSourceList.Items.Clear();
                foreach (
                    string file in
                        Directory.GetFiles(folderBrowserDialog.SelectedPath, "*.*")
                        .Where(file => file.ToLower().EndsWith("mst") || file.ToLower().EndsWith("prn"))
                        .Select(Path.GetFileName))
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
                var selectTime = new SelectData();

                if (selectTime.ShowDialog() == DialogResult.OK)
                {
                    var chart = tcCharts.SelectedTab as ChartTabPage;
                    if (chart == null) return;

                    KeyValuePair<DateTime, double>[] data =
                        PrnFinancialParser.ParseFile(folderBrowserDialog.SelectedPath + "\\" +
                                                     selectedSource);

                    if ((data == null) || (data.Count() == 0))
                        return;

                    var selectedData = new Stack<KeyValuePair<DateTime, double>>();

                    foreach (var d in data.Where(d => (d.Key >= selectTime.DateFrom) && (d.Key <= selectTime.DateTo)))
                    {
                        selectedData.Push(d);
                    }

                    if (!selectedData.Any())
                    {
                        MessageBox.Show("Brak danych w podanym zakresie.");
                        return;
                    }

                    chart.FixedValues = selectedData.ToArray();
                    chart.UpdateFixedSeries(selectedSource);
                }
            }
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tcCharts.TabPages.Remove(tcCharts.SelectedTab);
        }

        private void Teach(bool full = true)
        {
            var chartTabPage = tcCharts.SelectedTab as ChartTabPage;
            if (chartTabPage == null) return;
            double[] data;
            if (full)
            {
                data = chartTabPage.Data;
            }
            else
            {
                int toTeach = (int)(chartTabPage.Data.Length * 0.7);
                data = new double[toTeach];
                Array.Copy(chartTabPage.Data, data, toTeach);
            }
            double min = data.Min();
            double max = data.Max();
            double range = (max - min);
            int learningSamples = data.Length - layer[0] + indicator.Count - 1;
            var input = new double[learningSamples][];
            var output = new double[learningSamples][];
            for (int i = 0; i < learningSamples; i++)
            {
                input[i] = new double[layer[0]];
                output[i] = new double[1];

                for (int j = 0; j < layer[0] - indicator.Count; j++)
                {
                    input[i][j] = TransformData(data[i + j], min, range);
                }
                output[i][0] = TransformData(data[i + layer[0]- indicator.Count], min, range);
            }
            double[][] indicatorValue = IndicatorsData(data.Length);
            for (int i = 0; i < learningSamples; i++)
            {
                for (int j = layer[0] - indicator.Count; j < layer[0]; j++)
                {
                    input[i][j] = TransformData(indicatorValue[j - layer[0] + indicator.Count][i + layer[0] - indicator.Count]
                        , indicatorValue[j - layer[0] + indicator.Count].Min()
                        , indicatorValue[j - layer[0] + indicator.Count].Max() - indicatorValue[j - layer[0] + indicator.Count].Min());
                }                
            }   
         
            learner.Rate = rate;
            learner.Momentum = momentum;  
            ProgressBar.Value = 0;
            ProgressBar.Minimum = 0;
            ProgressBar.Maximum = 100;
            ProgressBar.Step = 1;

            for (int i = 0; i < eraCount; i++)
            {
                var ins = new List<double[]>();
                var outs = new List<double[]>();

                for (int ii = 0; ii < input.Length; ii++)
                {
                    ins.Add(input[ii]);
                    outs.Add(output[ii]);
                }
                te.Text ="Teach error: " + learner.TeachOnSamples(ins, outs).ToString("F6");                
                if (i % (eraCount / 100) == 0)
                {
                    ProgressBar.PerformStep();
                }
            }
        }

        private void Predict(bool full= true,bool predictOneValue = true)
        {
            var chartTabPage = tcCharts.SelectedTab as ChartTabPage;
            if (chartTabPage == null) return;
            double[] data = chartTabPage.Data;
            int pred;
            try
            {
                pred = int.Parse(string.IsNullOrEmpty(toPred.Text) ? "0" : toPred.Text);
            }
            catch (FormatException)
            {
                pred = (int)(chartTabPage.Data.Length * 0.3);
            }
            if (predictOneValue)
            {
                if (full)
                {
                    if (pred > chartTabPage.Data.Length - layer[0])
                        pred = chartTabPage.Data.Length - layer[0];                    
                }
                else
                {
                    pred = (int)(chartTabPage.Data.Length * 0.3);
                    
                }
            }
            else
            {
                pred = (int)(chartTabPage.Data.Length * 0.3);               
            }
            predLabel.Text = "Predict: " + pred.ToString();

            double min = chartTabPage.Data.Min();
            double max = chartTabPage.Data.Max();
            double range = (max - min);

            var solution = new double[pred, 2];
            var netInput = new double[layer[0]];

            ProgressBar.Value = 0;
            ProgressBar.Minimum = 0;
            ProgressBar.Maximum = 100;
            ProgressBar.Step = 10;

            double[][] indicatorValue = IndicatorsData(data.Length);
            for (int j = 0; j < pred; j++)
            {
                for (int k = 0; k < layer[0]- indicator.Count; k++)
                {
                    netInput[k] = TransformData(data[j + data.Length - pred - layer[0] + k], min, range);
                }
                for (int l = layer[0] - indicator.Count; l < layer[0]; l++)
                {
                    netInput[l] = TransformData(indicatorValue[l - layer[0] + indicator.Count][j + data.Length - pred]
                        , indicatorValue[l - layer[0] + indicator.Count].Min()
                        , indicatorValue[l - layer[0] + indicator.Count].Max() - indicatorValue[l - layer[0] + indicator.Count].Min());
                }  
                solution[j, 1] = TransformBack(network.ComputeOutputVector(netInput)[0], min, max);
                if (!predictOneValue)
                {
                    data[data.Length - pred + j] = solution[j, 1];
                }
                if (j % (pred / 10) == 0)
                {
                    ProgressBar.PerformStep();
                }
            }
            var aproximated = new double[pred];
            Console.WriteLine("Try those Values in Excel or whatever");
            using (var writer = new StreamWriter("data_dump_time_series.txt"))
            {
                for (int i = 0; i < aproximated.Length; i++)
                {
                    aproximated[i] = solution[i, 1];
                    writer.WriteLine((aproximated[i]).ToString());
                }
            }
            predErrorLabel.Text = "Predict value RMS error: " + RMS(data, aproximated).ToString("F6");
            ChartTabPage chart = chartTabPage;
            chart.DrawPred("predicted", aproximated);
        }  

        private double TransformData(double input, double min, double range)
        {
            //scale to -1..1
            return (((input - min) * 2.0) / range) - 1.0;
        }

        private double TransformBack(double input, double min, double max)
        {
            //scale back to original data from -1..1
            return ((input + 1) * (max - min) / 2.0) + min;
        }

        private void runButton_Click(object sender, EventArgs e)
        {
            if (network != null)
            {
                Teach(false);
                teach100 = false;
            }
        }

        private void run100ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (network != null)
            {
                Teach();
                teach100 = true;
            }
        }      

        private void runToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (network != null)
            {
                bool one = oneValue.CheckState == CheckState.Checked;
                Predict(teach100, one);
            }
        }

        private double [][] IndicatorsData(int count)
        {
            double[][] toReturn = new double[indicator.Count][];
            var chartTabPage = tcCharts.SelectedTab as ChartTabPage;
            if (chartTabPage == null) return null;
            for (int i = 0; i < indicator.Count; i++)
            {
                toReturn[i] = indicator[i].Key(chartTabPage.Data,indicator[i].Value,count);
            }
            return toReturn;
        }

        private double RMS(double[] data, double[] pedictData)
        {
            double error = pedictData.Select((t, i) => Math.Pow(t - data[data.Length - pedictData.Length + i], 2)).Sum();
            error /= pedictData.Length;
            error = Math.Sqrt(error);
            return error;
        }
        //------------------------------------
        //for sinus       
        private static double[] ReadSinData(out double min, out double max)
        {
            var readedData = new double[50];

            using (var reader = new StreamReader(File.OpenRead("sinusoid.csv")))
            {
                int i = 0;
                string line = reader.ReadLine();
                if (line == null)
                {
                    min = max = 0;
                    return null;
                }
                readedData[i] = double.Parse(line, CultureInfo.InvariantCulture);
                min = readedData[i];
                max = readedData[i];

                try
                {
                    for (i = 1; i < 50; i++)
                    {
                        line = reader.ReadLine();
                        if (line == null) break;
                        readedData[i] = double.Parse(line, CultureInfo.InvariantCulture);
                        if (min > readedData[i])
                            min = readedData[i];
                        if (max < readedData[i])
                            max = readedData[i];
                    }
                }
                catch (Exception)
                {
                }
                if (i > 0)
                {
                    var data = new double[i];
                    Array.Copy(readedData, data, i);
                    return data;
                }
                return null;
            }
        }

        private void TeachSinus()
        {
            double min, max;
            double[] data = ReadSinData(out min, out max);
            min = data.Min();
            max = data.Max();
            double range = (max - min);
            int learningSamples = data.Length - layer[0] - 1;

            var input = new double[learningSamples][];
            var output = new double[learningSamples][];


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

            for (int i = 0; i < eraCount; i++)
            {
                var ins = new List<double[]>();
                var outs = new List<double[]>();

                for (int ii = 0; ii < input.Length; ii++)
                {
                    ins.Add(input[ii]);
                    outs.Add(output[ii]);
                }
                te.Text = "Teach error: " + learner.TeachOnSamples(ins, outs).ToString("F6");
            }
        }

        private void PredictSinus()
        {
            double min, max;
            double[] data = ReadSinData(out min, out max);
            min = data.Min();
            max = data.Max();
            double range = (max - min);

            var solution = new double[data.Length - layer[0], 2];
            var netInput = new double[layer[0]];


            for (int j = 0; j < data.Length - layer[0]; j++)
            {
                for (int k = 0; k < layer[0]; k++)
                {
                    netInput[k] = TransformData(data[j + k], min, range);
                }
                solution[j, 1] = TransformBack(network.ComputeOutputVector(netInput)[0], min, max);
                //(network.ComputeOutputVector(netInput)[0]) / range + min;
            }
            var aproximated = new double[data.Length - layer[0]];
            Console.WriteLine("Try those Values in Excel or whatever");
            using (var writer = new StreamWriter("data_dump_time_series.txt"))
            {
                for (int i = 0; i < aproximated.Length; i++)
                {
                    aproximated[i] = solution[i, 1];
                    writer.WriteLine((aproximated[i]).ToString());
                }
            }


            tcCharts.SelectedIndex = 0;
            var chart = tcCharts.SelectedTab as ChartTabPage;
            if (chart == null) return;

            var predData = new double[layer[0] + aproximated.Length];
            for (int i = 0; i < layer[0]; i++)
            {
                predData[i] = data[i];
            }
            Array.Copy(aproximated, 0, predData, layer[0], aproximated.Length);
            chart.DrawPred("pred sin", predData);
        }

        private void runSinToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (network != null)
            {               
                PredictSinus();
            }
        }

        private void runSinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (network != null)
            {
                TeachSinus();
            }
        }     
        //-------------------------------
    }
}