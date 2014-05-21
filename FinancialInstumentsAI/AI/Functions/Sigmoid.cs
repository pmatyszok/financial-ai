using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Functions
{
    class Sigmoid : IActivationFunction
    {
        public double Alpha { get; set; }

        public Sigmoid(double a)
        {
            this.Alpha = a;
        }

        public double Derivative(double x)
        {
            var y = Calculate(x);
            return (Alpha * y * (1 - y));
        }

        public double Calculate(double x)
        {
            return (1 / (1 + Math.Exp(-Alpha * x)));
        }

        public double DerivativeFromY(double y)
        {
            return (Alpha * y * (1 - y));
        }
    }
}
