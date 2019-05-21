using PolynomialExpression;
using System;
using System.Collections.Generic;

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

            var b = new Polynomial(new List<Term>()
            {
                new Term(0, 4),
                new Term(1, 5)
            });
            Console.WriteLine(b);
            Console.WriteLine(polynomial);
            Console.WriteLine(b==polynomial);
            Console.WriteLine(new string('-', 50));
            Console.WriteLine(polynomial.ToString());
            Console.WriteLine((polynomial * term).ToString());
            Console.WriteLine(polynomial[5]);
        }
    }
}
