namespace ProyectoADAT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Auxiliar : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CuentaBancarias", "saldo", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.CuentaBancarias", "Usuario_UsuarioId", c => c.Int());
            CreateIndex("dbo.CuentaBancarias", "Usuario_UsuarioId");
            AddForeignKey("dbo.CuentaBancarias", "Usuario_UsuarioId", "dbo.Usuarios", "UsuarioId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CuentaBancarias", "Usuario_UsuarioId", "dbo.Usuarios");
            DropIndex("dbo.CuentaBancarias", new[] { "Usuario_UsuarioId" });
            DropColumn("dbo.CuentaBancarias", "Usuario_UsuarioId");
            DropColumn("dbo.CuentaBancarias", "saldo");
        }
    }
}
