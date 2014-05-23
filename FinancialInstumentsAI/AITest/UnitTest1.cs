using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AI;
using AI.Neurons;
using AI.Functions;
using System.Collections.Generic;
using System.IO;
namespace AITest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestNetworkWorkingAtAll_TryAndOperation()
        {
            IActivationFunction func = new BipolarSigmoid(2.0);
            var layerCounts = new List<int>();
            layerCounts.Add(2);
            layerCounts.Add(2);
            layerCounts.Add(1);
            var network = new Network(2, 3, layerCounts, func, new RandomInitializer());
            //double[] input = new double[] { -3, -2, -1, 0, 1, 2, 3 };
            //double[] output = new double[] { 9, 4, 1, 0, 1, 4, 9 };

            double[][] input = new double[][] { new[] { 0.0, 0.0 }, new[] { 1.0, 0.0 }, new[] { 0.0, 1.0 }, new[] { 1.0, 1.0 } };
            double[] output = new double[] { 0.0, 1.0, 1.0, 1.0 };

            //double[] input = new double[] { 1,2,3,4,5,6,7,8,9 };
            //double[] output = new double[]{ 0,4,7,9,10,9,7,4,0 };
            var learner = new Teacher(network);

            learner.Rate = 0.2;
            learner.Momentum = 0.0;

            List<double[]> netInputs = new List<double[]>();
            List<double[]> netOutputs = new List<double[]>();

            for (int i = 0; i < input.Length; i++)
            {
                netInputs.Add(input[i]);
                netOutputs.Add(new double[]{output[i]});
            }

            for (int i = 0; i < 1000; i++)
            {
                learner.TeachOnSamples(netInputs, netOutputs);
            }

            double[] aproximated = new double[input.Length];
            Console.WriteLine("Try those Values in Excel or whatever");
            using (var writer = new StreamWriter("data_dump.txt"))
            {
                for (int i = 0; i < aproximated.Length; i++)
                {
                    aproximated[i] = network.ComputeOutputVector(input[i])[0];
                    writer.WriteLine((aproximated[i] ).ToString());
                }
            }
            
        }
    }
}
