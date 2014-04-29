﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Functions
{
    interface IActivationFunction
    {
        /// <summary>
        /// Calculates function value at given point
        /// </summary>
        double Calculate(double x);

        double Derivative(double x);
    }
}
