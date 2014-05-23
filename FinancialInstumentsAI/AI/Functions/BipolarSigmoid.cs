using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Functions
{
    public class BipolarSigmoid : IActivationFunction
    {

        public double Alpha{get;set;}
        public BipolarSigmoid(double alpha)
        {
            this.Alpha = alpha;
        }
        public double Calculate(double x)
        {
            return ((2/ (1 + Math.Exp(-Alpha * x))) - 1);
        }

        public double Derivative(double x)
        {
            var y = Calculate(x);
            return Alpha * (1 - y * y) / 2;
        }

        public double DerivativeFromY(double y)
        {
            return (Alpha * (1 - y * y) / 2);
        }
    }
}
