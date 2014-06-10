using System;
using System.Collections.Generic;
using System.Windows.Forms;
using AI.Functions;
using AI.Neurons;
using System.IO;

namespace FinancialInstumentsAI.Dialogs
{
    public sealed partial class AISettings : Form
    {
        private static AISettings instance;

        public static List<int> Layer { get; set; }
        public static INeuronInitilizer Init { get; set; }
        public static IActivationFunction Activ { get; set; }
        public static double LearnerRate { get; set; }
        public static double LearnerMomentum { get; set; }
        public static int IterationsCount { get; set; }
        public static List<KeyValuePair<Indi, int>> Indicator { get; set; }

        private AISettings()
        {
            InitializeComponent();
            initFuncComboBox.DataSource = Enum.GetNames(typeof(Initialization));
            activFuncComboBox.DataSource = Enum.GetNames(typeof(Activation));
            constValueTextBox.Enabled = false;
            Indicator = new List<KeyValuePair<Indi, int>>();
        }
        
        public static AISettings Instance
        {
            get { return instance ?? (instance = new AISettings()); }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            AssignValue();
            var a = this.Owner as MainForm;
            if (a != null)
                a.SetSettings();
            this.Hide();
        }

        private void apply_Click(object sender, EventArgs e)
        {
            AssignValue();
            var a = this.Owner as MainForm;
            if (a != null)
                a.SetSettings();
        }

        private void initFuncComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {            
            if (initFuncComboBox.SelectedIndex == 1)
            {
                activFuncComboBox.SelectedIndex = 1;
                activFuncComboBox.Enabled = false;
            }
            else
            {
                activFuncComboBox.Enabled = true;
            }
            if (initFuncComboBox.SelectedIndex == 2)
            {
                constValueTextBox.Enabled = true;
            }
            else
            {
                constValueTextBox.Enabled = false;
            }
        }        

        private void AssignValue()
        {           
            switch (initFuncComboBox.SelectedIndex)
            {
                case (0):
                    Init = new RandomInitializer();
                    break;
                case (1):
                    Init = new OptimalRangeRandomInitializer(Activ = new BipolarSigmoid((double)alphaNumeric.Value));
                    break;
                case (2):
                    Init = new ConstInitializer(double.Parse(constValueTextBox.Text));
                    break;
                default:
                    Init = null;
                    break;
            }

            if (activFuncComboBox.SelectedIndex == 1)
            {
                Activ = new BipolarSigmoid((double)alphaNumeric.Value);
            }
            else
            {
                Activ = new Sigmoid((double)alphaNumeric.Value);
            }

            LearnerRate = (double)rateNumeric.Value;
            LearnerMomentum = (double)momentumNumeric.Value;


            int firstLayer = (int)windowSize.Value;
            firstLayer += AddIndicators();

            Layer = new List<int> { firstLayer };
            if (layerCountCheckBox.Checked)
            {
                for (int i = 0; i < (int)layersNumeric.Value - 2; i++)
                {
                    Layer.Add((int)windowSize.Value * 2);
                }
            }
            else
            {
                for (int i = 0; i < (int)layersNumeric.Value - 2; i++)
                {
                    var neuron = new NeuronCounts { Text = "Layer no. " + i + " count" };
                    DialogResult res = neuron.ShowDialog(this);
                    if (res == DialogResult.OK)
                    {
                        Layer.Add(neuron.Value);
                    }
                }
            }

            Layer.Add(1);

            int iterations;
            if (!int.TryParse(iterationsTextBox.Text, out iterations))
            {
                iterations = 1000;
            }
            IterationsCount = iterations;
        }

