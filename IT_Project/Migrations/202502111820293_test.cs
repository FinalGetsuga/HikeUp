namespace IT_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ShoppingCarts",
                c => new
                    {
                        ShoppingCartId = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.ShoppingCartId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ShoppingCarts");
        }
    }
}
