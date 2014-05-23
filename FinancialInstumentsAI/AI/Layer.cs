using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AI.Neurons;

namespace AI
{
    public class Layer
    {
        private Neuron[] neurons;

        public int Inputs { get; set; }
        public double[] Output { get; set; }
        public int Size { get { return neurons.Length; } }
        public Layer(int inputsCount, int neuronsCount, Functions.IActivationFunction function, INeuronInitilizer initializer)
        {
            Inputs = inputsCount;
            neurons = new Neuron[neuronsCount];
            for(int i = 0; i < neuronsCount; ++i)
            {
                neurons[i] = new Neuron(inputsCount, function, initializer);
            }
            Output = new double[neuronsCount];
        }

        public void Initialize()
        {
            foreach(Neuron neuron in neurons)
                neuron.Initialize();
        }

        public double[] CalculateOutput(double[] input)
        {
            for (int i = 0; i < neurons.Length; ++i)
                Output[i] = neurons[i].ComputeOutput(input);
            return Output;
        }
        public Neuron this[int i] { get { return neurons[i]; } }
    }
}
