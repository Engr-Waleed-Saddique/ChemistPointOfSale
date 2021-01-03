namespace SuperShaheenChemist.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PrimaryForiegnKeyRelationAdded : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Products", "MedicineTypeId");
            CreateIndex("dbo.Products", "CompanyId");
            CreateIndex("dbo.Products", "DistributorId");
            CreateIndex("dbo.Products", "CategoryId");
            AddForeignKey("dbo.Products", "CategoryId", "dbo.Categories", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Products", "CompanyId", "dbo.Companies", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Products", "DistributorId", "dbo.Distributors", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Products", "MedicineTypeId", "dbo.MedicineTypes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "MedicineTypeId", "dbo.MedicineTypes");
            DropForeignKey("dbo.Products", "DistributorId", "dbo.Distributors");
            DropForeignKey("dbo.Products", "CompanyId", "dbo.Companies");
            DropForeignKey("dbo.Products", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Products", new[] { "CategoryId" });
            DropIndex("dbo.Products", new[] { "DistributorId" });
            DropIndex("dbo.Products", new[] { "CompanyId" });
            DropIndex("dbo.Products", new[] { "MedicineTypeId" });
        }
    }
}

