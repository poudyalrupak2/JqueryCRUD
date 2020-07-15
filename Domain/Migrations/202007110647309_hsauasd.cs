namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class hsauasd : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Products", "ProductData_Id", "dbo.ProductDatas");
            DropIndex("dbo.Products", new[] { "ProductData_Id" });
            DropColumn("dbo.Products", "ProductDataId");
            RenameColumn(table: "dbo.Products", name: "ProductData_Id", newName: "ProductDataId");
            AlterColumn("dbo.Products", "ProductDataId", c => c.Guid(nullable: false));
            AlterColumn("dbo.Products", "ProductDataId", c => c.Guid(nullable: false));
            CreateIndex("dbo.Products", "ProductDataId");
            AddForeignKey("dbo.Products", "ProductDataId", "dbo.ProductDatas", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "ProductDataId", "dbo.ProductDatas");
            DropIndex("dbo.Products", new[] { "ProductDataId" });
            AlterColumn("dbo.Products", "ProductDataId", c => c.Guid());
            AlterColumn("dbo.Products", "ProductDataId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Products", name: "ProductDataId", newName: "ProductData_Id");
            AddColumn("dbo.Products", "ProductDataId", c => c.Int(nullable: false));
            CreateIndex("dbo.Products", "ProductData_Id");
            AddForeignKey("dbo.Products", "ProductData_Id", "dbo.ProductDatas", "Id");
        }
    }
}
