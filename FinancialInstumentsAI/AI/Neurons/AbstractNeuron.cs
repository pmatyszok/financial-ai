using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Neurons
{
    abstract class AbstractNeuron
    {
        public int inputs
        {
            get;
            protected set;
        }

        public double output
        {
            get;
            protected set;
        }

        protected double[] weights;

        public Functions.IActivationFunction ActivationFunction { get; set; }

        public INeuronInitilizer Initializer { get; set; }

        public AbstractNeuron(int inputCount, INeuronInitilizer init, Functions.IActivationFunction function)
        {
            inputs = inputCount;
            weights = new double[inputCount];
            ActivationFunction = function;
            Initializer = init;
            Initialize();
        }

        public virtual void Initialize()
        {
            Initializer.Initialize(weights);
        }

        public abstract double ComputeOutput(double[] input);

    }
}
