namespace ElGitano.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cambioproductoeimagenes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Image",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Url = c.String(),
                        ThumnailUrl = c.String(),
                        Producto_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Producto", t => t.Producto_ID)
                .Index(t => t.Producto_ID);
            
            DropColumn("dbo.Producto", "Url");
            DropColumn("dbo.Producto", "ThumnailUrl");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Producto", "ThumnailUrl", c => c.String());
            AddColumn("dbo.Producto", "Url", c => c.String());
            DropForeignKey("dbo.Image", "Producto_ID", "dbo.Producto");
            DropIndex("dbo.Image", new[] { "Producto_ID" });
            DropTable("dbo.Image");
        }
    }
}
