namespace SistemZaZakazuvanje.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Appointments", "Usluga", c => c.String());
            AddColumn("dbo.Appointments", "Status", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Appointments", "Status");
            DropColumn("dbo.Appointments", "Usluga");
        }
    }
}
