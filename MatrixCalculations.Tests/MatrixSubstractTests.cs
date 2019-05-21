using Xunit;

namespace MatrixCalculations.Tests
{
    public class MatrixSubstractTests
    {
        [Theory]
        [InlineData(MatrixProccessType.NonParallel)]
        [InlineData(MatrixProccessType.Parallel)]
        public void MatrixSubstract_Simple(MatrixProccessType type)
        {
            //Arrange
            MatrixOperationSettings.ProccessType = type;

            Matrix a = new Matrix(new double[3][]
            {
                new double[3] {-1, 1, 3},
                new double[3] {2, 0, 3},
                new double[3] {0, 3, 3}
            });

            Matrix b = new Matrix(new double[3][]
            {
                new double[3] {3, 1, 2},
                new double[3] {0, -1, 4},
                new double[3] {3, 3, 3}
            });

            var expected = new Matrix(new double[3][]
            {
                new double[3] {-4, 0, 1},
                new double[3] {2, 1, -1},
                new double[3] {-3, 0, 0}
            });

            //Act
            var actual = a - b;
            //Assert
            Assert.Equal(actual, expected);
        }


        [Theory]
        [InlineData(MatrixProccessType.NonParallel)]
        [InlineData(MatrixProccessType.Parallel)]
        public void MatrixSubstract_Hard(MatrixProccessType type)
        {
            //Arrange
            MatrixOperationSettings.ProccessType = type;
            int n = 1000;
            int m = 1000;
            var data = new double[n, m];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                    data[i, j] = i + j;


            Matrix a = new Matrix(data);
            Matrix b = new Matrix(data);


            var expected = new Matrix(n, m);

            //Act
            var actual = a - b;

            //Assert
            Assert.Equal(actual, expected);
        }
    }
}
