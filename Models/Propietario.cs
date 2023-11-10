using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InmobiliariaNetApi.Models
{
    public class Propietario
    {
        public int Id { get; set; }
        public string? Nombre { get; set; } = "";
        public string? Apellido { get; set; } = "";

        public string? Email { get; set; } = "";
        public string? Contraseña { get; set; } = "";

        public DateTime? Nacimiento { get; set; }
        public String? Dni { get; set; }
        public String? Telefono { get; set; }

    }
}