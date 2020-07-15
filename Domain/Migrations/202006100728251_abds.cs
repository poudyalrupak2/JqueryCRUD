namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class abds : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Incomes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Income_ = c.String(),
                        Date = c.String(),
                        Description = c.String(),
                        DateCreated = c.DateTime(),
                        UserCreated = c.String(),
                        DateModified = c.DateTime(),
                        UserModified = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductName = c.String(),
                        Price = c.Int(),
                        Quantity = c.Int(),
                        ProductDataId = c.Int(nullable: false),
                        DateCreated = c.DateTime(),
                        UserCreated = c.String(),
                        DateModified = c.DateTime(),
                        UserModified = c.String(),
                        ProductData_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductDatas", t => t.ProductData_Id)
                .Index(t => t.ProductData_Id);
            
            CreateTable(
                "dbo.ProductDatas",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TotalPrice = c.String(),
                        Date = c.String(),
                        Details = c.String(),
                        DateCreated = c.DateTime(),
                        UserCreated = c.String(),
                        DateModified = c.DateTime(),
                        UserModified = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "ProductData_Id", "dbo.ProductDatas");
            DropIndex("dbo.Products", new[] { "ProductData_Id" });
            DropTable("dbo.ProductDatas");
            DropTable("dbo.Products");
            DropTable("dbo.Incomes");
        }
    }
}
