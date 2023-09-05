using System;


namespace NeuralNetworksFeedforwardAlgorithm
{
    public class Setup
    {
        public void setup()
        {
            NeuralNetwork neuralNetwork = new NeuralNetwork(2, 2, 1);

            double[] input = new double[] { 1, 0 };
            Matrix output = neuralNetwork.FeedForward(input);
            Console.WriteLine(output);
        }
    }
}
