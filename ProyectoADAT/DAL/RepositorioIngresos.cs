using ProyectoADAT.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoADAT.DAL
{
    public class RepositorioIngresos : GenericRepository<Ingreso>
    {
        public RepositorioIngresos(ContabilidadContext context) : base(context)
        {

        }
    }
}
