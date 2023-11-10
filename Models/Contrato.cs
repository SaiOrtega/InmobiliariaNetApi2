using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InmobiliariaNetApi.Models
{
    public class Contrato
    {
        public int Id { get; set; }
        public int InmuebleId { get; set; }
        public int? InquilinoId { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFinal { get; set; }
        public decimal? MontoMensual { get; set; }

    }
}