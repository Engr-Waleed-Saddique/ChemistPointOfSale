namespace SuperShaheenChemist.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MoreTablesAddedWithPFRelation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Distributors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MedicineTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Products", "MedicineTypeId", c => c.Int(nullable: false));
            AddColumn("dbo.Products", "UnitQuantity", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Products", "CustDiscount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Products", "DistributorId", c => c.Int(nullable: false));
            AddColumn("dbo.Products", "MinStock", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Products", "SideEffects", c => c.String());
            AddColumn("dbo.Products", "PackSize", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Products", "Location", c => c.String());
            AddColumn("dbo.Products", "UnitCost", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Products", "UnitRetail", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Products", "PackCost", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Products", "PackRetailCost", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Categories", "Name", c => c.String());
            AlterColumn("dbo.Products", "ProductName", c => c.String());
            AlterColumn("dbo.Products", "CompanyId", c => c.Int(nullable: false));
            AlterColumn("dbo.Products", "BatchNo", c => c.String());
            AlterColumn("dbo.Products", "BarCode", c => c.String());
            DropColumn("dbo.Categories", "Description");
            DropColumn("dbo.Products", "TotalUnitQuantity");
            DropColumn("dbo.Products", "Discount");
            DropColumn("dbo.Products", "PurchasePrice");
            DropColumn("dbo.Products", "RetailPrice");
            DropColumn("dbo.Products", "PerTabletCost");
            DropColumn("dbo.Products", "Margin");
            DropColumn("dbo.Products", "SupplierID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "SupplierID", c => c.String(nullable: false));
            AddColumn("dbo.Products", "Margin", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Products", "PerTabletCost", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Products", "RetailPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Products", "PurchasePrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Products", "Discount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Products", "TotalUnitQuantity", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Categories", "Description", c => c.String(maxLength: 500));
            AlterColumn("dbo.Products", "BarCode", c => c.String(nullable: false));
            AlterColumn("dbo.Products", "BatchNo", c => c.String(nullable: false));
            AlterColumn("dbo.Products", "CompanyId", c => c.String(nullable: false));
            AlterColumn("dbo.Products", "ProductName", c => c.String(nullable: false));
            AlterColumn("dbo.Categories", "Name", c => c.String(nullable: false, maxLength: 50));
            DropColumn("dbo.Products", "PackRetailCost");
            DropColumn("dbo.Products", "PackCost");
            DropColumn("dbo.Products", "UnitRetail");
            DropColumn("dbo.Products", "UnitCost");
            DropColumn("dbo.Products", "Location");
            DropColumn("dbo.Products", "PackSize");
            DropColumn("dbo.Products", "SideEffects");
            DropColumn("dbo.Products", "MinStock");
            DropColumn("dbo.Products", "DistributorId");
            DropColumn("dbo.Products", "CustDiscount");
            DropColumn("dbo.Products", "UnitQuantity");
            DropColumn("dbo.Products", "MedicineTypeId");
            DropTable("dbo.MedicineTypes");
            DropTable("dbo.Distributors");
        }
    }
}
