namespace NewsWeb.Models
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DBInit2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        id = c.Int(nullable: false),
                        idUser = c.Int(nullable: false),
                        link = c.String(nullable: false),
                        message = c.String(nullable: false),
                        dateTimeComment = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Comments");
        }
    }
}
