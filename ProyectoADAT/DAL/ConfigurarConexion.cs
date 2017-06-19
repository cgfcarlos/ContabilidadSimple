using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoADAT.DAL
{
    public class ConfigurarConexion
    {
        
        string nombreBDAT;
        string nombreUsuario = "sa";
        string password = "sql";

        public string NombreBDAT
        {
            get
            {
                return nombreBDAT;
            }

            set
            {
                nombreBDAT = value;
            }
        }

        public string NombreUsuario
        {
            get
            {
                return nombreUsuario;
            }
        }

        public string Password
        {
            get
            {
                return password;
            }
        }
    }
}
