namespace SuperShaheenChemist.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StockInventoryTableAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StockInventries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        Received = c.Int(nullable: false),
                        Sale = c.Int(nullable: false),
                        Stock = c.Int(nullable: false),
                        Price = c.Int(nullable: false),
                        TotalAmount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.StockInventries");
        }
    }
}
