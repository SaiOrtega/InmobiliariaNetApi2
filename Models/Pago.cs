using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace InmobiliariaNetApi.Models
{
    public class Pago
    {
        public int Id { get; set; }
        public int ContratoId { get; set; }
        public int? Numero { get; set; }

        public DateTime? FechaPago { get; set; }
        public Decimal Monto { get; set; }

    }
}