using ProyectoADAT.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoADAT.DAL
{
    public class ContabilidadContext : DbContext
    {
        public ContabilidadContext() : base("ContabilidadContext")
        {

        }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<CuentaBancaria> CuentasBancarias { get; set; }
        public DbSet<Ingreso> Ingresos { get; set; }
        public DbSet<Gasto> Gastos { get; set; }
        public DbSet<Accion> Acciones { get; set; }
        public DbSet<Deposito> Depositos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
