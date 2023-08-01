namespace WhiteBoardDemo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingWhiteboardColumnInRoomTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Rooms", "WhiteboardData", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Rooms", "WhiteboardData");
        }
    }
}
