namespace IdentitySample.Migration.ScoreSystem
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAddressField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tournaments", "Address", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tournaments", "Address");
        }
    }
}
