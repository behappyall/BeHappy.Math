using System;

namespace MatrixCalculations.Exceptions
{
    public class MatrixInitializeException : Exception
    {
        public MatrixInitializeException()
        {
        }

        public MatrixInitializeException(string message)
            : base(message)
        {
        }

        public MatrixInitializeException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
