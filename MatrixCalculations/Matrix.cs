using MatrixCalculations.Exceptions;
using MatrixCalculations.Helpers;
using System;
using System.Linq;
using static MatrixCalculations.MatrixOperationSettings;

namespace MatrixCalculations
{
    public class Matrix : IEquatable<Matrix>
    {
        /// <summary>
        /// matrix data
        /// </summary>
        private double[][] _data;

        public int RowsCount { get; private set; }

        public int ColumnsCount { get; private set; }

        /// <summary>
        /// get or set element by indexes
        /// </summary>
        /// <param name="i">rows index</param>
        /// <param name="j">columns index</param>
        /// <returns></returns>
        public double this[int i, int j]
        {
            get
            {
                if (i < 0 || i >= RowsCount)
                    throw new ArgumentException($"Parameter {nameof(i)} must be bigger 0 and less than {nameof(RowsCount)}={RowsCount}");

                if (j < 0 || j >= ColumnsCount)
                    throw new ArgumentException($"Parameter {nameof(j)} must be bigger 0 and less than {nameof(ColumnsCount)}={ColumnsCount}");

                return _data[i][j];
            }
            set
            {
                if (i < 0 || i >= RowsCount)
                    throw new ArgumentException($"Parameter {nameof(i)} must be bigger 0 and less than {nameof(RowsCount)}={RowsCount}");

                if (j < 0 || j >= ColumnsCount)
                    throw new ArgumentException($"Parameter {nameof(j)} must be bigger 0 and less than {nameof(ColumnsCount)}={ColumnsCount}");

                _data[i][j] = value;
            }
        }

        public Matrix(double[][] data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            if (data.Any(r => r.Length != data[0].Length))
                throw new MatrixInitializeException($"All rows must be the same length");

            __initData(data);
        }

        public Matrix(double[,] data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            __initData(data.ToJaggedArray());
        }

        /// <summary>
        /// Constructor for empty matrix
        /// </summary>
        /// <param name="rowsCount">Count of Rows</param>
        /// <param name="columnsCount">Count of Columns</param>
        public Matrix(int rowsCount, int columnsCount)
        {
            if (rowsCount < 1 || columnsCount < 1)
                throw new MatrixInitializeException($"{nameof(rowsCount)} and {nameof(columnsCount)} params must be positive numbers (>0)!");

            var data = new double[rowsCount][];

            for (int i = 0; i < rowsCount; i++)
                data[i] = new double[columnsCount];

            __initData(data);
        }

        private void __initData(double[][] data)
        {
            this._data = data;
            this.RowsCount = _data.Length;
            this.ColumnsCount = _data[0].Length;
        }

        public static Matrix operator +(Matrix leftSide, Matrix rightSide)
        {
            if (leftSide == null)
                throw new ArgumentNullException(nameof(leftSide));

            if (rightSide == null)
                throw new ArgumentNullException(nameof(rightSide));

            if (leftSide.RowsCount != rightSide.RowsCount || leftSide.ColumnsCount != rightSide.ColumnsCount)
                throw new DimensionsDontMatchException("Addition is impossible");

            return CalculateProvider.Add(leftSide, rightSide);
        }
        public static Matrix operator +(Matrix matrix, double number)
        {
            if (matrix == null)
                throw new ArgumentNullException(nameof(matrix));

            return CalculateProvider.Add(matrix, number);
        }
        public static Matrix operator +(double number, Matrix matrix) => matrix + number;


        public static Matrix operator -(Matrix leftSide, Matrix rightSide)
        {
            if (leftSide == null)
                throw new ArgumentNullException(nameof(leftSide));

            if (rightSide == null)
                throw new ArgumentNullException(nameof(rightSide));

            if (leftSide.RowsCount != rightSide.RowsCount || leftSide.ColumnsCount != rightSide.ColumnsCount)
                throw new DimensionsDontMatchException("Substraction is impossible");

            return CalculateProvider.Substract(leftSide, rightSide);
        }
        public static Matrix operator -(Matrix matrix, double number)
        {
            if (matrix == null)
                throw new ArgumentNullException(nameof(matrix));

            return CalculateProvider.Substract(matrix, number);
        }
        public static Matrix operator -(double number, Matrix matrix) => matrix - number;

        public static Matrix operator *(Matrix leftSide, Matrix rightSide)
        {
            if (leftSide == null)
                throw new ArgumentNullException(nameof(leftSide));

            if (rightSide == null)
                throw new ArgumentNullException(nameof(rightSide));

            if (leftSide.ColumnsCount != rightSide.RowsCount)
                throw new DimensionsDontMatchException("Multiplication is impossible");

            return CalculateProvider.Multiply(leftSide, rightSide);
        }
        public static Matrix operator *(Matrix matrix, double number)
        {
            if (matrix == null)
                throw new ArgumentNullException(nameof(matrix));

            return CalculateProvider.Multiply(matrix, number);
        }
        public static Matrix operator *(double number, Matrix matrix) => matrix * number;

        public static bool operator ==(Matrix leftSide, Matrix rightSide)
        {
            if (ReferenceEquals(leftSide, null))
                return false;

            return leftSide.Equals(rightSide);
        }

        public static bool operator !=(Matrix leftSide, Matrix rightSide) => !(leftSide == rightSide);

        public static explicit operator double[][] (Matrix matrix)
        {
            return matrix._data;
        }
        public static explicit operator Matrix(double[][] data)
        {
            return new Matrix(data);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, null))
                return false;

            return obj.GetType() == GetType() && Equals((Matrix)obj);
        }
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;

                for (int i = 0; i < RowsCount; i++)
                {
                    var pointer = _data[i]; // for less work to find j-element in array[i][j]
                    for (int j = 0; j < ColumnsCount; j++)
                        hash = hash * 23 + pointer[j].GetHashCode();
                }

                return hash;
            }

        }

        public bool Equals(Matrix matrix)
        {
            if (ReferenceEquals(matrix, null))
                return false;

            if (ReferenceEquals(this, matrix))
                return true;

            return CalculateProvider.IsEqual(this, matrix);
        }
    }
}