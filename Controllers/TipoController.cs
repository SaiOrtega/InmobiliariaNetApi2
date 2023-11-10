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
    public class TipoController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IConfiguration _config;
        public TipoController(DataContext contexto, IConfiguration config)
        {
            _context = contexto;
            _config = config;
        }


        [HttpGet("{id}")]
        [Authorize]
        public IActionResult getTipo(int id)
        {
            var tipo = _context.Tipo.FirstOrDefault(u => u.Id == id)?.NombreTipo;

            if (tipo == null)
            {
                return NotFound();
            }

            return Ok(tipo);
        }

    }
}