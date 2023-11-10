using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using InmobiliariaNetApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace InmobiliariaNetApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsoController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IConfiguration _config;
        public UsoController(DataContext contexto, IConfiguration config)
        {
            _context = contexto;
            _config = config;
        }





        [HttpGet("{id}")]
        [Authorize]
        public IActionResult getUso(int id)
        {
            var uso = _context.Uso.FirstOrDefault(u => u.Id == id)?.NombreUso;

            if (uso == null)
            {
                return NotFound();
            }

            return Ok(uso);
        }
    }
}