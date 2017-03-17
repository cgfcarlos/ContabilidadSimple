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

        public IEnumerable<Ingreso> GetFiltrado(String buscado)
        {
            if (!string.IsNullOrWhiteSpace(buscado))
            {
                return Get(filter: (a => a.tipoIngreso.ToUpper().Contains(buscado.ToUpper())
                                            || a.nombreIngreso.ToUpper().Contains(buscado.ToUpper())
                                            || a.fechaOperacion>(Convert.ToDateTime(buscado))
                                            || a.fechaOperacion<(Convert.ToDateTime(buscado))
                                            || a.cuantia>(Convert.ToDecimal(buscado))
                                            || a.cuantia<(Convert.ToDecimal(buscado))
                                            ));
            }
            else return Get();
        }
    }
}
