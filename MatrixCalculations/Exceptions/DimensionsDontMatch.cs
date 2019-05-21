using System;

namespace MatrixCalculations.Exceptions
{
    public class DimensionsDontMatchException : Exception
    {
        public DimensionsDontMatchException()
        {
        }

        public DimensionsDontMatchException(string message)
            : base(message)
        {
        }

        public DimensionsDontMatchException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
