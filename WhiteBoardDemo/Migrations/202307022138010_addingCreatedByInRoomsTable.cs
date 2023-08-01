namespace WhiteBoardDemo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingCreatedByInRoomsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Messages", "Time", c => c.DateTime(nullable: false));
            AddColumn("dbo.Rooms", "ApplicationUserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Rooms", "ApplicationUserId");
            AddForeignKey("dbo.Rooms", "ApplicationUserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Rooms", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.Rooms", new[] { "ApplicationUserId" });
            DropColumn("dbo.Rooms", "ApplicationUserId");
            DropColumn("dbo.Messages", "Time");
        }
    }
}
