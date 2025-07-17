namespace IT_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductsShoppingCartRelationship : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ShoppingCartProducts",
                c => new
                    {
                        ShoppingCart_ShoppingCartId = c.Int(nullable: false),
                        Product_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ShoppingCart_ShoppingCartId, t.Product_Id })
                .ForeignKey("dbo.ShoppingCarts", t => t.ShoppingCart_ShoppingCartId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.Product_Id, cascadeDelete: true)
                .Index(t => t.ShoppingCart_ShoppingCartId)
                .Index(t => t.Product_Id);
            
            AddColumn("dbo.ShoppingCarts", "UserId", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ShoppingCartProducts", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.ShoppingCartProducts", "ShoppingCart_ShoppingCartId", "dbo.ShoppingCarts");
            DropIndex("dbo.ShoppingCartProducts", new[] { "Product_Id" });
            DropIndex("dbo.ShoppingCartProducts", new[] { "ShoppingCart_ShoppingCartId" });
            DropColumn("dbo.ShoppingCarts", "UserId");
            DropTable("dbo.ShoppingCartProducts");
        }
    }
}
