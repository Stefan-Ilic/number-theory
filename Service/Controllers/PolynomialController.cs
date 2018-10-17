﻿using System;
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
            //TODO hacky and stupid, use regex instead
            if (((polynomialA + "+" + polynomialB).Split('+')).Any(subExpression => !Regex.Match(subExpression, @"^(([0-9]*x\^[0-9]*)|(x)|([0-9]*)|(x\^([0-9]*)))$").Success))
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
    }
}