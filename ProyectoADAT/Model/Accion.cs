using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoADAT.Model
{
    public class Accion
    {
        public int AccionId { get; set; }
        public string nombreEmpresa { get; set; }
        public decimal precio { get; set; }
        public decimal comisionCompra { get; set; }
        public virtual CuentaBancaria CuentaBancaria { get; set; }
    }
}
