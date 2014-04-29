using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Neurons
{
    class RandomInitializer : INeuronInitilizer
    {
        private Random random;
        private double randomMin;
        private double randomMax;

        public RandomInitializer(double randMin = 0.0, double randMax = 1.0)
        {
            random = new Random((int)DateTime.Now.Ticks);
        }

        public void Initialize(double[] weights)
        {
            double range = randomMax - randomMin;
            for (int i = 0; i < weights.Length; i++)
            { 
                weights[i] = random.NextDouble() * range + randomMin;
            }
        }

        public double Initialize()
        {
            return random.NextDouble() * (randomMax - randomMin) + randomMin;
        }
    }
}
