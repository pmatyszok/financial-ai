﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using AI.Functions;
using AI.Neurons;

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

        private void button2_Click(object sender, EventArgs e)
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
    }
}