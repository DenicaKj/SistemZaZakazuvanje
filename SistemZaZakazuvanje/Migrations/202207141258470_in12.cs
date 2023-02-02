namespace SistemZaZakazuvanje.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class in12 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Appointments", "UslugaId", "dbo.Uslugas");
            DropIndex("dbo.Appointments", new[] { "UslugaId" });
            AddColumn("dbo.Appointments", "Usluga", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Appointments", "Usluga");
            CreateIndex("dbo.Appointments", "UslugaId");
            AddForeignKey("dbo.Appointments", "UslugaId", "dbo.Uslugas", "Id", cascadeDelete: true);
        }
    }
}
