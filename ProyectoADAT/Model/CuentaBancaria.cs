using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoADAT.Model
{
    public class CuentaBancaria
    {
        public CuentaBancaria()
        {
            //Ingresos = new HashSet<Ingreso>();
            //Gastos = new HashSet<Gasto>();
            //Acciones = new HashSet<Accion>();
            //Depositos = new HashSet<Deposito>();
        }
        public int CuentaBancariaId { get; set; }
        public string numeroCuenta { get; set; }
        public string titularCuenta { get; set; }
        public string entidadCuenta { get; set; }
        public string tipoCuenta { get; set; }
        public string paisDomiciliacion { get; set; }
        public string BIC { get; set; }
        //public virtual ICollection<Ingreso> Ingresos { get; set; }
        //public virtual ICollection<Gasto> Gastos { get; set; }
        //public virtual ICollection<Accion> Acciones { get; set; }
        //public virtual ICollection<Deposito> Depositos { get; set; }
    }
}
