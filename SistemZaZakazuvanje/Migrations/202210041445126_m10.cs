namespace SistemZaZakazuvanje.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m10 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        UserId = c.String(),
                        UserName = c.String(),
                        Comment_Id = c.Int(),
                        Review_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Comments", t => t.Comment_Id)
                .ForeignKey("dbo.Reviews", t => t.Review_Id)
                .Index(t => t.Comment_Id)
                .Index(t => t.Review_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "Review_Id", "dbo.Reviews");
            DropForeignKey("dbo.Comments", "Comment_Id", "dbo.Comments");
            DropIndex("dbo.Comments", new[] { "Review_Id" });
            DropIndex("dbo.Comments", new[] { "Comment_Id" });
            DropTable("dbo.Comments");
        }
    }
}
