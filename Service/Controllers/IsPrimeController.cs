using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DTOs;
using Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Service.Controllers
{
    [Route("api/isprime")]
    [ApiController]
    public class IsPrimeController : ControllerBase
    {
        public IsPrimeController(IDataAccessLayer dal)
        {
            _dal = dal;
        }

        private readonly IDataAccessLayer _dal;

        [HttpGet("{number}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Produces(typeof(IsPrimeResult))]
        public IActionResult IsPrime([FromRoute] long number)
        {
            return Ok(new IsPrimeResult {Result = IsPrimeCheck(number)});
        }

        private static bool IsPrimeCheck(long numbertocheck)
        {
            if (numbertocheck <= 1) return false;
            if (numbertocheck == 2) return true;

            var limit = Math.Ceiling(Math.Sqrt(numbertocheck));

            for (var i = 2; i <= limit; i++)
            {
                if (numbertocheck % i == 0) return false;
            }
            return true;
        }
    }
}