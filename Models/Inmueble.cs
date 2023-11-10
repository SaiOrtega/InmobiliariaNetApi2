using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace InmobiliariaNetApi.Models
{
    public class Inmueble
    {
        public int Id { get; set; }
        public string? Direccion { get; set; } = "";
        public int Uso { get; set; }
        public int Tipo { get; set; }
        public int? CantidadDeAmbientes { get; set; }
        public decimal? Precio { get; set; }
        public int? PropietarioId { get; set; }
        public bool Estado { get; set; }
        public string Image { get; set; }

        [ForeignKey("Uso")]
        public virtual Uso? UsoNavigation { get; set; }

        [ForeignKey("Tipo")]
        public virtual Tipo? TipoNavigation { get; set; }


    }
}