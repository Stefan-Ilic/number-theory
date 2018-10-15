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
        [InlineData("x^15", 15)]
        [InlineData("1+x+2x^2+x^3",3)]
        [InlineData("1", 0)]
        [InlineData("x",1)]
        [InlineData("5", 0)]
        public void Polynomial_Correct_Degree(string literal, int expected)
        {
            var polynomial = new Polynomial(new Field(Set.R), literal);
            polynomial.Degree.ShouldBe(expected);
        }
    }
}
