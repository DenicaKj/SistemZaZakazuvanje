namespace SistemZaZakazuvanje.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Appointments", "Usluga_Id", "dbo.Uslugas");
            DropIndex("dbo.Appointments", new[] { "Usluga_Id" });
            RenameColumn(table: "dbo.Appointments", name: "Usluga_Id", newName: "UslugaId");
            AlterColumn("dbo.Appointments", "UslugaId", c => c.Int(nullable: false));
            CreateIndex("dbo.Appointments", "UslugaId");
            AddForeignKey("dbo.Appointments", "UslugaId", "dbo.Uslugas", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Appointments", "UslugaId", "dbo.Uslugas");
            DropIndex("dbo.Appointments", new[] { "UslugaId" });
            AlterColumn("dbo.Appointments", "UslugaId", c => c.Int());
            RenameColumn(table: "dbo.Appointments", name: "UslugaId", newName: "Usluga_Id");
            CreateIndex("dbo.Appointments", "Usluga_Id");
            AddForeignKey("dbo.Appointments", "Usluga_Id", "dbo.Uslugas", "Id");
        }
    }
}
