using MatrixCalculations;
using System;
using System.Diagnostics;
using System.IO;

namespace AwfulTest
{
    class Program
    {
        static void Main(string[] args)
        {
            MatrixOperationSettings.ProccessType = MatrixProccessType.Parallel;
            int n = 1000;
            int m = 1000;
            var leftData = new double[n, m];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                    leftData[i, j] = i + j;


            var rightData = new double[m, n];
            for (int i = 0; i < m; i++)
                for (int j = 0; j < n; j++)
                    rightData[i, j] = i * j;

            Matrix a = new Matrix(leftData);
            Matrix b = new Matrix(rightData);

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Matrix parallelResult = a * b;
            stopwatch.Stop();
            Console.WriteLine(stopwatch.ElapsedMilliseconds);
            //MatrixOperationSettings.ProccessType = MatrixProccessType.NonParallel;

            //stopwatch.Restart();
            //var nonParallelResult = a * b;
            //stopwatch.Stop();
            //Console.WriteLine(stopwatch.ElapsedMilliseconds);

            //Console.WriteLine(parallelResult[554, 123]);
            //Console.WriteLine(nonParallelResult[554, 123]);
            //nonParallelResult[1, 2] = 3;
            //MatrixOperationSettings.ProccessType = MatrixProccessType.Parallel;
            //Console.WriteLine(nonParallelResult==parallelResult);
        }
    }
}
