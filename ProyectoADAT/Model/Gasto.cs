using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoADAT.Model
{
    public class Gasto
    {
        public int GastoId { get; set; }
        public string nombreGasto { get; set; }
        public string tipoGasto { get; set; }
        public decimal cuantia { get; set; }
        public DateTime fechaOperacion { get; set; }
        public DateTime fechaValor { get; set; }

        public virtual CuentaBancaria CuentaBancaria { get; set; }
    }
}
