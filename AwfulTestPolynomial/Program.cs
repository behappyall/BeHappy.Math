using PolynomialExpression;
using System;

namespace AwfulTestPolynomial
{
    class Program
    {
        static void Main(string[] args)
        {
            Polynomial polynomial = new Polynomial();
            polynomial.Append(4);
            polynomial.Append(new Term(1, 5));

            Term term = new Term(4, 5);

            Console.WriteLine(polynomial.ToString());
            Console.WriteLine((polynomial * term).ToString());
            Console.WriteLine(polynomial[5]);
        }
    }
}
