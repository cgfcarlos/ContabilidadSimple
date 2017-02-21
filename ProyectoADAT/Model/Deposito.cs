using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoADAT.Model
{
    public class Deposito
    {
        public int DepositoId { get; set; }
        public string nombreDeposito { get; set; }
        public string tipoDeposito { get; set; }
        public CuentaBancaria CuentaBancaria { get; set; }
    }
}
