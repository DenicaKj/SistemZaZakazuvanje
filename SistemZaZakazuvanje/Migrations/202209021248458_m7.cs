namespace SistemZaZakazuvanje.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m7 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Uslugas", "Price", c => c.Double(nullable: false));
            AddColumn("dbo.Uslugas", "Tip", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Uslugas", "Tip");
            DropColumn("dbo.Uslugas", "Price");
        }
    }
}
