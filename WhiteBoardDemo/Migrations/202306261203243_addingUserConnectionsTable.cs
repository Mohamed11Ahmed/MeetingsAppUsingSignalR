namespace WhiteBoardDemo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingUserConnectionsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserConnections",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ApplicationUserId = c.String(maxLength: 128),
                        ConnectionId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .Index(t => t.ApplicationUserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserConnections", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.UserConnections", new[] { "ApplicationUserId" });
            DropTable("dbo.UserConnections");
        }
    }
}
