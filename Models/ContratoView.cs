using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InmobiliariaNetApi.Models
{
    public class ContratoView
    {
        public ContratoView()
        {

        }

        public ContratoView(Contrato Contrato, Inmueble Inmueble, Inquilino inquilino)
        {
            this.Id = Contrato.Id;
            this.Inmueble = Inmueble;
            this.FechaFinal = DateOnly.FromDateTime(Contrato.FechaFinal);
            this.FechaInicio = DateOnly.FromDateTime(Contrato.FechaInicio);
            this.Inquilino = inquilino;
            this.MontoMensual = Contrato.MontoMensual;
        }


        public int Id { get; set; }
        public Inquilino? Inquilino { get; set; }
        public Inmueble? Inmueble { get; set; }


        public DateOnly? FechaInicio { get; set; }
        public DateOnly? FechaFinal { get; set; }
        public decimal? MontoMensual { get; set; }

    }

}