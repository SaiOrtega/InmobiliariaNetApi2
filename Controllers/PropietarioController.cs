using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using InmobiliariaNetApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
//using MimeKit;

namespace InmobiliariaNetApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PropietarioController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IConfiguration _config;
        private readonly IWebHostEnvironment _environment;
        public PropietarioController(DataContext contexto, IConfiguration config, IWebHostEnvironment environment)
        {
            _context = contexto;
            _config = config;
            _environment = environment;
        }




        [HttpPost("{login}")]
        public IActionResult login([FromForm] LoginView login)
        {
            var propietario = _context.Propietario.FirstOrDefault(x => x.Email == login.Email);
            if (propietario == null)
            {
                return NotFound();
            }

            String hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                          password: login.Clave,
                          salt: System.Text.Encoding.ASCII.GetBytes(_config["Salt"]),
                          prf: KeyDerivationPrf.HMACSHA1,
                          iterationCount: 1000,
                          numBytesRequested: 256 / 8));
            if (hashed != propietario.Contraseña)
            {
                return BadRequest("Contraseña incorrecta");
            }

            var key = new SymmetricSecurityKey(
                          System.Text.Encoding.ASCII.GetBytes(_config["TokenAuthentication:SecretKey"]));
            var credenciales = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, propietario.Email),
                        new Claim("id", propietario.Id.ToString()),
						//new Claim(ClaimTypes.Role, "Propietario"),
					};

            var token = new JwtSecurityToken(
                issuer: _config["TokenAuthentication:Issuer"],
                audience: _config["TokenAuthentication:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: credenciales
            );
            Console.WriteLine("Token: " + token);
            return Ok(new JwtSecurityTokenHandler().WriteToken(token));



        }

        [HttpGet()]
        [Authorize]
        public IActionResult MiPerfil()
        {
            var propietario = _context.Propietario.FirstOrDefault(x => x.Email == User.Identity.Name);
            if (propietario == null)
            {
                return NotFound();
            }

            PropietarioView propietarioView = new PropietarioView(propietario);
            Console.WriteLine(propietarioView);

            return Ok(propietarioView);
        }



        [HttpPut("{actualizar}")]
        [Authorize]
        public IActionResult ActualizarPerfil(Propietario nuevo)
        {
            //recupeeo propietario
            var propietario = _context.Propietario.Where(x => x.Email == User.Identity.Name).FirstOrDefault();

            if (propietario == null)
            {
                return NotFound();
            }
            //actualizo datos

            propietario.Telefono = nuevo.Telefono;
            propietario.Dni = nuevo.Dni;

            propietario.Email = nuevo.Email;
            propietario.Nombre = nuevo.Nombre;
            propietario.Apellido = nuevo.Apellido;
            if (nuevo.Contraseña != "")
            {
                String hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                            password: nuevo.Contraseña,
                            salt: System.Text.Encoding.ASCII.GetBytes(_config["Salt"]),
                            prf: KeyDerivationPrf.HMACSHA1,
                            iterationCount: 1000,
                            numBytesRequested: 256 / 8));
                propietario.Contraseña = hashed;
            }

            //guardo cambios
            _context.SaveChanges();
            //recupero nuevo perfil
            var perfilCambiado = _context.Propietario.Where(x => x.Email == User.Identity.Name).FirstOrDefault();
            PropietarioView propietarioView = new PropietarioView(perfilCambiado);
            //retorno el perfil
            return Ok(propietarioView);
        }


        [HttpGet("fotoPerfil/{nombreImagen}")]
        [Authorize]
        public IActionResult RecuperarImagen(string nombreImagen)
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

        ////Para Probar
        // // GET api/<controller>/token
        // [HttpGet("token")]
        // public async Task<IActionResult> Token()
        // {
        //     try
        //     { //este método si tiene autenticación, al entrar, generar clave aleatorio y enviarla por correo
        //         var perfil = new
        //         {
        //             Email = User.Identity.Name,
        //             Nombre = User.Claims.First(x => x.Type == "FullName").Value,
        //             Rol = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role).Value
        //         };
        //         Random rand = new Random(Environment.TickCount);
        //         string randomChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz0123456789";
        //         string nuevaClave = "";
        //         for (int i = 0; i < 8; i++)
        //         {
        //             nuevaClave += randomChars[rand.Next(0, randomChars.Length)];
        //         }//!Falta hacer el hash a la clave y actualizar el usuario con dicha clave
        //         var message = new MimeKit.MimeMessage();
        //         message.To.Add(new MailboxAddress(perfil.Nombre, perfil.Email));
        //         message.From.Add(new MailboxAddress("Sistema", _config["SMTPUser"]));
        //         message.Subject = "Prueba de Correo desde API";
        //         message.Body = new TextPart("html")
        //         {
        //             Text = @$"<h1>Hola</h1>
        // 			<p>¡Bienvenido, {perfil.Nombre}!</p>",//falta enviar la clave generada (sin hashear)
        //         };
        //         message.Headers.Add("Encabezado", "Valor");//solo si hace falta
        //         MailKit.Net.Smtp.SmtpClient client = new SmtpClient();
        //         client.ServerCertificateValidationCallback = (object sender,
        //             System.Security.Cryptography.X509Certificates.X509Certificate certificate,
        //             System.Security.Cryptography.X509Certificates.X509Chain chain,
        //             System.Net.Security.SslPolicyErrors sslPolicyErrors) =>
        //         { return true; };
        //         client.Connect("smtp.gmail.com", 465, MailKit.Security.SecureSocketOptions.Auto);
        //         client.Authenticate(_config["SMTPUser"], _config["SMTPPass"]);//estas credenciales deben estar en el user secrets
        //         await client.SendAsync(message);
        //         return Ok(perfil);
        //     }
        //     catch (Exception ex)
        //     {
        //         return BadRequest(ex.Message);
        //     }
        // }

        // // GET api/<controller>/email
        // [HttpPost("email")]
        // [AllowAnonymous]
        // public async Task<IActionResult> GetByEmail([FromForm] string email)
        // {
        //     try
        //     { //método sin autenticar, busca el propietario x email
        //         var entidad = await _context.Propietario.FirstOrDefaultAsync(x => x.Email == email);
        //         //para hacer: si el propietario existe, mandarle un email con un enlace con el token
        //         //ese enlace servirá para resetear la contraseña
        //         //Dominio sirve para armar el enlace, en local será la ip y en producción será el dominio www...
        //         var dominio = _environment.IsDevelopment() ? HttpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString() : "www.misitio.com";
        //         return entidad != null ? Ok(entidad) : NotFound();
        //     }
        //     catch (Exception ex)
        //     {
        //         return BadRequest(ex.Message);

        //     }
        // }
    }
}