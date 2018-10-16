using System.Collections.Generic;
using System.Text;
using DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.DataAnnotations.Internal;
using Service.Controllers;
using Shouldly;
using Xunit;
namespace Tests
{
    public class IsPrimeControllerTests
    {
        //TODO do we really need theories here?
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

        [Fact]
        public void IsPrime_UnknownNumber_404()
        {
            var mockdal = new MockDal { LargestTestedNumber = 100 - 1 };
            var controller = new IsPrimeController(mockdal);

            var result = controller.IsPrime(100);

            result.ShouldNotBeNull();
            var objectResult = result.ShouldBeOfType<NotFoundResult>();
            objectResult.StatusCode.ShouldBe(404);
        }
    }
}
