using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoADAT.Model;

namespace ProyectoADAT.DAL
{
    public class RepositorioGastos : GenericRepository<Gasto>
    {
        public RepositorioGastos(ContabilidadContext context) : base(context)
        {

        }

        public IEnumerable<Gasto> GetFiltrado(String buscado)
        {
            if (!string.IsNullOrWhiteSpace(buscado))
            {
                return Get(filter: (a => a.tipoGasto.ToUpper().Contains(buscado.ToUpper())
                                            || a.nombreGasto.ToUpper().Contains(buscado.ToUpper())
                                            || a.fechaOperacion > (Convert.ToDateTime(buscado))
                                            || a.fechaOperacion < (Convert.ToDateTime(buscado))
                                            || a.cuantia > (Convert.ToDecimal(buscado))
                                            || a.cuantia < (Convert.ToDecimal(buscado))
                                            ));
            }
            else return Get();
        }
    }
}
