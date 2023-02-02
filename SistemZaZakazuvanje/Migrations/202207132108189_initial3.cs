namespace SistemZaZakazuvanje.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Uslugas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Duration = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Appointments", "Usluga_Id", c => c.Int());
            CreateIndex("dbo.Appointments", "Usluga_Id");
            AddForeignKey("dbo.Appointments", "Usluga_Id", "dbo.Uslugas", "Id");
            DropColumn("dbo.Appointments", "Usluga");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Appointments", "Usluga", c => c.String());
            DropForeignKey("dbo.Appointments", "Usluga_Id", "dbo.Uslugas");
            DropIndex("dbo.Appointments", new[] { "Usluga_Id" });
            DropColumn("dbo.Appointments", "Usluga_Id");
            DropTable("dbo.Uslugas");
        }
    }
}
