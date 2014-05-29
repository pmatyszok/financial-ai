using AI;
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

        public static List<int> layer { get; set; }
        public static INeuronInitilizer init { get; set; }
        public static IActivationFunction activ { get; set; }
        public static double learnerRate{get;set;}
        public static double learnerMomentum{ get; set; }

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
                    init = new RandomInitializer();
                    break;
                case (1):
                    init = new OptimalRangeRandomInitializer(activ = new BipolarSigmoid((double)alphaNumeric.Value));
                   break;
                case (2):
                    init = new ConstInitializer(double.Parse(constValueTextBox.Text));
                    break;
                default:
                    init = null;
                    break;
            }

            if (activFuncComboBox.SelectedIndex == 1)
            {
                activ = new BipolarSigmoid((double)alphaNumeric.Value);
            }
            else
            {
                activ = new Sigmoid((double)alphaNumeric.Value);
            }

            learnerRate = (double)rateNumeric.Value;
            learnerMomentum = (double)momentumNumeric.Value;

            layer = new List<int>();
            layer.Add((int)windowSize.Value);
            if (layerCountCheckBox.Checked)
            {
                for (int i = 0; i < (int)layersNumeric.Value - 2; i++)
                {
                    layer.Add((int)windowSize.Value * 2);
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
                        layer.Add(neuron.value);
                    }
                }
            }
            
            layer.Add(1);

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