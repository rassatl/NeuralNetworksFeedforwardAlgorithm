using NeuralNetworksFeedforwardAlgorithm;
using System;
using System.Collections.Generic;

public class Matrix
{
    private int rows;
    private int cols;
    private double[][] data;

    public Matrix(int rows, int cols)
    {
        this.rows = rows;
        this.cols = cols;
        this.data = new double[rows][];
        for (int i = 0; i < rows; i++)
        {
            this.data[i] = new double[cols];
        }
    }

    public Matrix Copy()
    {
        Matrix result = new Matrix(rows, cols);
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                result.data[i][j] = data[i][j];
            }
        }
        return result;
    }

    public static Matrix FromArray(double[] arr)
    {
        Matrix result = new Matrix(arr.Length, 1);
        for (int i = 0; i < arr.Length; i++)
        {
            result.data[i][0] = arr[i];
        }
        return result;
    }

    public static Matrix Subtract(Matrix a, Matrix b)
    {
        if (a.rows != b.rows || a.cols != b.cols)
        {
            Console.WriteLine("Columns and Rows of A must match Columns and Rows of B.");
            return null;
        }

        Matrix result = new Matrix(a.rows, a.cols);
        for (int i = 0; i < a.rows; i++)
        {
            for (int j = 0; j < a.cols; j++)
            {
                result.data[i][j] = a.data[i][j] - b.data[i][j];
            }
        }

        return result;
    }

    public double[] ToArray()
    {
        List<double> arr = new List<double>();

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                arr.Add(data[i][j]);
            }
        }

        return arr.ToArray();
    }




    public void Randomize()
    {
        Random random = new Random();
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                data[i][j] = random.NextDouble() * 2 - 1;
            }
        }
    }

    public void Add(Matrix n)
    {
        if (n is Matrix)
        {
            Matrix matrixN = (Matrix)n;
            for (int i = 0; i < this.rows; i++)
            {
                for (int j = 0; j < this.cols; j++)
                {
                    this.data[i][j] += matrixN.data[i][j];
                }
            }
        }
        

    }

    public static Matrix Transpose(Matrix matrix)
    {
        Matrix result = new Matrix(matrix.cols, matrix.rows);
        result.Map((_, i, j) => matrix.data[j][i]);
        return result;
    }

    public static Matrix Multiply(Matrix a, Matrix b)
    {
        if (a.cols != b.rows)
        {
            Console.WriteLine("Columns of A must match rows of B.");
            return null;
        }

        Matrix result = new Matrix(a.rows, b.cols);
        result.Map((_, i, j) =>
        {
            double sum = 0;
            for (int k = 0; k < a.cols; k++)
            {
                sum += a.data[i][k] * b.data[k][j];
            }
            return sum;
        });

        return result;
    }

    public void Multiply(double n)
    {
        Map((e, i, j) => e * n);
    }

    public Matrix Map(Func<double, int, int, double> func)
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                data[i][j] = func(data[i][j], i, j);
            }
        }
        return this;
    }


    public void Print()
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                Console.Write(data[i][j] + "\t");
            }
            Console.WriteLine();
        }
    }
}
