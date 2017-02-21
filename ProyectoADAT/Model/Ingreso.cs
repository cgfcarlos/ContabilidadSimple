using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoADAT.Model
{
    public class Ingreso
    {
        public int IngresoId { get; set; }
        public string nombreIngreso { get; set; }
        public string tipoIngreso { get; set; }
        public decimal cuantia { get; set; }
        public DateTime fechaOperacion { get; set; }
        public DateTime fechaValor { get; set; }

        public virtual CuentaBancaria CuentaBancaria { get; set; }
    }
}
