using System;
using System.Collections.Generic;
using System.Text;
using Interfaces;
using Models;
using Shouldly;
using Xunit;

namespace Tests
{
    public class PolynomialTests
    {
        [Theory]
        [InlineData("x^3+2x^2+x+1", 3)]
        [InlineData("x^3+x^3+2x^2+x+1", 3)]
        [InlineData("x^15", 15)]
        [InlineData("1+x+2x^2+x^3",3)]
        [InlineData("1", 0)]
        [InlineData("x",1)]
        [InlineData("5", 0)]
        [InlineData("0", 0)]
        [InlineData("4x", 1)]
        public void Degree(string literal, int expected)
        {
            var polynomial = new Polynomial(new Field(Set.R), literal);
            polynomial.Degree.ShouldBe(expected);
        }

        [Theory]
        [InlineData("x^3+2x^2+x+1", "x^3+2x^2+x+1")]
        [InlineData("x^3+x^3+2x^2+x+1", "2x^3+2x^2+x+1")]
        [InlineData("x^15", "x^15")]
        [InlineData("1+x+2x^2+x^3", "x^3+2x^2+x+1")]
        [InlineData("1", "1")]
        [InlineData("x", "x")]
        [InlineData("5", "5")]
        [InlineData("0", "0")]
        public void ToStringOverride(string literal, string expected)
        {
            var polynomial = new Polynomial(new Field(Set.R), literal);

            var polynomialAsString = polynomial.ToString();

            polynomialAsString.ShouldNotBeNullOrEmpty();
            polynomialAsString.ShouldBe(expected);
        }

        [Theory]
        [InlineData("x^3+2x^2+x+1", "5x^4+x^2+1", "5x^4+x^3+3x^2+x+2")]
        [InlineData("x^15", "x^15", "2x^15")]
        [InlineData("x", "x", "2x")]
        [InlineData("x", "x^2", "x^2+x")]
        [InlineData("5", "5", "10")]
        [InlineData("0", "0", "0")]
        [InlineData("2x", "2x", "4x")]
        public void Addition(string polynomialA, string polynomialB, string result)
        {
            var a = GetRealPolynomial(polynomialA);
            var b = GetRealPolynomial(polynomialB);

            var c = a + b;

            c.ToString().ShouldBe(result);
        }

        private static Polynomial GetRealPolynomial(string literal)
        {
            return new Polynomial(new Field(Set.R), literal);
        }
    }
}
