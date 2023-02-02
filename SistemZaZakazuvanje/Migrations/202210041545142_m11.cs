namespace SistemZaZakazuvanje.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m11 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "IdReview", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Comments", "IdReview");
        }
    }
}
