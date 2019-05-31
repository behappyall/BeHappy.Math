using MatrixCalculations.Helpers;
using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace MatrixCalculations.CalculateProviders
{
    public class ParallelMatrixCalculateProvider : IMatrixCalculateProvider
    {
        public Matrix Add(Matrix matrix, double number)
        {
            var resultData = ArraysExtensions.GetJaggedArray(matrix.RowsCount, matrix.ColumnsCount);

            Parallel.For(0, matrix.RowsCount, i =>
            {
                for (int j = 0; j < matrix.ColumnsCount; j++)
                    resultData[i][j] = matrix[i, j] + number;
            });

            return new Matrix(resultData);
        }
        public Matrix Add(Matrix leftSide, Matrix rightSide)
        {
            var resultData = ArraysExtensions.GetJaggedArray(leftSide.RowsCount, leftSide.ColumnsCount);

            Parallel.For(0, leftSide.RowsCount, i =>
            {
                // also can be pointer
                for (int j = 0; j < leftSide.ColumnsCount; j++)
                    resultData[i][j] = leftSide[i, j] + rightSide[i, j];
            });

            return new Matrix(resultData);
        }

        public Matrix Substract(Matrix matrix, double number) => Add(matrix, -1 * number);
        public Matrix Substract(Matrix leftSide, Matrix rightSide)
        {
            var result = ArraysExtensions.GetJaggedArray(leftSide.RowsCount, leftSide.ColumnsCount);

            Parallel.For(0, leftSide.RowsCount, i =>
            {
                for (int j = 0; j < leftSide.ColumnsCount; j++)
                    result[i][j] = leftSide[i, j] - rightSide[i, j];
            });

            return new Matrix(result);
        }

        public Matrix Multiply(Matrix leftSide, double number)
        {
            double[][] result = ArraysExtensions.GetJaggedArray(leftSide.RowsCount, leftSide.ColumnsCount);

            var leftData = (double[][])leftSide;
            Parallel.For(0, leftSide.RowsCount, (i) =>
            {
                var row = leftData[i];
                for (int j = 0; j < leftSide.ColumnsCount; j++)
                    result[i][j] = row[j] * number;
            });

            return new Matrix(result);
        }
        public Matrix Multiply(Matrix leftSide, Matrix rightSide)
        {
            int rows = leftSide.RowsCount;
            int columns = rightSide.ColumnsCount;

            double[][] result = ArraysExtensions.GetJaggedArray(rows, columns);

            double[][] leftMatrixData = (double[][])leftSide;
            double[][] rightMatrixData_Transpose = (double[][])Transpose(rightSide);// just for more speed

            Parallel.For(0, rows, (i) =>
            {
                var row = leftMatrixData[i]; // just for more speed
                for (int j = 0; j < columns; j++)
                {
                    var column = rightMatrixData_Transpose[j]; // just for more speed
                    double sum = 0;

                    for (int k = 0; k < row.Length; k++)
                        sum += row[k] * column[k];

                    result[i][j] = sum;
                }
            });

            return new Matrix(result);
        }


        public Matrix Transpose(Matrix matrix)
        {
            double[][] result = ArraysExtensions.GetJaggedArray(matrix.ColumnsCount, matrix.RowsCount);

            Parallel.For(0, matrix.RowsCount, (i) =>
            {
                for (int j = 0; j < matrix.ColumnsCount; j++)
                    result[j][i] = matrix[i, j];
            });

            return new Matrix(result);
        }

        public bool IsEqual(Matrix leftSide, Matrix rightSide)
        {
            if (leftSide.RowsCount != rightSide.RowsCount || leftSide.ColumnsCount != rightSide.ColumnsCount)
                return false;

            var loopResult = Parallel.For(0, leftSide.RowsCount, (i, state) =>
            {
                for (int j = 0; j < leftSide.ColumnsCount; j++)
                    if (leftSide[i, j] != rightSide[i, j])
                        state.Stop();
            });

            return loopResult.IsCompleted;
        }
    }
}
