namespace SistemZaZakazuvanje.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial51 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Appointments", "UserId", c => c.Int(nullable: false));
            AddColumn("dbo.Appointments", "ApplicationUser_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.AspNetUsers", "firstName", c => c.String());
            CreateIndex("dbo.Appointments", "ApplicationUser_Id");
            AddForeignKey("dbo.Appointments", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Appointments", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Appointments", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.AspNetUsers", "firstName");
            DropColumn("dbo.Appointments", "ApplicationUser_Id");
            DropColumn("dbo.Appointments", "UserId");
        }
    }
}
