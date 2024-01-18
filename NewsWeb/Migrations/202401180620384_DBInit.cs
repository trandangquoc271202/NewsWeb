namespace NewsWeb.Models
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DBInit : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        idUser = c.Int(nullable: false),
                        link = c.String(nullable: false),
                        message = c.String(nullable: false),
                        dateTimeComment = c.DateTime(nullable: false),
                        allow = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Contacts",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false),
                        email = c.String(nullable: false),
                        subject = c.String(nullable: false),
                        message = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.HistoryNews",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        Link = c.String(),
                        Time = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        email = c.String(maxLength: 40),
                        password = c.String(maxLength: 40),
                        permission = c.String(maxLength: 10),
                        status = c.Boolean(nullable: false),
                        name = c.String(maxLength: 40),
                        phone = c.String(maxLength: 20),
                        address = c.String(maxLength: 40),
                        birthDay = c.String(maxLength: 40),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.HistoryNews", "UserID", "dbo.Users");
            DropIndex("dbo.HistoryNews", new[] { "UserID" });
            DropTable("dbo.Users");
            DropTable("dbo.HistoryNews");
            DropTable("dbo.Contacts");
            DropTable("dbo.Comments");
        }
    }
}
