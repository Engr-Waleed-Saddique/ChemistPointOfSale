namespace SuperShaheenChemist.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tables : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.PurchaseProducts");
            AddColumn("dbo.PurchaseProducts", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.PurchaseProducts", "ProductId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.PurchaseProducts", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.PurchaseProducts");
            AlterColumn("dbo.PurchaseProducts", "ProductId", c => c.Int(nullable: false, identity: true));
            DropColumn("dbo.PurchaseProducts", "Id");
            AddPrimaryKey("dbo.PurchaseProducts", "ProductId");
        }
    }
}
