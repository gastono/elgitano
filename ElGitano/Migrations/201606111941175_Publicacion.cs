namespace ElGitano.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Publicacion : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Publicacion",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ProductoID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Producto", t => t.ProductoID)
                .Index(t => t.ProductoID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Publicacion", "ProductoID", "dbo.Producto");
            DropIndex("dbo.Publicacion", new[] { "ProductoID" });
            DropTable("dbo.Publicacion");
        }
    }
}