        private int AddIndicators()
        {
            int toAddForFirstLayer = 0;
            Indicator.Clear();
            if (sma.CheckState == CheckState.Checked)
            {                 
                Indicator.Add(new KeyValuePair<Indi, int>(Indicators.Indicators.SMA, (int)smaPeriod.Value));
                toAddForFirstLayer++;
            }

            if (wma.CheckState == CheckState.Checked)
            {
                Indicator.Add(new KeyValuePair<Indi, int>(Indicators.Indicators.WMA, (int)wmaPeriod.Value));
                toAddForFirstLayer++;
            }

            if (ema.CheckState == CheckState.Checked)
            {
                Indicator.Add(new KeyValuePair<Indi, int>(Indicators.Indicators.EMA, (int)emaPeriod.Value));
                toAddForFirstLayer++;
            }

            if (roc.CheckState == CheckState.Checked)
            {
                Indicator.Add(new KeyValuePair<Indi, int>(Indicators.Indicators.ROC, (int)rocPeriod.Value));
                toAddForFirstLayer++;
            }

            if (macd.CheckState == CheckState.Checked)
            {
                Indicator.Add(new KeyValuePair<Indi, int>(Indicators.Indicators.MACD, (int)macdPeriod.Value));
                toAddForFirstLayer++;
            }

            if (oscill.CheckState == CheckState.Checked)
            {
                Indicator.Add(new KeyValuePair<Indi, int>(Indicators.Indicators.Oscillator, (int)oscillValue.Value));
                toAddForFirstLayer++;
            }

            return toAddForFirstLayer;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void tests()
        {
            Init = new OptimalRangeRandomInitializer(Activ = new BipolarSigmoid(2.0));
            //Activ = new BipolarSigmoid((double)2.0);
            LearnerRate = 0.3;
            LearnerMomentum = 0.0;
            IterationsCount = 500;
            
            using (var writer = new StreamWriter("2hidelayer.txt"))
            {
                string column = "window|hide layer 1|hide layer 2|teach error|predic error 30%|10 values|5 |1|predic error 30%|10 values|5 |1";
                writer.WriteLine(column);
                for (int j = 2; j < 10; j++)
                {
                    for (int i = 1; i < 21; i++)
                    {
                        for (int k = 1; k < 21; k++)
                        {
                            string toFile = j + "|" + i + "|" + k + "|";
                            Layer = new List<int> { j };
                            Layer.Add(i);
                            Layer.Add(k);
                            Layer.Add(1);
                            var a = this.Owner as MainForm;
                            if (a != null)
                            {
                                a.SetSettings();
                                toFile += a.Teach(false).ToString("F6") + "|";
                                toFile += a.Predict(false, true).ToString("F6") + "|";
                                toFile += a.Predict(false, true, 10).ToString("F6") + "|";
                                toFile += a.Predict(false, true, 5).ToString("F6") + "|";
                                toFile += a.Predict(false, true, 1).ToString("F6") + "|";
                                toFile += a.Predict(false, false).ToString("F6") + "|";
                                toFile += a.Predict(false, false, 10).ToString("F6") + "|";
                                toFile += a.Predict(false, false, 5).ToString("F6") + "|";
                                toFile += a.Predict(false, false, 1).ToString("F6");
                            }
                            writer.WriteLine(toFile);
                        }
                    }
                }
            }

            using (var writer = new StreamWriter("1hidelayer.txt"))
            {
                string column = "window|hide layer 1|teach error|predic error 30%|10 values|5 |1|predic error 30%|10 values|5 |1";
                writer.WriteLine(column);
                for (int j = 2; j < 10; j++)
                {
                    for (int i = 1; i < 21; i++)
                    {
                       
                            string toFile = j + "|" + i + "|";
                            Layer = new List<int> { j };
                            Layer.Add(i);                           
                            Layer.Add(1);
                            var a = this.Owner as MainForm;
                            if (a != null)
                            {
                                a.SetSettings();
                                toFile += a.Teach(false).ToString("F6") + "|";
                                toFile += a.Predict(false, true).ToString("F6") + "|";
                                toFile += a.Predict(false, true, 10).ToString("F6") + "|";
                                toFile += a.Predict(false, true, 5).ToString("F6") + "|";
                                toFile += a.Predict(false, true, 1).ToString("F6") + "|";
                                toFile += a.Predict(false, false).ToString("F6") + "|";
                                toFile += a.Predict(false, false, 10).ToString("F6") + "|";
                                toFile += a.Predict(false, false, 5).ToString("F6") + "|";
                                toFile += a.Predict(false, false, 1).ToString("F6");
                            }
                            writer.WriteLine(toFile);
                        
                    }
                }
            }

            using (var writer = new StreamWriter("wskazniki.txt"))
            {
                Init = new OptimalRangeRandomInitializer(Activ = new BipolarSigmoid(2.0));
                ////Activ = new BipolarSigmoid((double)2.0);
                LearnerRate = 0.3;
                LearnerMomentum = 0.0;
                IterationsCount = 500;
                writer.WriteLine("indicators|argument|teach error|predic error 30%|10 values|5 |1|predic error 30%|10 values|5 |1");
                
                    for (int j = 0; j < 6; j++)
                    {
                        for (int i = 0; i < 30; i++)
                        {
                        string toFile = "";
                        switch (j)
                        {
                            case 0:
                                Indicator.Clear();
                                Indicator.Add(new KeyValuePair<Indi, int>(Indicators.Indicators.SMA, i));
                                toFile += "sma|";
                                break;
                            case 1:
                                Indicator.Clear();
                                Indicator.Add(new KeyValuePair<Indi, int>(Indicators.Indicators.WMA, i));
                                toFile += "wma|";
                                break;
                            case 2:
                                Indicator.Clear();
                                Indicator.Add(new KeyValuePair<Indi, int>(Indicators.Indicators.EMA, i));
                                toFile += "ema|";
                                break;
                            case 3:
                                Indicator.Clear();
                                Indicator.Add(new KeyValuePair<Indi, int>(Indicators.Indicators.ROC, i));
                                toFile += "roc|";
                                break;
                            case 4:
                                Indicator.Clear();
                                Indicator.Add(new KeyValuePair<Indi, int>(Indicators.Indicators.MACD, i));
                                toFile += "macd|";
                                break;
                            case 5:
                                Indicator.Clear();
                                Indicator.Add(new KeyValuePair<Indi, int>(Indicators.Indicators.Oscillator, 3 + i));
                                toFile += "oscil|";
                                break;
                        }
                        toFile += i + "|";
                        Layer = new List<int> { 4 };
                        Layer.Add(12);
                        Layer.Add(1);
                        var a = this.Owner as MainForm;
                        if (a != null)
                        {
                            a.SetSettings();
                            toFile += a.Teach(false).ToString("F6") + "|";
                            toFile += a.Predict(false, true).ToString("F6") + "|";
                            toFile += a.Predict(false, true, 10).ToString("F6") + "|";
                            toFile += a.Predict(false, true, 5).ToString("F6") + "|";
                            toFile += a.Predict(false, true, 1).ToString("F6") + "|";
                            toFile += a.Predict(false, false).ToString("F6") + "|";
                            toFile += a.Predict(false, false, 10).ToString("F6") + "|";
                            toFile += a.Predict(false, false, 5).ToString("F6") + "|";
                            toFile += a.Predict(false, false, 1).ToString("F6");
                        }
                        writer.WriteLine(toFile);
                        }
                    
                }
            }
            using (var writer = new StreamWriter("parametry.txt"))
            {
                writer.WriteLine("rate|momentum|alpha|teach error|predic error 30%|10 values|5 |1|predic error 30%|10 values|5 |1");
                double alpha = 0;
                LearnerRate = 0.0;
                LearnerMomentum = 0.0;
                IterationsCount = 500;
                for (int j = 0; j < 50; j++)
                {
                    Init = new OptimalRangeRandomInitializer(Activ = new BipolarSigmoid(2.0));
                    //Activ = new BipolarSigmoid((double)2.0);

                    string toFile = LearnerRate + "|" + LearnerMomentum + "|2.0|";
                    Layer = new List<int> { 3 };
                    Layer.Add(12);
                    Layer.Add(1);
                    var a = this.Owner as MainForm;
                    if (a != null)
                    {
                        a.SetSettings();
                        toFile += a.Teach(false).ToString("F6") + "|";
                        toFile += a.Predict(false, true).ToString("F6") + "|";
                        toFile += a.Predict(false, true, 10).ToString("F6") + "|";
                        toFile += a.Predict(false, true, 5).ToString("F6") + "|";
                        toFile += a.Predict(false, true, 1).ToString("F6") + "|";
                        toFile += a.Predict(false, false).ToString("F6") + "|";
                        toFile += a.Predict(false, false, 10).ToString("F6") + "|";
                        toFile += a.Predict(false, false, 5).ToString("F6") + "|";
                        toFile += a.Predict(false, false, 1).ToString("F6");
                    }
                    writer.WriteLine(toFile);
                    LearnerRate += 0.05;
                }
                for (int j = 0; j < 50; j++)
                {
                    Init = new OptimalRangeRandomInitializer(Activ = new BipolarSigmoid(2.0));
                    //Activ = new BipolarSigmoid((double)2.0);
                    LearnerRate = 0.3;
                    string toFile = LearnerRate + "|" + LearnerMomentum + "|2.0|";
                    Layer = new List<int> { 3 };
                    Layer.Add(12);
                    Layer.Add(1);
                    var a = this.Owner as MainForm;
                    if (a != null)
                    {
                        a.SetSettings();
                        toFile += a.Teach(false).ToString("F6") + "|";
                        toFile += a.Predict(false, true).ToString("F6") + "|";
                        toFile += a.Predict(false, true, 10).ToString("F6") + "|";
                        toFile += a.Predict(false, true, 5).ToString("F6") + "|";
                        toFile += a.Predict(false, true, 1).ToString("F6") + "|";
                        toFile += a.Predict(false, false).ToString("F6") + "|";
                        toFile += a.Predict(false, false, 10).ToString("F6") + "|";
                        toFile += a.Predict(false, false, 5).ToString("F6") + "|";
                        toFile += a.Predict(false, false, 1).ToString("F6");
                    }
                    writer.WriteLine(toFile);

                    LearnerMomentum += 0.05;
                }
                for (int j = 0; j < 50; j++)
                {
                    Init = new OptimalRangeRandomInitializer(Activ = new BipolarSigmoid(alpha));
                    //Activ = new BipolarSigmoid((double)2.0);
                    LearnerRate = 0.3;
                    LearnerMomentum = 0.0;
                    string toFile = LearnerRate + "|" + LearnerMomentum + "|" + alpha + "|";
                    Layer = new List<int> { 3 };
                    Layer.Add(12);
                    Layer.Add(1);
                    var a = this.Owner as MainForm;
                    if (a != null)
                    {
                        a.SetSettings();
                        toFile += a.Teach(false).ToString("F6") + "|";
                        toFile += a.Predict(false, true).ToString("F6") + "|";
                        toFile += a.Predict(false, true, 10).ToString("F6") + "|";
                        toFile += a.Predict(false, true, 5).ToString("F6") + "|";
                        toFile += a.Predict(false, true, 1).ToString("F6") + "|";
                        toFile += a.Predict(false, false).ToString("F6") + "|";
                        toFile += a.Predict(false, false, 10).ToString("F6") + "|";
                        toFile += a.Predict(false, false, 5).ToString("F6") + "|";
                        toFile += a.Predict(false, false, 1).ToString("F6");
                    }
                    writer.WriteLine(toFile);
                    alpha += 0.1;
                }
            }
           
        }

        private void runTests_Click(object sender, EventArgs e)
        {
            tests();
        }
    }
}