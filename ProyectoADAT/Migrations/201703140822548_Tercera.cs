namespace ProyectoADAT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Tercera : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Usuarios", "imagenUsuario", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Usuarios", "imagenUsuario");
        }
    }
}
