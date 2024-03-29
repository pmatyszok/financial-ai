﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Neurons
{
    [System.Runtime.Serialization.DataContract]
    public class RandomInitializer : INeuronInitilizer
    {
        [System.Runtime.Serialization.IgnoreDataMember]
        private Random random;
        [System.Runtime.Serialization.DataMember]
        private double randomMin;
        [System.Runtime.Serialization.DataMember]
        private double randomMax;
        public RandomInitializer()
            : this(0.0, 1.0)
        { 
            
        }

        public RandomInitializer(double randMin = 0.0, double randMax = 1.0)
        {
            random = new Random((int)DateTime.Now.Ticks);
            randomMin = randMin;
            randomMax = randMax;
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
