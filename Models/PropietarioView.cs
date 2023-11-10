using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InmobiliariaNetApi.Models
{
    public class PropietarioView
    {
        public PropietarioView()
        {

        }

        public PropietarioView(Propietario propietario)
        {
            this.Id = propietario.Id;
            this.Email = propietario.Email;
            this.Nombre = propietario.Nombre;
            this.Apellido = propietario.Apellido;


            this.Dni = propietario.Dni;
            this.Telefono = propietario.Telefono;

            this.Nacimiento = propietario.Nacimiento;


        }
        public int Id { get; set; }
        public string? Nombre { get; set; } = "";
        public string? Apellido { get; set; } = "";

        public string? Email { get; set; } = "";


        public DateTime? Nacimiento { get; set; }
        public String? Dni { get; set; }
        public String? Telefono { get; set; }


    }
}