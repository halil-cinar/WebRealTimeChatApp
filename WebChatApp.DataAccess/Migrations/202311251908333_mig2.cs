namespace WebChatApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MessageEntities", "SenderIP", c => c.String());
            AddColumn("dbo.MessageEntities", "SendTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MessageEntities", "SendTime");
            DropColumn("dbo.MessageEntities", "SenderIP");
        }
    }
}
