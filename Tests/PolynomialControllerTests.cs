using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Service.Controllers;
using Shouldly;
using Xunit;

namespace Tests
{
    public class PolynomialControllerTests
    {
        [Theory]
        [InlineData("x^3+2x^2+x+1", "5x^4+x^2+1", "5x^4+x^3+3x^2+x+2")]
        [InlineData("x^15", "x^15", "2x^15")]
        [InlineData("x", "x", "2x")]
        [InlineData("x", "x^2", "x^2+x")]
        [InlineData("5", "5", "10")]
        [InlineData("0", "0", "0")]
        public void Add_ValidInputPolynomial_200CorrectOutputPolynomial(
            string inputA, string inputB, string expectedOutput)
        {
            var controller = new PolynomialController();

            var result = controller.Add(inputA, inputB);

            result.ShouldNotBeNull();
            var objectResult = result.ShouldBeOfType<OkObjectResult>();
            objectResult.StatusCode.ShouldBe(200);
            var dtoPolynomial = objectResult.Value.ShouldBeOfType<DTOs.Polynomial>();
            dtoPolynomial.StringForm.ShouldBe(expectedOutput);
        }

        [Theory]
        [InlineData("a", "x")]
        [InlineData("x", "a")]
        [InlineData("x+x#x", "a")]
        [InlineData("a", "x+x#x")]
        [InlineData("4^x+x^4", "x")]
        [InlineData("x", "4^x+x^4")]
        public void Add_InvalidPolynomial_400BadRequest(string inputA, string inputB)
        {
            var controller = new PolynomialController();

            var result = controller.Add(inputA, inputB);

            result.ShouldNotBeNull();
            var objectResult = result.ShouldBeOfType<BadRequestResult>();
            objectResult.StatusCode.ShouldBe(400);
        }
    }
}
