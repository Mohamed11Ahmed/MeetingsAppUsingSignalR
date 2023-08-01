namespace WhiteBoardDemo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingRoomClasses : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RoomMembers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoomId = c.Int(nullable: false),
                        ApplicationUserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .ForeignKey("dbo.Rooms", t => t.RoomId, cascadeDelete: true)
                .Index(t => t.RoomId)
                .Index(t => t.ApplicationUserId);
            
            CreateTable(
                "dbo.Rooms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoomName = c.String(nullable: false, maxLength: 200),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RoomMembers", "RoomId", "dbo.Rooms");
            DropForeignKey("dbo.RoomMembers", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.RoomMembers", new[] { "ApplicationUserId" });
            DropIndex("dbo.RoomMembers", new[] { "RoomId" });
            DropTable("dbo.Rooms");
            DropTable("dbo.RoomMembers");
        }
    }
}
