namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class hgdsasda : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Logins",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        RandomPass = c.String(),
                        PasswordHash = c.Binary(),
                        PasswordSalt = c.Binary(),
                        Role = c.String(),
                        Status = c.Boolean(nullable: false),
                        LastLogin = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PersonalInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(),
                        CompanyName = c.String(),
                        DateCreated = c.DateTime(),
                        UserCreated = c.String(),
                        DateModified = c.DateTime(),
                        UserModified = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PersonalInfoes");
            DropTable("dbo.Logins");
        }
    }
}
