namespace ElGitano.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class producto : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Producto",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UsuarioID = c.Int(nullable: false),
                        Descripcion = c.String(),
                        Url = c.String(),
                        ThumnailUrl = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Producto");
        }
    }
}
