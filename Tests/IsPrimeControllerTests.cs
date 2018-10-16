using System.Collections.Generic;
using System.Text;
using DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.DataAnnotations.Internal;
using Service.Controllers;
using Shouldly;
using Xunit;
using static System.Int32;

namespace Tests
{
    public class IsPrimeControllerTests
    {
        [Theory]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(23)]
        public void IsPrime_KnownPrimeNumber_200True(int numberToTest)
        {
            var mockdal = new MockDal {LargestTestedNumber = numberToTest + 1};
            var controller = new IsPrimeController(mockdal);

            var result = controller.IsPrime(numberToTest);

            result.ShouldNotBeNull();
            var objectResult = result.ShouldBeOfType<OkObjectResult>();
            objectResult.StatusCode.ShouldBe(200);
            var primeResult = objectResult.Value.ShouldBeOfType<IsPrimeResult>();
            primeResult.Result.ShouldBe(true);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(4)]
        [InlineData(20)]
        public void IsPrime_KnownNotPrimeNumber_200False(int numberToTest)
        {
            var mockdal = new MockDal { LargestTestedNumber = numberToTest + 1 };
            var controller = new IsPrimeController(mockdal);

            var result = controller.IsPrime(numberToTest);

            result.ShouldNotBeNull();
            var objectResult = result.ShouldBeOfType<OkObjectResult>();
            objectResult.StatusCode.ShouldBe(200);
            var primeResult = objectResult.Value.ShouldBeOfType<IsPrimeResult>();
            primeResult.Result.ShouldBe(false);
        }

        [Theory]
        [InlineData(100)]
        public void IsPrime_UnknownNumber_404(int numberToTest)
        {
            var mockdal = new MockDal { LargestTestedNumber = numberToTest - 1 };
            var controller = new IsPrimeController(mockdal);

            var result = controller.IsPrime(numberToTest);

            result.ShouldNotBeNull();
            var objectResult = result.ShouldBeOfType<NotFoundResult>();
            objectResult.StatusCode.ShouldBe(404);
        }
    }
}
