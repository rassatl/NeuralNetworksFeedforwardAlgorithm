using System;
using System.ComponentModel;

namespace NeuralNetworksFeedforwardAlgorithm
{
    public class ActivationFunction
    {
        private Func<double, double> func;
        private Func<double, double> dfunc;

        public ActivationFunction(Func<double, double> func, Func<double, double> dfunc)
        {
            this.func = func;
            this.dfunc = dfunc;
        }

        public double Func(double x) => func(x);
        public double DFunc(double y) => dfunc(y);

        public static float Sigmoid(double value)
        {
            return (float)(1.0 / (1.0 + Math.Pow(Math.E, -value)));
        }
    }

    public class NeuralNetwork
    {
        int input_nodes;
        int hidden_nodes;
        int output_nodes;

        Matrix weights_ih;
        Matrix weights_ho;

        Matrix bias_h;
        Matrix bias_o;

        public NeuralNetwork(int input_nodes, int hidden_nodes, int output_nodes)
        {
            this.input_nodes = input_nodes;
            this.hidden_nodes = hidden_nodes;
            this.output_nodes = output_nodes;
            this.Weights_ih = new Matrix(hidden_nodes, input_nodes);
            this.Weights_ho = new Matrix(output_nodes, hidden_nodes);
            this.weights_ih.Randomize();
            this.weights_ho.Randomize();
            this.bias_h = new Matrix(hidden_nodes, 1);
            this.bias_o = new Matrix(output_nodes, 1);
            this.bias_h.Randomize();
            this.bias_o.Randomize();
        }

        public int Input_nodes { get => input_nodes; set => input_nodes = value; }
        public int Hidden_nodes { get => hidden_nodes; set => hidden_nodes = value; }
        public int Output_nodes { get => output_nodes; set => output_nodes = value; }
        internal Matrix Weights_ih { get => weights_ih; set => weights_ih = value; }
        internal Matrix Weights_ho { get => weights_ho; set => weights_ho = value; }

        public Matrix FeedForward(double[] input_array)
        {
            // generating the hidden outputs (first layer)
            Matrix inputs = Matrix.FromArray(input_array);
            Matrix hidden = Matrix.Multiply(this.weights_ih, inputs);
            hidden.Add(this.bias_h);
            // activation function
            hidden.Map((value, i, j) => ActivationFunction.Sigmoid(value));
            
            // generating the output's output (second layer)
            Matrix output = Matrix.Multiply(this.weights_ho, hidden);
            output.Add(this.bias_o);
            // activation function
            output.Map((value, i, j) => ActivationFunction.Sigmoid(value));

            return output;
        }
    }
}
