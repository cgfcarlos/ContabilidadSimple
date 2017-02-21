using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoADAT.Model;

namespace ProyectoADAT.DAL
{
    public class RepositorioDepositos : GenericRepository<Deposito>
    {
        public RepositorioDepositos(ContabilidadContext context) : base(context)
        {

        }
    }
}
