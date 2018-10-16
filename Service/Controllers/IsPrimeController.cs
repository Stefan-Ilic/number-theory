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
        [Produces(typeof(IsPrimeResult))]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult IsPrime(long number)
        {
            var result = _dal.IsPrime(number);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(new IsPrimeResult{Result = result.Value});
        }
    }
}