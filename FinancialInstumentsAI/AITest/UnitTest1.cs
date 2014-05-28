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
        private double TransformData(double input, double min, double range)
        { //scale to -1..1
            return (((input - min) * 2.0) / range) - 1.0;
        }

        private double TransformBack(double input, double min, double max)
        { //scale back to original data from -1..1
            return ((input + 1) * (max - min) / 2.0) + min;
        }

        [TestMethod()]
        public void TestTimeSeriesPrediction()
        {
            const int windowSize = 5;
            const int predictionSize = 1;
            IActivationFunction func = new BipolarSigmoid(2.0);
            var layerCounts = new List<int>();
            layerCounts.Add(windowSize);
            layerCounts.Add(2*windowSize);
            layerCounts.Add(1);
            var network = new Network(5, 3, layerCounts, func, new RandomInitializer());
            //double[] input = new double[] { -3, -2, -1, 0, 1, 2, 3 };
            //double[] output = new double[] { 9, 4, 1, 0, 1, 4, 9 };

            double[] readedData = new double[50];
            double[] data;
            double min, max;
            double range;
            using(var reader = new StreamReader(File.OpenRead("sinusoid.csv")))
            {
                int i = 0;
                readedData[i] = double.Parse(reader.ReadLine(), System.Globalization.CultureInfo.InvariantCulture);
                min = readedData[i];
                max = readedData[i];

                try
                {
                    for (i = 1; i < 50; i++)
                    {
                        readedData[i] = double.Parse(reader.ReadLine(), System.Globalization.CultureInfo.InvariantCulture);
                        if (min > readedData[i])
                            min = readedData[i];
                        if (max < readedData[i])
                            max = readedData[i];
                    }
                }
                catch (Exception ex)
                {
                   ;
                }
                if (i > 0)
                {
                    data = new double[i];
                    Array.Copy(readedData, data, i);
                }
                else return;
            }
            range =  (max - min);
            int learningSamples = data.Length - windowSize - predictionSize;

            double[][] input = new double[learningSamples][];
            double[][] output = new double[learningSamples][];

            

            for (int i = 0; i < learningSamples; i++)
            {
                input[i] = new double[windowSize];
                output[i] = new double[1];

                for (int j = 0; j < windowSize; j++)
                {
                    input[i][j] = TransformData(data[i + j], min, range);
                }
                output[i][0] = TransformData(data[i + windowSize], min, range);
            }
            //double[] input = new double[] { 1,2,3,4,5,6,7,8,9 };
            //double[] output = new double[]{ 0,4,7,9,10,9,7,4,0 };
            var learner = new Teacher(network);

            learner.Rate = 0.1;
            learner.Momentum = 0.0;
            var solution = new double[data.Length - windowSize, 2];
            var netInput = new double[windowSize];

            for (int i = 0; i < 1000; i++)
            {
                var ins = new List<double[]>();
                var outs = new List<double[]>();

                for (int ii = 0; ii < input.Length; ii++)
                {
                    ins.Add(input[ii]);
                    outs.Add(output[ii]);
                }    
                learner.TeachOnSamples(ins, outs);
                for (int j = 0; j < data.Length - windowSize; j++)
                {
                    for (int k = 0; k < windowSize; k++)
                    {
                        netInput[k] = TransformData(data[j + k], min, range) ;
                    }
                    solution[j,1]= TransformBack(network.ComputeOutputVector(netInput)[0], min, max );//(network.ComputeOutputVector(netInput)[0]) / range + min;
                }
            }



            double[] aproximated = new double[data.Length - windowSize];
            Console.WriteLine("Try those Values in Excel or whatever");
            using (var writer = new StreamWriter("data_dump_time_series.txt"))
            {
                for (int i = 0; i < aproximated.Length; i++)
                {
                    aproximated[i] = solution[i,1];
                    writer.WriteLine((aproximated[i]).ToString());
                }
            }
        }
    }
}
