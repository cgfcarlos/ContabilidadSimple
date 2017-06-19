using ProyectoADAT.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using ProyectoADAT.Migrations;
using System.IO;

namespace ProyectoADAT.DAL
{
    public class ContabilidadContext : DbContext
    {
        ConfigurarConexion configurar = new ConfigurarConexion();
        public ContabilidadContext() : base("ContabilidadContext")
        {
            /*if (!Database.Exists())
            {
                Database.SetInitializer(new CrearDB());
                //Database.SetInitializer(new MigrateDatabaseToLatestVersion<ContabilidadContext,Migrations.Configuration>());
                //Database.Create();
            }
            else
            {
                //Database.SetInitializer(new MigrateDatabaseToLatestVersion<ContabilidadContext, Migrations.Configuration>());
            }*/
        }



        class CrearDB : CreateDatabaseIfNotExists<ContabilidadContext>
        {
            protected override void Seed(ContabilidadContext context)
            {
                //if (MainWindow.u.RepositorioUsuarios.Single(a => a.nickUsuario == "admin") == null)
                //{

                Usuario user = new Usuario();
                user.nombreusuario = "admin";
                user.nickusuario = "admin";
                user.emailusuario = "admin@gmail.com";
                user.apellidosusuario = "admin";
                user.dniusuario = "00000000F";
                user.tlfusuario = "636252141";
                user.passwordusuario = "Admin12345.";
                context.Usuarios.Add(user);

                if (!Directory.Exists(@".\..\..\Usuarios"))
                {
                    Directory.CreateDirectory(@".\..\..\Usuarios");
                }

                //MainWindow.u.RepositorioUsuarios.Create(user);

                CuentaBancaria c = new CuentaBancaria();
                c.numerocuenta = "ES000000000000000000";
                string carpeta = c.numerocuenta;
                c.paisdomiciliacion = "NaN";
                c.entidadcuenta = "Nan";
                c.bic = "12345678";
                c.tipocuenta = "Corriente";
                c.titularcuenta = "admin admin";
                c.saldo = 25000;
                c.Usuario = user;
                //MainWindow.u.RepositorioCuentasBancarias.Create(c);
                context.CuentasBancarias.Add(c);

                if (!Directory.Exists(@".\..\..\Usuarios\" + c.numerocuenta))
                {
                    Directory.CreateDirectory(@".\..\..\Usuarios\" + carpeta);
                }

                Operacion i = new Operacion();
                i.nombreoperacion = "Ingreso Ejemplo";
                i.tipooperacion = "Ingreso";
                i.fechaoperacion = DateTime.Now.Date;
                i.fechavalor = DateTime.Now.Date;
                i.cuantia = 1000;
                i.CuentaBancaria = c;
                context.Operaciones.Add(i);
                //MainWindow.u.RepositorioIngresos.Create(i);

                Operacion g = new Operacion();
                g.nombreoperacion = "Gasto Ejemplo";
                g.tipooperacion = "Gasto";
                g.fechaoperacion = DateTime.Now.Date;
                g.fechavalor = DateTime.Now.Date;
                g.cuantia = -250;
                g.CuentaBancaria = c;
                context.Operaciones.Add(g);
                //MainWindow.u.RepositorioGastos.Create(g);

                //base.Seed(context);
            }
        }


        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<CuentaBancaria> CuentasBancarias { get; set; }
        //public DbSet<Ingreso> Ingresos { get; set; }
        public DbSet<Operacion> Operaciones { get; set; }
        //public DbSet<Accion> Acciones { get; set; }
        //public DbSet<Deposito> Depositos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}
