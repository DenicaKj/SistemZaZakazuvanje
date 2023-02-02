namespace SistemZaZakazuvanje.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class in1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Appointments", "UserId", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Appointments", "UserId", c => c.Int(nullable: false));
        }
    }
}
