namespace ElGitano.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class catsubcat : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categoria",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Subcategoria",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(),
                        CategoriaID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Categoria", t => t.CategoriaID)
                .Index(t => t.CategoriaID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Subcategoria", "CategoriaID", "dbo.Categoria");
            DropIndex("dbo.Subcategoria", new[] { "CategoriaID" });
            DropTable("dbo.Subcategoria");
            DropTable("dbo.Categoria");
        }
    }
}
