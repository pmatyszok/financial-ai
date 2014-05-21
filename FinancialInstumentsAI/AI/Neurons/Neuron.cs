using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Neurons
{
    class Neuron : AbstractNeuron
    {
        public double threshold { get; set; }

        public Neuron(int inputs, Functions.IActivationFunction function, INeuronInitilizer initializer)
            : base(inputs, initializer, function)
        { }

        public override void Initialize()
        {
            base.Initialize();

            threshold = Initializer.Initialize();
        }

        public override double ComputeOutput(double[] input)
        {
            if (input.Length != Inputs)
                throw new ApplicationException("Wrong number of data on neuron' inputs!");

            double ret = threshold;
            for (int i = 0; i < input.Length; i++)
                ret += Weights[i] * input[i];

            Output = ActivationFunction.Calculate(ret);
            
            return Output;
            
        }
    }
}
