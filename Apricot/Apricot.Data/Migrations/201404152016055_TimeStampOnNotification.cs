namespace Apricot.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TimeStampOnNotification : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Notifications", "TimeStamp", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Notifications", "TimeStamp");
        }
    }
}
