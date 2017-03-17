namespace ProyectoADAT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Sexta : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CuentaBancarias", "titularCuenta", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CuentaBancarias", "titularCuenta", c => c.String(nullable: false));
        }
    }
}
