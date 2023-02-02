namespace SistemZaZakazuvanje.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m6 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reviews", "UserName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Reviews", "UserName");
        }
    }
}
