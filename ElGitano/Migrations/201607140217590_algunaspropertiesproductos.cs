namespace ElGitano.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class algunaspropertiesproductos : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Producto", "CategoriaID", c => c.Int(nullable: false));
            AddColumn("dbo.Producto", "SubcategoriaID", c => c.Int(nullable: false));
            AddColumn("dbo.Producto", "Titulo", c => c.String());
            CreateIndex("dbo.Producto", "CategoriaID");
            CreateIndex("dbo.Producto", "SubcategoriaID");
            AddForeignKey("dbo.Producto", "CategoriaID", "dbo.Categoria", "ID");
            AddForeignKey("dbo.Producto", "SubcategoriaID", "dbo.Subcategoria", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Producto", "SubcategoriaID", "dbo.Subcategoria");
            DropForeignKey("dbo.Producto", "CategoriaID", "dbo.Categoria");
            DropIndex("dbo.Producto", new[] { "SubcategoriaID" });
            DropIndex("dbo.Producto", new[] { "CategoriaID" });
            DropColumn("dbo.Producto", "Titulo");
            DropColumn("dbo.Producto", "SubcategoriaID");
            DropColumn("dbo.Producto", "CategoriaID");
        }
    }
}
