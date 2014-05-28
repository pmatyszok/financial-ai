using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AI.Functions;

namespace AI.Neurons
{
    public class OptimalRangeRandomInitializer : INeuronInitilizer
    {
        private Random random;
        const double aCoef = 2.38;
        public OptimalRangeRandomInitializer(IActivationFunction func)
        {
            if (func is BipolarSigmoid)
            {
                //it is ok
                random = new Random((int)DateTime.Now.Ticks);
            }
            else
                throw new ApplicationException("Only BipolarSigmoid can be used with this initializer.");
        }
        public void Initialize(double[] weights)
        {
            var lim = aCoef / Math.Sqrt(weights.Length);
            var range = lim - (-lim); //explicit to the bone
            var randomMin = -lim;
            for (int i = 0; i < weights.Length; i++)
            {
                weights[i] = random.NextDouble() * range + randomMin;
            }
        }

        public double Initialize()
        {
            return random.NextDouble(); //no way to to make it according to formula here!
        }
    }
}
