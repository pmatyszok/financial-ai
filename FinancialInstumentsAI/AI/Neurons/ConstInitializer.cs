using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Neurons
{
    public class ConstInitializer : INeuronInitilizer
    {
        private double value;
        public double Value 
        { 
            get { return value; } 
            set { this.value = Math.Max(0.0, Math.Min(value, 1.0)); } 
        }
        public ConstInitializer(double value)
        {
            Value = value;
        }

        public void Initialize(double[] weights)
        {
            for (int i = 0; i < weights.Length; i++)
                weights[i] = Value;    
        }

        public double Initialize()
        {
            return value;
        }
    }
}
