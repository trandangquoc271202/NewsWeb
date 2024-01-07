namespace NewsWeb.Models
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Dbinit : DbMigration
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
                "dbo.Users",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        email = c.String(nullable: false, maxLength: 40),
                        password = c.String(nullable: false, maxLength: 40),
                        permission = c.String(nullable: false, maxLength: 10),
                        status = c.Boolean(nullable: false),
                        name = c.String(maxLength: 40),
                        phone = c.String(maxLength: 20),
                        address = c.String(maxLength: 40),
                        birthDay = c.String(maxLength: 40),
                    })
                .PrimaryKey(t => new { t.id, t.email, t.password, t.permission, t.status });
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Users");
            DropTable("dbo.Contacts");
            DropTable("dbo.Comments");
        }
    }
}
