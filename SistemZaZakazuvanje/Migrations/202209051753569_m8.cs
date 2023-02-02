namespace SistemZaZakazuvanje.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m8 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Uslugas", "imageUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Uslugas", "imageUrl");
        }
    }
}
