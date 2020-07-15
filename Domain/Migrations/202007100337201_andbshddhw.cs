namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class andbshddhw : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductDatas", "Discount", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProductDatas", "Discount");
        }
    }
}
