namespace IT_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserShoppingCartRelationship : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "ShoppingCart_ShoppingCartId", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "ShoppingCart_ShoppingCartId");
            AddForeignKey("dbo.AspNetUsers", "ShoppingCart_ShoppingCartId", "dbo.ShoppingCarts", "ShoppingCartId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "ShoppingCart_ShoppingCartId", "dbo.ShoppingCarts");
            DropIndex("dbo.AspNetUsers", new[] { "ShoppingCart_ShoppingCartId" });
            DropColumn("dbo.AspNetUsers", "ShoppingCart_ShoppingCartId");
        }
    }
}
