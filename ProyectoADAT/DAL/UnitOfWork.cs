using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoADAT.DAL
{
    public class UnitOfWork
    {
        private ContabilidadContext context;
        private RepositorioUsuarios repositorioUsuarios;
        private RepositorioCuentasBancarias repositorioCuentasBancarias;
        private RepositorioIngresos repositorioIngresos;
        private RepositorioGastos repositorioGastos;
        private RepositorioAcciones repositorioAcciones;
        private RepositorioDepositos repositorioDepositos;

        public RepositorioUsuarios RepositorioUsuarios
        {
            get
            {
                if (this.repositorioUsuarios == null)
                {
                    repositorioUsuarios = new RepositorioUsuarios(context);
                }
                return repositorioUsuarios;
            }

          
        }

        public RepositorioCuentasBancarias RepositorioCuentasBancarias
        {
            get
            {
                if (this.repositorioCuentasBancarias == null)
                {
                    repositorioCuentasBancarias = new RepositorioCuentasBancarias(context);
                }
                return repositorioCuentasBancarias;
            }

        
        }

        public RepositorioIngresos RepositorioIngresos
        {
            get
            {
                if (this.repositorioIngresos == null)
                {
                    repositorioIngresos = new RepositorioIngresos(context);
                }
                return repositorioIngresos;
            }

        }

        public RepositorioGastos RepositorioGastos
        {
            get
            {
                if (repositorioGastos == null)
                {
                    repositorioGastos = new RepositorioGastos(context);
                }
                return repositorioGastos;
            }

            
        }

        public RepositorioAcciones RepositorioAcciones
        {
            get
            {
                if (repositorioAcciones == null)
                {
                    repositorioAcciones = new RepositorioAcciones(context);
                }
                return repositorioAcciones;
            }

       
        }

        public RepositorioDepositos RepositorioDepositos
        {
            get
            {
                if(repositorioDepositos == null)
                {
                    repositorioDepositos = new RepositorioDepositos(context);
                }
                return repositorioDepositos;
            }

        }
    }
}
