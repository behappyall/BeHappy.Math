using Xunit;

namespace MatrixCalculations.Tests
{
    public class MatrixMultiplicationTests
    {
        [Theory]
        [InlineData(MatrixProccessType.NonParallel)]
        [InlineData(MatrixProccessType.Parallel)]
        public void MatrixMultiplication_Simple(MatrixProccessType type)
        {
            //Arrange
            MatrixOperationSettings.ProccessType = type;

            Matrix A = new Matrix(new double[3][]
            {
                new double[2] {-1, 1},
                new double[2] {2, 0},
                new double[2] {0, 3}
            });

            Matrix B = new Matrix(new double[2][]
            {
                new double[3] {3, 1, 2},
                new double[3] {0, -1, 4}
            });

            var expected = new Matrix(new double[3][]
            {
                new double[3] {-3, -2, 2},
                new double[3] {6, 2, 4},
                new double[3] {0, -3, 12}
            });

            //Act
            var actual = A * B;
            //Assert
            Assert.Equal(actual, expected);
        }


        [Theory]
        [InlineData(MatrixProccessType.NonParallel)]
        [InlineData(MatrixProccessType.Parallel)]
        public void MatrixMultiplication_Hard(MatrixProccessType type)
        {
            //Arrange
            MatrixOperationSettings.ProccessType = type;
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


            var expected = 74975449500d;

            //Act
            var actual = a * b;

            //Assert
            Assert.Equal(actual[554, 123], expected);
        }
    }
}
