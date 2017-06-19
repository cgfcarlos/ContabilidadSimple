using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoADAT.Model;

namespace ProyectoADAT.DAL
{
    public class RepositorioOperaciones : GenericRepository<Operacion>
    {
        public RepositorioOperaciones(ContabilidadContext context) : base(context)
        {

        }

        public IEnumerable<Operacion> GetFiltrado(String buscado)
        {
            if (!string.IsNullOrWhiteSpace(buscado))
            {
                return Get(filter: (a => a.tipooperacion.ToUpper().Contains(buscado.ToUpper())
                                            || a.nombreoperacion.ToUpper().Contains(buscado.ToUpper())
                                            || a.fechaoperacion > (Convert.ToDateTime(buscado))
                                            || a.fechaoperacion < (Convert.ToDateTime(buscado))
                                            || a.cuantia > (Convert.ToDecimal(buscado))
                                            || a.cuantia < (Convert.ToDecimal(buscado))
                                            ));
            }
            else return Get();
        }
    }
}
