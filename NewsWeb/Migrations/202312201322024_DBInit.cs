namespace NewsWeb.Models
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DBInit : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        id = c.Int(nullable: false),
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
        }
    }
}
