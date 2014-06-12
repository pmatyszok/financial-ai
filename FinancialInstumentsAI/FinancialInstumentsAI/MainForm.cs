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
        private List<KeyValuePair<Indi, int>> indicator = new List<KeyValuePair<Indi, int>>();
        private INeuronInitilizer init;
        private List<int> layer;
        private Teacher learner;
        private double momentum;
        private Network network;
        private double rate;

        private bool teach100;


        public MainForm()
        {
            InitializeComponent();
            oneValue.CheckOnClick = true;
            oneValue.CheckState = CheckState.Checked;
            
        }

        public string FinancialFileSearchPattern { get; set; }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (AISettings.Instance.Visible == false)
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
                    KeyValuePair<DateTime, double>[] data;
                    if (!(Path.GetExtension(selectedSource) == ".mst"))
                    {
                         data = PrnFinancialParser.ParseFile(folderBrowserDialog.SelectedPath + "\\" +
                                                         selectedSource);
                    }
                    else
                    {
                        data = MstFinancialParser.ParseFile(folderBrowserDialog.SelectedPath + "\\" +
                                                                                 selectedSource);
                    }

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
                    valueCount.Text = "Value count: " + chart.FixedValues.Length;
                }
            }
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tcCharts.TabPages.Remove(tcCharts.SelectedTab);
        }

        public double Teach(bool full = true)
        {
            var chartTabPage = tcCharts.SelectedTab as ChartTabPage;
            if ((chartTabPage == null) || (chartTabPage.FixedValues == null)) return -1;
            int toTeach = chartTabPage.FixedValues.Length;
            if (!full) toTeach = (int)(toTeach * 0.7);

            var data = new double[toTeach];
            for (int i = 0; i < data.Count(); i++)
                data[i] = chartTabPage.FixedValues[i].Value;

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
                output[i][0] = TransformData(data[i + layer[0] - indicator.Count], min, range);
            }
            double[][] indicatorValue = IndicatorsData(data,data.Length);
            for (int i = 0; i < learningSamples; i++)
            {
                for (int j = layer[0] - indicator.Count; j < layer[0]; j++)
                {
                    input[i][j] =
                        TransformData(indicatorValue[j - layer[0] + indicator.Count][i + layer[0] - indicator.Count-1]
                            , indicatorValue[j - layer[0] + indicator.Count].Min()
                            ,
                            indicatorValue[j - layer[0] + indicator.Count].Max() -
                            indicatorValue[j - layer[0] + indicator.Count].Min());
                }
            }

            learner.Rate = rate;
            learner.Momentum = momentum;
            ProgressBar.Value = 0;
            ProgressBar.Minimum = 0;
            ProgressBar.Maximum = 100;
            ProgressBar.Step = 1;

            double error = 0;
            for (int i = 0; i < eraCount; i++)
            {
                var ins = new List<double[]>();
                var outs = new List<double[]>();

                for (int ii = 0; ii < input.Length; ii++)
                {
                    ins.Add(input[ii]);
                    outs.Add(output[ii]);
                }
                error = learner.TeachOnSamples(ins, outs);
                if ((eraCount / 100) > 0 && i % (eraCount / 100) == 0)
                {
                    ProgressBar.PerformStep();
                }
            }
            te.Text = "Teach error: " + error.ToString("F6");
            return error;
        }

        public double Predict(bool full = true, bool predictOneValue = true,int pred = 0)
        {
            var chartTabPage = tcCharts.SelectedTab as ChartTabPage;
            if ((chartTabPage == null) || (chartTabPage.FixedValues == null)) return -1;

            var data = new double[chartTabPage.FixedValues.Length];
            for (int i = 0; i < data.Count(); i++)
                data[i] = chartTabPage.FixedValues[i].Value;
            
            if (pred == 0)
            {
                try
                {
                    pred = int.Parse(string.IsNullOrEmpty(toPred.Text) ? "0" : toPred.Text);
                }
                catch (FormatException)
                {
                    pred = (int)(chartTabPage.FixedValues.Length * 0.3);
                }
                if (predictOneValue)
                {
                    if (full)
                    {
                        if (pred > chartTabPage.FixedValues.Length - layer[0])
                            pred = chartTabPage.FixedValues.Length - layer[0];
                    }
                    else
                    {
                        int maxi = (int)(chartTabPage.FixedValues.Length * 0.3);
                        if (pred > maxi || pred == 0)
                            pred = maxi;
                    }
                }
                else
                {
                    int maxi = (int)(chartTabPage.FixedValues.Length * 0.3);
                    if (pred > maxi || pred == 0)
                        pred = maxi;
                }
            }
            predLabel.Text = "Predict: " + pred;

            double min = chartTabPage.FixedValues.Min(pair => pair.Value);
            double max = chartTabPage.FixedValues.Max(pair => pair.Value);
            double range = (max - min);

            var solution = new double[pred, 2];
            var netInput = new double[layer[0]];

            ProgressBar.Value = 0;
            ProgressBar.Minimum = 0;
            ProgressBar.Maximum = 100;
            ProgressBar.Step = 10;

            double[][] indicatorValue = IndicatorsData(data,data.Length);
            for (int j = 0; j < pred; j++)
            {
                for (int k = 0; k < layer[0] - indicator.Count; k++)
                {
                    netInput[k] = TransformData(data[j + data.Length - pred - layer[0] + k], min, range);
                }
                for (int l = layer[0] - indicator.Count; l < layer[0]; l++)
                {
                    netInput[l] = TransformData(indicatorValue[l - layer[0] + indicator.Count][j + data.Length - pred-1]
                        , indicatorValue[l - layer[0] + indicator.Count].Min()
                        ,
                        indicatorValue[l - layer[0] + indicator.Count].Max() -
                        indicatorValue[l - layer[0] + indicator.Count].Min());
                }
                solution[j, 1] = TransformBack(network.ComputeOutputVector(netInput)[0], min, max);
                if (!predictOneValue)
                {
                    data[data.Length - pred + j] = solution[j, 1];
                    indicatorValue = IndicatorsData(data, data.Length);
                }
                if (pred >10 && j % (pred / 10) == 0)
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
            double error = RMS(chartTabPage.FixedValues, aproximated);
            predErrorLabel.Text = "Predict value RMS error: " + error.ToString("F6");
            ChartTabPage chart = chartTabPage;
            chart.PredictedValues = new KeyValuePair<DateTime, double>[chart.FixedValues.Length];

            int predStartIndex = chart.FixedValues.Length - aproximated.Length;

            Array.Copy(chart.FixedValues, chart.PredictedValues, predStartIndex);

            for (int i = 0; i < aproximated.Length; i++)
            {
                chart.PredictedValues[predStartIndex + i] =
                    new KeyValuePair<DateTime, double>(chart.FixedValues[predStartIndex + i].Key, aproximated[i]);
            }

            chart.UpdatePredictedSeries("predicted");

            return error;
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

        private double[][] IndicatorsData(double[] data,int count)
        {
            var toReturn = new double[indicator.Count][];            
            for (int i = 0; i < indicator.Count; i++)
            {
                toReturn[i] = indicator[i].Key(data, indicator[i].Value, count);
            }
            return toReturn;
        }

        private double RMS(KeyValuePair<DateTime,double>[] values, double[] pedictData)
        {
            var data = new double[values.Length];
            for (int i = 0; i < data.Count(); i++)
                data[i] = values[i].Value;
            double error = pedictData.Select((t, i) => Math.Pow(t - data[data.Length - pedictData.Length + i], 2)).Sum();
            error /= pedictData.Length;
            error = Math.Sqrt(error);
            return error;
        }      
    }
}