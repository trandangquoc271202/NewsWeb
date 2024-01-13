namespace NewsWeb.Models
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DbInit2 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Users");
            AlterColumn("dbo.Users", "email", c => c.String(maxLength: 40));
            AlterColumn("dbo.Users", "password", c => c.String(maxLength: 40));
            AlterColumn("dbo.Users", "permission", c => c.String(maxLength: 10));
            AddPrimaryKey("dbo.Users", "id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Users");
            AlterColumn("dbo.Users", "permission", c => c.String(nullable: false, maxLength: 10));
            AlterColumn("dbo.Users", "password", c => c.String(nullable: false, maxLength: 40));
            AlterColumn("dbo.Users", "email", c => c.String(nullable: false, maxLength: 40));
            AddPrimaryKey("dbo.Users", new[] { "id", "email", "password", "permission", "status" });
        }
    }
}
