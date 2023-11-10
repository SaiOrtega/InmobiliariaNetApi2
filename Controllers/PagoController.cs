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
    public class PagoController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IConfiguration _config;
        public PagoController(DataContext contexto, IConfiguration config)
        {
            _context = contexto;
            _config = config;
        }


        [HttpGet("{id}")]
        [Authorize]
        public IActionResult getPago(int id)
        {
            var pago = _context.Pago.Where(u => u.ContratoId == id).ToList();

            if (pago == null)
            {
                return NotFound();
            }
            Console.WriteLine(pago[0].Monto);
            return Ok(pago);
        }


    }
}