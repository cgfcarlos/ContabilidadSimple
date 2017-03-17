namespace ProyectoADAT.Migrations
{
    using Model;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ProyectoADAT.DAL.ContabilidadContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(ProyectoADAT.DAL.ContabilidadContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            if (MainWindow.u.RepositorioUsuarios.Single(a => a.nickUsuario == "admin") == null)
            {
                Usuario user = new Usuario();
                user.nombreUsuario = "admin";
                user.nickUsuario = "admin";
                user.emailUsuario = "admin@gmail.com";
                user.apellidosUsuario = "admin";
                user.dniUsuario = "00000000F";
                user.tlfUsuario = "636252141";
                user.passwordUsuario = "admin";

                if (!Directory.Exists(@".\..\..\Usuarios"))
                {
                    Directory.CreateDirectory(@".\..\..\Usuarios");
                }

                MainWindow.u.RepositorioUsuarios.Create(user);

                CuentaBancaria c = new CuentaBancaria();
                c.numeroCuenta = "00000000";
                string carpeta = c.numeroCuenta;
                c.paisDomiciliacion = "NaN";
                c.entidadCuenta = "Nan";
                c.BIC = "12345678";
                c.tipoCuenta = "Corriente";
                c.titularCuenta = "admin admin";
                c.saldo = 25000;
                c.Usuario = MainWindow.u.RepositorioUsuarios.Single(a => a.nickUsuario == "admin");
                MainWindow.u.RepositorioCuentasBancarias.Create(c);

                if (!Directory.Exists(@".\..\..\Usuarios\" + c.numeroCuenta))
                {
                    Directory.CreateDirectory(@".\..\..\Usuarios\" + carpeta);
                }

                Ingreso i = new Ingreso();
                i.nombreIngreso = "Ingreso Ejemplo";
                i.tipoIngreso = "Nomina Ejemplo";
                i.fechaOperacion = DateTime.Now.Date;
                i.fechaValor = DateTime.Now.Date;
                i.cuantia = 1000;
                i.CuentaBancaria = c;

                MainWindow.u.RepositorioIngresos.Create(i);

                Gasto g = new Gasto();
                g.nombreGasto = "Gasto Ejemplo";
                g.tipoGasto = "Efectivo Ejemplo";
                g.fechaOperacion = DateTime.Now.Date;
                g.fechaValor= DateTime.Now.Date;
                g.cuantia = -250;
                g.CuentaBancaria = c;

                MainWindow.u.RepositorioGastos.Create(g);
            }
        }
    }
}
