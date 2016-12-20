namespace IdentitySample.Migration.ScoreSystem
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddImageField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Units", "Image", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Units", "Image");
        }
    }
}
