using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using InmobiliariaNetApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace InmobiliariaNetApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InquilinoController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IConfiguration _config;
        public InquilinoController(DataContext contexto, IConfiguration config)
        {
            _context = contexto;
            _config = config;
        }

        [HttpGet("{id}")]
        // [Authorize]
        public IActionResult getInquilino(int id)
        {
            var inquilino = _context.Inquilino.FirstOrDefault(x => x.Id == id);
            if (inquilino == null)
            {
                return NotFound();

            }
            return Ok(inquilino);
        }


    }
}