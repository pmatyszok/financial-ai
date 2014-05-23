using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Neurons
{
    public interface INeuronInitilizer
    {
        void Initialize(double[] weights);

        double Initialize();
    }
}
