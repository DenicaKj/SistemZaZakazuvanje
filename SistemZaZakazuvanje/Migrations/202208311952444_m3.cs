 namespace SistemZaZakazuvanje.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Cancelleds", "appointment_Id", "dbo.Appointments");
            DropIndex("dbo.Cancelleds", new[] { "appointment_Id" });
            AddColumn("dbo.Cancelleds", "appointment_StartTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Cancelleds", "appointment_EndTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Cancelleds", "appointment_UslugaId", c => c.Int(nullable: false));
            AddColumn("dbo.Cancelleds", "appointment_Usluga", c => c.String());
            AddColumn("dbo.Cancelleds", "appointment_Status", c => c.String());
            AddColumn("dbo.Cancelleds", "appointment_UserId", c => c.String());
            DropColumn("dbo.Cancelleds", "appointment_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Cancelleds", "appointment_Id", c => c.Int());
            DropColumn("dbo.Cancelleds", "appointment_UserId");
            DropColumn("dbo.Cancelleds", "appointment_Status");
            DropColumn("dbo.Cancelleds", "appointment_Usluga");
            DropColumn("dbo.Cancelleds", "appointment_UslugaId");
            DropColumn("dbo.Cancelleds", "appointment_EndTime");
            DropColumn("dbo.Cancelleds", "appointment_StartTime");
            CreateIndex("dbo.Cancelleds", "appointment_Id");
            AddForeignKey("dbo.Cancelleds", "appointment_Id", "dbo.Appointments", "Id");
        }
    }
}
