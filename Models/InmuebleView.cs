using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InmobiliariaNetApi.Models
{
    public class InmuebleView
    {
        public InmuebleView()
        {

        }

        public InmuebleView(Inmueble inmueble)
        {
            this.Id = inmueble.Id;
            this.Direccion = inmueble.Direccion;
            //this.Uso = inmueble.Uso;
            //this.Tipo = inmueble.Tipo;
            this.CantidadDeAmbientes = inmueble.CantidadDeAmbientes;
            this.Precio = inmueble.Precio;
            //this.PropietarioId = inmueble.PropietarioId;
            this.Estado = inmueble.Estado;


        }

        public int Id { get; set; }
        public string? Direccion { get; set; } = "";
        public String? Uso { get; set; }
        public String? Tipo { get; set; }
        public int? CantidadDeAmbientes { get; set; }
        public decimal? Precio { get; set; }
        public Propietario? Propietario { get; set; }
        public bool Estado { get; set; }
        public string Image { get; set; }

    }

}