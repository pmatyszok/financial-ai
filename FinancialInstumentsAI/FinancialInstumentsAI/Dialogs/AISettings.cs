﻿using AI;
using AI.Functions;
using AI.Neurons;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace FinancialInstumentsAI.Dialogs
{
    public sealed partial class AISettings : Form
    {
        private static AISettings instance;

        public static List<int> Layer { get; set; }
        public static INeuronInitilizer Init { get; set; }
        public static IActivationFunction Activ { get; set; }
        public static double LearnerRate{get;set;}
        public static double LearnerMomentum{ get; set; }

        public static int IterationsCount { get; set; }

        private AISettings()
        {
            InitializeComponent();
            initFuncComboBox.DataSource = Enum.GetNames(typeof(Initialization));
            activFuncComboBox.DataSource = Enum.GetNames(typeof(Activation));
            constValueTextBox.Enabled = false;
        }

        public static AISettings Instance
        {
            get { return instance ?? (instance = new AISettings()); }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            switch(initFuncComboBox.SelectedIndex)
            {
                case(0):
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

            Layer = new List<int>();
            Layer.Add((int)windowSize.Value);
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
                    neuronCounts neuron = new neuronCounts();
                    neuron.Text = "Layer no. " + i.ToString() + " count";
                    DialogResult res = neuron.ShowDialog(this);
                    if (res == DialogResult.OK)
                    {
                        Layer.Add(neuron.value);
                    }
                }
            }
            
            Layer.Add(1);

            int iterations = 0;
            if (!int.TryParse(iterationsTextBox.Text, out iterations))
            {
                iterations = 1000;
            }
            IterationsCount = iterations;

            DialogResult = DialogResult.OK;
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
    }
}