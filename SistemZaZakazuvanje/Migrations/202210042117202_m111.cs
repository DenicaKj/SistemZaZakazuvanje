namespace SistemZaZakazuvanje.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m111 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Comments", "IdReview", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Comments", "IdReview", c => c.String());
        }
    }
}
