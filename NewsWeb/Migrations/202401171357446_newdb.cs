namespace NewsWeb.Models
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newdb : DbMigration
    {
        public override void Up()
        {
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.HistoryNews", "UserID", "dbo.Users");
            DropIndex("dbo.HistoryNews", new[] { "UserID" });
            DropTable("dbo.HistoryNews");
        }
    }
}
