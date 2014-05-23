using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI
{
    public class Network
    {
        public int Inputs { get; set; }
        public int LayersCount { get; set; }

        private Layer[] layers;
        private double[] output;

        public Network(int inputsCount, int layersCount, List<int> neuronsCountInHiddenLayers,
            Functions.IActivationFunction function, Neurons.INeuronInitilizer initializer )
        {
            NetworkInit(inputsCount, layersCount);
            if (neuronsCountInHiddenLayers.Count != layersCount)
                throw new ApplicationException("Number of layers and provided layer's neuron count do not match!");
            layers[0] = new Layer(inputsCount, inputsCount, function, initializer);
            for(int i = 1; i < layers.Length; i++)
                layers[i] = new Layer(neuronsCountInHiddenLayers[i-1], neuronsCountInHiddenLayers[i], function, initializer);
        }
        public void NetworkInit(int inputsCount, int layersCount)
        {
            Inputs = inputsCount;
            LayersCount = layersCount;
            layers = new Layer[LayersCount];
        }

        public Layer InputLayer { get { return layers[0]; } }

        public Layer OutputLayer { get { return layers[layers.Length - 1]; } }

        public double[] ComputeOutputVector(double[] input)
        {
            output = new double[input.Length];
            Array.Copy(input, output, input.Length);
            foreach (var layer in layers)
            {
                output = layer.CalculateOutput(output);
            }
            return output;
        }

        public void Initialize()
        {
            foreach (var layer in layers)
                layer.Initialize();    
        }

        public Layer LayerAt(int i) 
        {
            return layers[i];
        }

        public Layer this[int i] { get { return layers[i]; } }
    }
}
