using System.Collections.Generic;
using System.Linq;
using System.Text;
using Interfaces;
using static System.Int32;

namespace Models
{
    public class Polynomial : IPolynomial
    {
        public Polynomial(IField field, string literal)
        {
            Field = field;
            var coefficientsWithExponents = literal.Split('+');
            foreach (var coefficientsWithExponent in coefficientsWithExponents)
            {
                var exponentCoefficient = coefficientsWithExponent.Split('^');
                _exponentsCoefficients.Add(GetExponent(exponentCoefficient)
                    , GetCoefficient(exponentCoefficient));
            }
        }


        public IField Field { get; }
        public int Degree => _exponentsCoefficients.Keys.OrderByDescending(i => i).ToArray()[0];
        public string HexadecimalRepresentation { get; }
        public string BinaryRepresentation { get; }
        private readonly Dictionary<int, int> _exponentsCoefficients = new Dictionary<int, int>();

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
    }
}
