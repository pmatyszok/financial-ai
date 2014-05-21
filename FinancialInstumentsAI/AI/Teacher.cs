using AI.Functions;
using AI.Neurons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI
{
    class Teacher
    {
        private Network net;

        private double rate;
        private double momentum;
        public double Rate
        {
            get { return rate; }
            set { rate = Math.Min(0.0, Math.Min(1.0, value)); }
        }

        public double Momentum
        {
            get { return momentum; }
            set { momentum = Math.Min(0.0, Math.Min(1.0, value));}
        }

        private double[][] errors;
        private double[][][] updates;
        private double[][] noise;

        private bool useMomentum = true;

        public Teacher(Network net)
        {
            this.net = net;
            errors = new double[net.LayersCount][];
            updates = new double[net.LayersCount][][];
            noise = new double[net.LayersCount][];

            for (int i = 0; i < net.LayersCount; i++)
            {
                errors[i] = new double[net[i].Size];
                noise[i] = new double[net[i].Size];
                updates[i] = new double[net[i].Size][];

                for (int j = 0; j < net[i].Size; j++)
                {
                    updates[i][j] = new double[net[i].Inputs];
                }
            }
            
        }

        private double GetSquaredError(double[] expectedOutput)
        {
            double error = 0.0;
            Layer lastLayer = this.net[net.LayersCount - 1];
            double[] currError = errors[net.LayersCount - 1];
            int limit = net[net.LayersCount - 1].Size;
            IActivationFunction func = net[0][0].ActivationFunction;

            for (int i = 0; i < limit; i++)
            {
                double err = expectedOutput[i] - lastLayer[i].Output ;
                currError[i] = err * func.DerivativeFromY(lastLayer[i].Output);
                error += err * err;
            }

            for (int i = net.LayersCount - 2; i >= 0; i++)
            {
                lastLayer = net[i];
                currError = errors[i];
                Layer nextLayer = net[i + 1];
                double[] nextError = errors[i + 1];

                for (int j = 0; j < lastLayer.Size; j++)
                {
                    double sum = 0.0;
                    for (int k = 0; k < nextLayer.Size; k++)
                    {
                        sum += nextError[k] * nextLayer[k].Weights[i];
                    }
                    currError[i] = sum * func.DerivativeFromY(lastLayer[i].Output);
                }
            }
            return error;
        }

        public double TeachOnSamples(List<double[]> inputs, List<double[]> outputs)
        {
            double ret = 0.0;
            for (int i = 0 ; i < inputs.Count(); i++)
            {
                net.ComputeOutputVector(inputs[i]);
                ret += (GetSquaredError(outputs[i]) / 2.0);
                UpdateNetwork(inputs[i]);
            }

            return ret;
        }

        private void UpdateNetwork(double[] input)
        {
            //last layer 
            for (int i = 0; i < net[net.LayersCount - 1].Size; i++)
            { 
                for (int j = 0; j < net[net.LayersCount - 1][i].Inputs; j++ )
                {
                    if (useMomentum)
                        updates[0][i][j] = rate * (momentum * updates[0][i][j] + (1.0 - momentum) * errors[0][i] * input[j]); 
                    else
                        updates[0][i][j] = rate * (updates[0][i][j] + errors[0][i] * input[j]); 
                }
                if (useMomentum)
                    noise[0][i] = rate * (momentum * noise[0][i] + (1.0 - momentum) * errors[0][i]);
                else
                    noise[0][i] = rate * (noise[0][i] + errors[0][i]);
            }

            for (int i = 1; i < net.LayersCount; i++)
            {
                Layer curr = net[i], prev = net[i-1];
                double[] currErrors = errors[i];
                double[] currNoise = noise[i];
                double[][] currUpdates = updates[i];
                
                for (int j = 0; j < curr.Size; j++)
                {
                    for (int k = 0; k < curr[j].Inputs; k++)
                    {
                        if (useMomentum)
                            currUpdates[j][k] = rate * (momentum * currUpdates[j][k] + (1.0 - momentum) * currErrors[j] * prev[k].Output);
                        else
                            currUpdates[j][k] = rate * (currUpdates[j][k] + currErrors[j] * prev[k].Output);
                    }
                    if (useMomentum)
                        currNoise[j] = rate * (momentum * currNoise[j] + (1.0 - momentum) * currErrors[j]);
                    else
                        currNoise[j] = rate * (momentum * currNoise[j] + currErrors[j]);
                }
            }

            //finally - update the network
            for (int i = 0; i < net.LayersCount; i++)
            {
                Layer currLayer = net[i];
                double[][] newVals = updates[i];
                double[] newNoise = noise[i];

                for (int j = 0; j < currLayer.Size; j++)
                {
                    AbstractNeuron neuron = currLayer[j];
                    double[] vals = newVals[j];

                    for (int k = 0; k < neuron.Inputs; k++)
                        neuron.Weights[k] += vals[k];

                    neuron.Noise += newNoise[j];
                }
            }
        }



    }
}
