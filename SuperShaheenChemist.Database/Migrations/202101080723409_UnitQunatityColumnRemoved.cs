namespace SuperShaheenChemist.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UnitQunatityColumnRemoved : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Products", "UnitQuantity");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "UnitQuantity", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
