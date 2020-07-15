namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class hsauasd1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "Price", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "Price", c => c.Int());
        }
    }
}
