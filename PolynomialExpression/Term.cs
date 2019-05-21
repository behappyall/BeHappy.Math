using System;

namespace PolynomialExpression
{
    public class Term : ICloneable, IEquatable<Term>
    {
        public double Power;
        public double Coefficient;

        public double this[double value]
        {
            get => _performResultWith(value);
        }

        private double _performResultWith(double value)
            => Coefficient * Math.Pow(value, Power);

        public Term(double power, double coefficient)
        {
            this.Coefficient = coefficient;
            this.Power = power;
        }


        public void Multiply(Term term)
        {
            if (term == null)
                throw new ArgumentException("term is null", nameof(term));

            this.Coefficient *= term.Coefficient;
            this.Power += term.Power;
        }
        public void Multiply(double number) => Coefficient *= number;

        public void Divide(Term term)
        {
            if (term == null)
                throw new ArgumentException("term is null", nameof(term));

            this.Coefficient /= term.Coefficient;
            this.Power -= term.Power;
        }
        public void Divide(double number) => Coefficient /= number;

        public static Term operator *(Term leftTerm, Term rightTerm)
        {
            if (leftTerm == null)
                throw new ArgumentException("leftTerm is null", nameof(leftTerm));
            if (rightTerm == null)
                throw new ArgumentException("rightTerm is null", nameof(rightTerm));

            var result = (Term)leftTerm.Clone();
            result.Multiply(rightTerm);
            return result;
        }
        public static Term operator *(Term leftTerm, double number)
        {
            if (leftTerm == null)
                throw new ArgumentException("leftTerm is null", nameof(leftTerm));

            var result = (Term)leftTerm.Clone();
            result.Multiply(number);
            return result;
        }

        public static Term operator /(Term leftTerm, Term rightTerm)
        {
            if (leftTerm == null)
                throw new ArgumentException("leftTerm is null", nameof(leftTerm));
            if (rightTerm == null)
                throw new ArgumentException("rightTerm is null", nameof(rightTerm));

            var result = (Term)leftTerm.Clone();
            result.Divide(rightTerm);
            return result;
        }
        public static Term operator /(Term leftTerm, double number)
        {
            if (leftTerm == null)
                throw new ArgumentException("leftTerm is null", nameof(leftTerm));

            var result = (Term)leftTerm.Clone();
            result.Divide(number);
            return result;
        }

        public object Clone()
        {
            return new Term(Power, Coefficient);
        }

        public override string ToString()
        {
            var sign = Coefficient > 0d ? "+ " : String.Empty;
            return $"{sign}{Coefficient}*x^{Power}";

        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, null))
                return false;

            return obj.GetType() == GetType() && Equals((Term)obj);
        }
        public override int GetHashCode()
        {
            unchecked // don't think that unchecked we need.. but
            {
                return 17 * 23 + (Power * Coefficient).GetHashCode(); ;
            }
        }
        public bool Equals(Term other)
        {
            return Power == other.Power && Coefficient == other.Coefficient;
        }
    }
}
