using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace InmobiliariaNetApi.Models
{
    public class Inquilino
    {
        public int Id { get; set; }
        public String? Dni { get; set; }
        public String? Direccion { get; set; } = "";
        public String? Nombre { get; set; }
        public String? Apellido { get; set; }
        public String? telefono { get; set; }
        public String? Email { get; set; }
        public DateTime? Nacimiento { get; set; }
        public String? NombreGarante { get; set; }
        public String? TelefonoGarante { get; set; }

    }
}