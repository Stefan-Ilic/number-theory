using System.Collections.Generic;
using System.Linq;
using System.Text;
using Interfaces;
using static System.Int32;

namespace Models
{
    //TODO minus
    public class Polynomial
    {
        public Polynomial(Field field, string literal)
        {
            Field = field;
            var coefficientsWithExponents = literal.Split('+');
            foreach (var coefficientsWithExponent in coefficientsWithExponents)
            {
                var exponentCoefficient = coefficientsWithExponent.Split('^');

                AddToExponentsCoefficients(
                    GetExponent(exponentCoefficient)
                    , GetCoefficient(exponentCoefficient));


                //_exponentsCoefficients.Add(GetExponent(exponentCoefficient)
                //    , GetCoefficient(exponentCoefficient));
            }
        }

        private void AddToExponentsCoefficients(int exponent, int coefficient)
        {
            if (_exponentsCoefficients.ContainsKey(exponent))
            {
                _exponentsCoefficients[exponent] += coefficient;
                return;
            }
            _exponentsCoefficients.Add(exponent, coefficient);
        }


        public override string ToString()
        {
            var builder = new StringBuilder();
            foreach (var pair in _exponentsCoefficients.Reverse())
            {
                builder.Append(GetSimplePolynomial(pair.Key, pair.Value)).Append("+");
            }
            builder.Length--;
            return builder.ToString();
        }

        private static string GetSimplePolynomial(int exponent, int coefficient)
        {
            if (coefficient == 1 && exponent == 0) { return "1"; }
            var builder = new StringBuilder();
            builder.Append(coefficient == 1 ? "" : coefficient.ToString());
            switch (exponent)
            {
                case 0:
                    return builder.ToString();
                case 1:
                    builder.Append("x");
                    return builder.ToString();
                default:
                    builder.Append("x^").Append(exponent);
                    return builder.ToString();
            }
        }

        public Field Field { get; }
        public int Degree => _exponentsCoefficients.Keys.OrderByDescending(i => i).ToArray()[0];
        public string HexadecimalRepresentation { get; } = "";
        public string BinaryRepresentation { get; } = "";
        private readonly SortedDictionary<int, int> _exponentsCoefficients = new SortedDictionary<int, int>();

        private static int GetExponent(IReadOnlyList<string> coefficientWithExponent)
        {
            if (coefficientWithExponent.Count != 2)
            {
                return coefficientWithExponent[0].TrimEnd('x') == "" ? 1 : 0;
            }
            TryParse(coefficientWithExponent[1], out var exponent);
            return exponent;
        }

        private static int GetCoefficient(IReadOnlyList<string> coefficientWithExponent)
        {
            var coefficientWithoutVariable = coefficientWithExponent[0].TrimEnd('x');
            TryParse(coefficientWithoutVariable, out var coefficient);
            return coefficientWithoutVariable == "" ? 1 : coefficient;
        }

        public static Polynomial operator+ (Polynomial a, Polynomial b)
        {
            foreach (var pair in b._exponentsCoefficients)
            {
                if (a._exponentsCoefficients.ContainsKey(pair.Key))
                {
                    a._exponentsCoefficients[pair.Key] += pair.Value;
                }
                else
                {
                    a._exponentsCoefficients.Add(pair.Key, pair.Value);
                }
            }

            return a;
        }
    }
}
