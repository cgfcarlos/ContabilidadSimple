using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoADAT.Model
{
    public class Usuario
    {
        public Usuario()
        {
            //CuentasBancarias = new HashSet<CuentaBancaria>();
        }

        public int UsuarioId { get; set; }
        public string dniUsuario { get; set; }
        public string nombreUsuario { get; set; }
        public string apellidosUsuario { get; set; }
        public string nickUsuario { get; set; }
        public string passwordUsuario { get; set; }
        public string emailUsuario { get; set; }
        public string tlfUsuario { get; set; }

        //public virtual ICollection<CuentaBancaria> CuentasBancarias { get; set; }
    }
}
