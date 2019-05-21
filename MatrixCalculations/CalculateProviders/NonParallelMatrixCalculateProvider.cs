using MatrixCalculations.Helpers;
using System;

namespace MatrixCalculations.CalculateProviders
{
    public class NonParallelMatrixCalculateProvider : IMatrixCalculateProvider
    {
        public Matrix Add(Matrix matrix, double number)
        {
            var resultData = ArraysExtensions.GetJaggedArray(matrix.RowsCount, matrix.ColumnsCount);

            for (int i = 0; i < matrix.RowsCount; i++)
                for (int j = 0; j < matrix.ColumnsCount; j++)
                    resultData[i][j] = matrix[i, j] + number;

            return new Matrix(resultData);
        }
        public Matrix Add(Matrix leftSide, Matrix rightSide)
        {
            var resultData = ArraysExtensions.GetJaggedArray(leftSide.RowsCount, leftSide.ColumnsCount);

            for (int i = 0; i < leftSide.RowsCount; i++)
                for (int j = 0; j < leftSide.ColumnsCount; j++)
                    resultData[i][j] = leftSide[i, j] + rightSide[i, j];

            return new Matrix(resultData);
        }


        public Matrix Substract(Matrix matrix, double number) => Add(matrix, -1 * number);
        public Matrix Substract(Matrix leftSide, Matrix rightSide)
        {
            var resultData = ArraysExtensions.GetJaggedArray(leftSide.RowsCount, leftSide.ColumnsCount);

            for (int i = 0; i < leftSide.RowsCount; i++)
                for (int j = 0; j < leftSide.ColumnsCount; j++)
                    resultData[i][j] = leftSide[i, j] - rightSide[i, j];

            return new Matrix(resultData);
        }

        public Matrix Multiply(Matrix matrix, double number)
        {
            double[][] result = ArraysExtensions.GetJaggedArray(matrix.RowsCount, matrix.ColumnsCount);

            for (int i = 0; i < matrix.RowsCount; i++)
                for (int j = 0; j < matrix.ColumnsCount; j++)
                    result[i][j] = matrix[i, j] * number;

            return new Matrix(result);
        }
        public Matrix Multiply(Matrix leftSide, Matrix rightSide)
        {
            int rows = leftSide.RowsCount;
            int columns = rightSide.ColumnsCount;
            double[][] result = ArraysExtensions.GetJaggedArray(rows, columns);

            var leftMatrixData = (double[][])leftSide;

            var rightMatrixData_Transpose = (double[][])Transpose(rightSide); // just for more speed.

            for (int i = 0; i < rows; i++)
            {
                //var row = leftMatrixData[i]; // just for more speed. pointer
                for (int j = 0; j < columns; j++)
                {
                    //var column = rightSide_Transpose[j]; // just for more speed. pointer
                    double sum = 0;
                    for (int k = 0; k < leftSide.ColumnsCount; k++)
                        sum += leftMatrixData[i][k] * rightMatrixData_Transpose[j][k];

                    result[i][j] = sum;
                }
            }
            return new Matrix(result);
        }


        public Matrix Transpose(Matrix matrix)
        {
            double[][] result = ArraysExtensions.GetJaggedArray(matrix.ColumnsCount, matrix.RowsCount);

            for (int i = 0; i < matrix.RowsCount; i++)
                for (int j = 0; j < matrix.ColumnsCount; j++)
                    result[j][i] = matrix[i, j];

            return new Matrix(result);
        }

        public bool IsEqual(Matrix leftSide, Matrix rightSide)
        {
            if (leftSide.RowsCount != rightSide.RowsCount || leftSide.ColumnsCount != rightSide.ColumnsCount)
                return false;

            for (int i = 0; i < leftSide.RowsCount; i++)
                for (int j = 0; j < leftSide.ColumnsCount; j++)
                    if (leftSide[i, j] != rightSide[i, j])
                        return false;

            return true;
        }
    }
}
