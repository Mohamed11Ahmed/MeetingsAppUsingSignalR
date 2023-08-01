namespace WhiteBoardDemo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingMessagesTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        ApplicationUserId = c.String(maxLength: 128),
                        roomId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .ForeignKey("dbo.Rooms", t => t.roomId, cascadeDelete: true)
                .Index(t => t.ApplicationUserId)
                .Index(t => t.roomId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Messages", "roomId", "dbo.Rooms");
            DropForeignKey("dbo.Messages", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.Messages", new[] { "roomId" });
            DropIndex("dbo.Messages", new[] { "ApplicationUserId" });
            DropTable("dbo.Messages");
        }
    }
}
