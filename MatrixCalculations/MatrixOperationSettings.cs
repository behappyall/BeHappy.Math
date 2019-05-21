using MatrixCalculations.CalculateProviders;

namespace MatrixCalculations
{
    public static class MatrixOperationSettings
    {
        public static MatrixProccessType ProccessType { get; set; } = MatrixProccessType.Parallel;

        internal static IMatrixCalculateProvider CalculateProvider
        {
            get
            {
                switch (ProccessType)
                {
                    case MatrixProccessType.Parallel:
                        return new ParallelMatrixCalculateProvider();
                    case MatrixProccessType.NonParallel:
                        return new NonParallelMatrixCalculateProvider();
                    default:
                        return new ParallelMatrixCalculateProvider();
                }
            }
        }

    }
}