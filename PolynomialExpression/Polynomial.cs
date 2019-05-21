using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace PolynomialExpression
{
    public class Polynomial : IEnumerable<Term>, ICloneable, IEquatable<Polynomial>
    {
        private List<Term> _terms;

        public Polynomial() => _terms = new List<Term>();

        public Polynomial(IEnumerable<Term> terms) => _terms = terms as List<Term> ?? new List<Term>();
    
        public IEnumerator<Term> GetEnumerator() => _terms.OrderByDescending(t => t.Power).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => _terms.OrderByDescending(t => t.Power).GetEnumerator();

        public double this[double value]
        {
            get => _performResultWith(value);
        }

        private double _performResultWith(double value)
            => _terms.Sum(t => t[value]);

        public void Append(double number) => _terms.Add(new Term(0, number));

        public void Append(Term term) => _terms.Add(term ?? throw new ArgumentException("term is null", nameof(term)));

        public void Append(Polynomial polynomial)
            => _terms.AddRange(polynomial ?? throw new ArgumentException("polynomial is null", nameof(polynomial)));

        public void Substract(double number) => Append(number * -1);

        public void Substract(Term term) => Append((term ?? throw new ArgumentException("term is null", nameof(term))) * -1);

        public void Substract(Polynomial polynomial) => Append((polynomial ?? throw new ArgumentException("polynomial is null", nameof(polynomial))) * -1);

        public void Multiply(double number) => _terms = _terms.Select(t => t * number).ToList();

        public void Multiply(Term multiplier) =>
            _terms = _terms.Select(t => t * (multiplier ?? throw new ArgumentException("multiplier is null", nameof(multiplier)))).ToList();

        public void Multiply(Polynomial multiplier)
        {
            if (multiplier == null)
                throw new ArgumentException("multiplier is null", nameof(multiplier));

            List<Term> terms = new List<Term>();
            foreach (var ct in this)
                foreach (var mt in multiplier)
                    terms.Add(ct * mt);

            _terms = terms;
        }

        public void Divide(double number)
            => _terms = _terms.Select(t => t / number).ToList();

        public static Polynomial operator +(Polynomial polynomial, double number)
        {
            if (polynomial == null)
                throw new ArgumentException("polynomial is null", nameof(polynomial));

            var result = (Polynomial)polynomial.Clone();
            result.Append(number);
            return result;
        }
        public static Polynomial operator +(Polynomial polynomial, Term term)
        {
            if (polynomial == null)
                throw new ArgumentException("polynomial is null", nameof(polynomial));

            var result = (Polynomial)polynomial.Clone();
            result.Append(term);
            return result;
        }
        public static Polynomial operator +(Polynomial polynomial, Polynomial addition)
        {
            if (polynomial == null)
                throw new ArgumentException("polynomial is null", nameof(polynomial));

            if (addition == null)
                throw new ArgumentException("addition is null", nameof(addition));

            var result = (Polynomial)polynomial.Clone();
            result.Append(addition);
            return result;
        }

        public static Polynomial operator -(Polynomial polynomial, double number)
        {
            if (polynomial == null)
                throw new ArgumentException("polynomial is null", nameof(polynomial));

            var result = (Polynomial)polynomial.Clone();
            result.Substract(number);
            return result;
        }
        public static Polynomial operator -(Polynomial polynomial, Term term)
        {
            if (polynomial == null)
                throw new ArgumentException("polynomial is null", nameof(polynomial));

            if (term == null)
                throw new ArgumentException("term is null", nameof(term));

            var result = (Polynomial)polynomial.Clone();
            result.Substract(term);
            return result;
        }
        public static Polynomial operator -(Polynomial polynomial, Polynomial addition)
        {
            if (polynomial == null)
                throw new ArgumentException("polynomial is null", nameof(polynomial));

            if (addition == null)
                throw new ArgumentException("addition is null", nameof(addition));

            var result = (Polynomial)polynomial.Clone();
            result.Substract(addition);
            return result;
        }


        public static Polynomial operator *(Polynomial polynomial, double number)
        {
            if (polynomial == null)
                throw new ArgumentException("polynomial is null", nameof(polynomial));

            var result = (Polynomial)polynomial.Clone();
            result.Multiply(number);
            return result;
        }
        public static Polynomial operator *(Polynomial polynomial, Term term)
        {
            if (polynomial == null)
                throw new ArgumentException("polynomial is null", nameof(polynomial));

            if (term == null)
                throw new ArgumentException("term is null", nameof(term));

            var result = (Polynomial)polynomial.Clone();
            result.Multiply(term);
            return result;
        }
        public static Polynomial operator *(Polynomial polynomial, Polynomial multiplier)
        {
            if (polynomial == null)
                throw new ArgumentException("polynomial is null", nameof(polynomial));

            if (multiplier == null)
                throw new ArgumentException("multiplier is null", nameof(multiplier));

            var result = (Polynomial)polynomial.Clone();
            result.Multiply(multiplier);
            return result;
        }

        public static Polynomial operator /(Polynomial polynomial, double divider)
        {
            if (polynomial == null)
                throw new ArgumentException("polynomial is null", nameof(polynomial));

            var result = (Polynomial)polynomial.Clone();
            result.Divide(divider);
            return result;
        }


        public static bool operator ==(Polynomial leftSide, Polynomial rightSide)
        {
            if (ReferenceEquals(leftSide, null))
                return false;

            return leftSide.Equals(rightSide);
        }

        public static bool operator !=(Polynomial leftSide, Polynomial rightSide) => !(leftSide == rightSide);

        public object Clone()
        {
            return new Polynomial(new List<Term>(_terms.Select(t => new Term(t.Power, t.Coefficient))));
        }

        public void ReduceSuchTerms()
            => _terms = this.GroupBy(f => f.Power)
                        .Select(g => new Term(g.Key, g.Sum(t => t.Coefficient)))
                        .ToList();

        public Polynomial GetReducePolynomial()
        {
            var result = (Polynomial)Clone();
            result.ReduceSuchTerms();
            return result;
        }

        public override string ToString()
            => String.Join(' ', this.OrderByDescending(t => t.Power));

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, null))
                return false;

            return obj.GetType() == GetType() && Equals((Polynomial)obj);
        }
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                foreach (var term in this)
                    hash += hash * 23 + term.GetHashCode();

                return hash;
            }

        }

        public bool Equals(Polynomial polynomial)
        {
            if (ReferenceEquals(polynomial, null))
                return false;

            if (ReferenceEquals(this, polynomial))
                return true;

            return this.SequenceEqual(polynomial);
            //return 
        }
        
    }
}
