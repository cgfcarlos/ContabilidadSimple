using ProyectoADAT.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoADAT.DAL
{
    public class RepositorioCuentasBancarias : GenericRepository<CuentaBancaria>
    {
        public RepositorioCuentasBancarias(ContabilidadContext context) : base(context)
        {

        }
    }
}
