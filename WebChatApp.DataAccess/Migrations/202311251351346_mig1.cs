namespace WebChatApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ChatEntities",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        CreateIpAddress = c.String(),
                        UpdateIpAddress = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        UpdateTime = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MessageEntities",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        SenderName = c.String(),
                        Message = c.String(),
                        ChatId = c.Long(nullable: false),
                        CreateIpAddress = c.String(),
                        UpdateIpAddress = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        UpdateTime = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ChatEntities", t => t.ChatId)
                .Index(t => t.ChatId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MessageEntities", "ChatId", "dbo.ChatEntities");
            DropIndex("dbo.MessageEntities", new[] { "ChatId" });
            DropTable("dbo.MessageEntities");
            DropTable("dbo.ChatEntities");
        }
    }
}
