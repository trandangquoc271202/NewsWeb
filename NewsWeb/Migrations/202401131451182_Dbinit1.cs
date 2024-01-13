namespace NewsWeb.Models
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Dbinit1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "allow", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Comments", "allow");
        }
    }
}
