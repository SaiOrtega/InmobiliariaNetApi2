using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using InmobiliariaNetApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;


namespace InmobiliariaNetApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InmuebleController : ControllerBase
    {

        private readonly DataContext _context;
        private readonly IConfiguration _config;
        private readonly IWebHostEnvironment environment;
        public InmuebleController(DataContext contexto, IConfiguration config, IWebHostEnvironment env)
        {
            _context = contexto;
            _config = config;
            environment = env;
        }



        [HttpGet()]
        [Authorize]
        public IActionResult getInmuebles()
        {
            var propietarioEmail = User.Identity.Name;
            var propietario = _context.Propietario.Where(x => x.Email == propietarioEmail).FirstOrDefault();

            var inmuebles = _context.Inmueble
             .Include(x => x.UsoNavigation)
             .Include(x => x.TipoNavigation)
             .Where(x => x.PropietarioId == propietario.Id)
             .ToList();

            var inmueblesV = inmuebles.Select(inmueble => new InmuebleView
            {
                Id = inmueble.Id,
                Direccion = inmueble.Direccion,
                Uso = inmueble.UsoNavigation.NombreUso,
                Tipo = inmueble.TipoNavigation.NombreTipo,
                CantidadDeAmbientes = inmueble.CantidadDeAmbientes,
                Precio = inmueble.Precio,
                Estado = inmueble.Estado,
                Image = inmueble.Image
            });

            //crear view y pasarle el inmueble y setearle los datos buscanos las tablas

            Console.WriteLine(inmuebles);
            return Ok(inmueblesV);
        }



        [HttpGet("fotoInmueble/{nombreImagen}")]
        //[Authorize]
        public IActionResult RecuperarImagenInm(string nombreImagen)
        {
            var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "uploads", nombreImagen);

            if (System.IO.File.Exists(imagePath))
            {
                var imageBytes = System.IO.File.ReadAllBytes(imagePath);
                return File(imageBytes, "image/jpeg");
            }
            else
            {
                return NotFound(); // Si la imagen no existe
            }
        }

        [HttpPut("CambiarEstado/{id}")]
        [Authorize]
        public IActionResult CambiarEstado(int id)
        {
            var propietarioEmail = User.Identity.Name;
            var propietario = _context.Propietario.SingleOrDefault(x => x.Email == propietarioEmail);

            var inmueble = _context.Inmueble.Where(u => u.Id == id).FirstOrDefault();

            if (inmueble == null || inmueble.PropietarioId != propietario.Id)
            {
                inmueble = new Inmueble(); ;
            }
            else
            {
                inmueble.Estado = !inmueble.Estado;
                _context.SaveChanges();

            }

            return Ok(inmueble);
        }



        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Inmueble>> Post([FromForm] Inmueble inmueble, IFormFile imagenFile)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Los datos del inmueble no son válidos.");
            }

            if (imagenFile != null && imagenFile.Length > 0)
            {
                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "uploads");
                string uniqueFileName = Guid.NewGuid().ToString() + ".jpg";
                string imagePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    await imagenFile.CopyToAsync(stream);
                }

                inmueble.Image = uniqueFileName;
            }

            // Obtén el ID del propietario actual basado en el usuario autenticado
            var propietarioEmail = User.Identity.Name;
            var propietario = _context.Propietario.SingleOrDefault(x => x.Email == propietarioEmail);

            if (propietario == null)
            {
                return BadRequest("No se pudo encontrar al propietario actual.");
            }

            inmueble.PropietarioId = propietario.Id;

            _context.Inmueble.Add(inmueble);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(getInmuebles), new { id = inmueble.Id }, inmueble);
        }


        // [HttpPost]
        // [Authorize]
        // public async Task<ActionResult> Crear([FromForm] Inmueble inmueble)
        // {
        //     try
        //     {
        //         var propietarioEmail = User.Identity.Name;
        //         var propietario = _context.Propietario.FirstAsync(x => x.Email == propietarioEmail);

        //         inmueble.PropietarioId = propietario.Id;
        //         await _context.Inmueble.AddAsync(inmueble);
        //         _context.SaveChanges();
        //         if (inmueble.Image != null && inmueble.Id > 0)
        //         {
        //             string wwPath = environment.WebRootPath;
        //             string path = Path.Combine(wwPath, "uploads");
        //             if (!Directory.Exists(path))
        //             {
        //                 Directory.CreateDirectory(path);
        //             }
        //             string fileName = "Inmueble_" + inmueble.Id + Path.GetExtension(inmueble.ImageFile.FileName);
        //             string pathCompleto = Path.Combine(path, fileName);
        //             inmueble.Image = Path.Combine("/uploads", fileName);
        //             using (FileStream stream = new FileStream(pathCompleto, FileMode.Create))
        //             {
        //                 inmueble.ImageFile.CopyTo(stream);
        //             }
        //             _context.Inmueble.Update(inmueble);
        //             await _context.SaveChangesAsync();
        //         }

        //         return CreatedAtAction(nameof(getInmuebles), new { id = inmueble.Id }, inmueble);
        //     }
        //     catch (Exception ex)
        //     {

        //         return BadRequest($"Error: {ex.Message}");
        //     }

        // }




    }
}
