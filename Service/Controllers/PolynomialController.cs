using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using DTOs;
using Interfaces;

namespace Service.Controllers
{
    [Route("api/polynomial")]
    [ApiController]
    public class PolynomialController : ControllerBase
    {
        [HttpGet]
        [Route("add/{polynomialA}/{polynomialB}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Produces(typeof(DTOs.Polynomial))]
        public IActionResult Add([FromRoute] string polynomialA, string polynomialB)
        {
            if (!Validate(polynomialA) || !Validate(polynomialB))
            {
                return BadRequest();
            }
            
            var a = new Models.Polynomial(new Field(Set.R), polynomialA);
            var b = new Models.Polynomial(new Field(Set.R), polynomialB);

            var returnPolynomial = new DTOs.Polynomial
            {
                StringForm = (a+b).ToString()
            };

            return Ok(returnPolynomial);
        }

        private static bool Validate(string polynomial)
        {
            const string pattern = @"^((((-?)[0-9]+)|(x)|((-)?[0-9]*x)|(-?[0-9]*x\^[0-9]+))\+?)*(((-?)[0-9]+)|(x)|((-)?[0-9]*x)|(-?[0-9]*x\^[0-9]+))$";

            return Regex.Match(polynomial, pattern).Success;
        }
    }
}