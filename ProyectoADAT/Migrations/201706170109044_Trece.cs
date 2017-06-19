namespace ProyectoADAT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Trece : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.cuentabancaria",
                c => new
                    {
                        cuentabancariaid = c.Int(nullable: false, identity: true),
                        numerocuenta = c.String(nullable: false, unicode: false),
                        titularcuenta = c.String(nullable: false, maxLength: 200, storeType: "nvarchar"),
                        entidadcuenta = c.String(nullable: false, unicode: false),
                        tipocuenta = c.String(nullable: false, unicode: false),
                        paisdomiciliacion = c.String(nullable: false, unicode: false),
                        bic = c.String(nullable: false, unicode: false),
                        saldo = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Usuario_usuarioid = c.Int(),
                    })
                .PrimaryKey(t => t.cuentabancariaid)
                .ForeignKey("dbo.usuario", t => t.Usuario_usuarioid)
                .Index(t => t.Usuario_usuarioid);
            
            CreateTable(
                "dbo.operacion",
                c => new
                    {
                        operacionid = c.Int(nullable: false, identity: true),
                        nombreoperacion = c.String(nullable: false, unicode: false),
                        tipooperacion = c.String(nullable: false, unicode: false),
                        cuantia = c.Decimal(nullable: false, precision: 18, scale: 2),
                        fechaoperacion = c.DateTime(nullable: false, precision: 0),
                        fechavalor = c.DateTime(nullable: false, precision: 0),
                        CuentaBancaria_cuentabancariaid = c.Int(),
                    })
                .PrimaryKey(t => t.operacionid)
                .ForeignKey("dbo.cuentabancaria", t => t.CuentaBancaria_cuentabancariaid)
                .Index(t => t.CuentaBancaria_cuentabancariaid);
            
            CreateTable(
                "dbo.usuario",
                c => new
                    {
                        usuarioid = c.Int(nullable: false, identity: true),
                        dniusuario = c.String(nullable: false, unicode: false),
                        nombreusuario = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                        apellidosusuario = c.String(nullable: false, unicode: false),
                        nickusuario = c.String(nullable: false, maxLength: 25, storeType: "nvarchar"),
                        passwordusuario = c.String(nullable: false, maxLength: 16, storeType: "nvarchar"),
                        emailusuario = c.String(nullable: false, unicode: false),
                        tlfusuario = c.String(nullable: false, unicode: false),
                    })
                .PrimaryKey(t => t.usuarioid);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.cuentabancaria", "Usuario_usuarioid", "dbo.usuario");
            DropForeignKey("dbo.operacion", "CuentaBancaria_cuentabancariaid", "dbo.cuentabancaria");
            DropIndex("dbo.operacion", new[] { "CuentaBancaria_cuentabancariaid" });
            DropIndex("dbo.cuentabancaria", new[] { "Usuario_usuarioid" });
            DropTable("dbo.usuario");
            DropTable("dbo.operacion");
            DropTable("dbo.cuentabancaria");
        }
    }
}
