using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Neurons
{
    [Serializable]
    [System.Runtime.Serialization.DataContract]    
    public abstract class AbstractNeuron
    {
        [System.Runtime.Serialization.DataMember]
        public int Inputs
        {
            get;
            protected set;
        }
        [System.Runtime.Serialization.DataMember]
        public double Output
        {
            get;
            protected set;
        }
        [System.Runtime.Serialization.DataMember]
        public double Noise { get; set; }
        [System.Runtime.Serialization.DataMember]
        public double[] Weights;
        [System.Runtime.Serialization.DataMember]
        public Functions.IActivationFunction ActivationFunction { get; set; }
        [System.Runtime.Serialization.DataMember]
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
