using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoADAT.Model;

namespace ProyectoADAT.DAL
{
    public class RepositorioAcciones : GenericRepository<Accion>
    {
        public RepositorioAcciones(ContabilidadContext context) : base(context)
        {

        }
    }
}
