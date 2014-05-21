using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Neurons
{
    abstract class AbstractNeuron
    {
        public int Inputs
        {
            get;
            protected set;
        }

        public double Output
        {
            get;
            protected set;
        }

        public double Noise { get; set; }

        public double[] Weights;

        public Functions.IActivationFunction ActivationFunction { get; set; }

        public INeuronInitilizer Initializer { get; set; }

        public AbstractNeuron(int inputCount, INeuronInitilizer init, Functions.IActivationFunction function)
        {
            Inputs = inputCount;
            Weights = new double[inputCount];
            ActivationFunction = function;
            Initializer = init;
            Initialize();
        }

        public virtual void Initialize()
        {
            Initializer.Initialize(Weights);
        }

        public abstract double ComputeOutput(double[] input);

    }
}
