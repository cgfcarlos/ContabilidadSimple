namespace ProyectoADAT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Siete : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CuentaBancarias", "numeroCuenta", c => c.String(nullable: false));
            AlterColumn("dbo.CuentaBancarias", "titularCuenta", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.CuentaBancarias", "entidadCuenta", c => c.String(nullable: false));
            AlterColumn("dbo.CuentaBancarias", "tipoCuenta", c => c.String(nullable: false));
            AlterColumn("dbo.CuentaBancarias", "paisDomiciliacion", c => c.String(nullable: false));
            AlterColumn("dbo.CuentaBancarias", "BIC", c => c.String(nullable: false));
            AlterColumn("dbo.Gastoes", "nombreGasto", c => c.String(nullable: false));
            AlterColumn("dbo.Gastoes", "tipoGasto", c => c.String(nullable: false));
            AlterColumn("dbo.Ingresoes", "nombreIngreso", c => c.String(nullable: false));
            AlterColumn("dbo.Ingresoes", "tipoIngreso", c => c.String(nullable: false));
            AlterColumn("dbo.Usuarios", "dniUsuario", c => c.String(nullable: false));
            AlterColumn("dbo.Usuarios", "nombreUsuario", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Usuarios", "apellidosUsuario", c => c.String(nullable: false));
            AlterColumn("dbo.Usuarios", "nickUsuario", c => c.String(nullable: false, maxLength: 25));
            AlterColumn("dbo.Usuarios", "passwordUsuario", c => c.String(nullable: false, maxLength: 16));
            AlterColumn("dbo.Usuarios", "emailUsuario", c => c.String(nullable: false));
            AlterColumn("dbo.Usuarios", "tlfUsuario", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Usuarios", "tlfUsuario", c => c.String());
            AlterColumn("dbo.Usuarios", "emailUsuario", c => c.String());
            AlterColumn("dbo.Usuarios", "passwordUsuario", c => c.String());
            AlterColumn("dbo.Usuarios", "nickUsuario", c => c.String());
            AlterColumn("dbo.Usuarios", "apellidosUsuario", c => c.String());
            AlterColumn("dbo.Usuarios", "nombreUsuario", c => c.String());
            AlterColumn("dbo.Usuarios", "dniUsuario", c => c.String());
            AlterColumn("dbo.Ingresoes", "tipoIngreso", c => c.String());
            AlterColumn("dbo.Ingresoes", "nombreIngreso", c => c.String());
            AlterColumn("dbo.Gastoes", "tipoGasto", c => c.String());
            AlterColumn("dbo.Gastoes", "nombreGasto", c => c.String());
            AlterColumn("dbo.CuentaBancarias", "BIC", c => c.String());
            AlterColumn("dbo.CuentaBancarias", "paisDomiciliacion", c => c.String());
            AlterColumn("dbo.CuentaBancarias", "tipoCuenta", c => c.String());
            AlterColumn("dbo.CuentaBancarias", "entidadCuenta", c => c.String());
            AlterColumn("dbo.CuentaBancarias", "titularCuenta", c => c.String());
            AlterColumn("dbo.CuentaBancarias", "numeroCuenta", c => c.String());
        }
    }
}
