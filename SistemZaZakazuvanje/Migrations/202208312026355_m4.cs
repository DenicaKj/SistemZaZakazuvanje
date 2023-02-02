namespace SistemZaZakazuvanje.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Appointments", "color", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Appointments", "color");
        }
    }
}
