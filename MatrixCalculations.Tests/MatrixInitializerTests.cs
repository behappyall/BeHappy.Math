using MatrixCalculations.Exceptions;
using System;
using Xunit;

namespace MatrixCalculations.Tests
{
    public class MatrixInitializerTests
    {
        [Theory]
        [InlineData(-1, 5)]
        [InlineData(10, -50)]
        [InlineData(-560, -3)]
        public void MatrixInitialize_NegativeRowsColumnsCount(int rows, int columns)
        {
            //Arrange

            //Act
            var exception = Assert.Throws<MatrixInitializeException>(() => new Matrix(rows, columns));

            //Assert
            Assert.NotNull(exception);
        }
    }
}
