namespace MatrixCalculations
{
    public interface IMatrixCalculateProvider
    {
        Matrix Add(Matrix matrix, Matrix rightSide);
        Matrix Add(Matrix leftSide, double number);

        Matrix Substract(Matrix matrix, double number);
        Matrix Substract(Matrix leftSide, Matrix rightSide);

        Matrix Multiply(Matrix matrix, double number);
        Matrix Multiply(Matrix leftSide, Matrix rightSide);

        Matrix Transpose(Matrix matrix);

        bool IsEqual(Matrix leftSide, Matrix rightSide);
    }
}
