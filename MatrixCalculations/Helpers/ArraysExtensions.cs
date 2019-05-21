using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace MatrixCalculations.Helpers
{
    internal static class ArraysExtensions
    {
        internal static T[][] ToJaggedArray<T>(this T[,] twoDimensionalArray)
        {
            int rowsFirstIndex = twoDimensionalArray.GetLowerBound(0);
            int rowsLastIndex = twoDimensionalArray.GetUpperBound(0);
            int numberOfRows = rowsLastIndex + 1;

            int columnsFirstIndex = twoDimensionalArray.GetLowerBound(1);
            int columnsLastIndex = twoDimensionalArray.GetUpperBound(1);
            int numberOfColumns = columnsLastIndex + 1;

            T[][] jaggedArray = new T[numberOfRows][];
            for (int i = rowsFirstIndex; i <= rowsLastIndex; i++)
            {
                jaggedArray[i] = new T[numberOfColumns];

                for (int j = columnsFirstIndex; j <= columnsLastIndex; j++)
                {
                    jaggedArray[i][j] = twoDimensionalArray[i, j];
                }
            }
            return jaggedArray;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double[][] GetJaggedArray(int rows, int columns)
        {
            double[][] array = new double[rows][];

            for (int i = 0; i < rows; i++)
                array[i] = new double[columns];

            return array;
        }
    }
}
