namespace SistemZaZakazuvanje.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cancelleds",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        appointment_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Appointments", t => t.appointment_Id)
                .Index(t => t.appointment_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cancelleds", "appointment_Id", "dbo.Appointments");
            DropIndex("dbo.Cancelleds", new[] { "appointment_Id" });
            DropTable("dbo.Cancelleds");
        }
    }
}
