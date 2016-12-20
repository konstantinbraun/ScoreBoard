namespace IdentitySample.Migration.ScoreSystem
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeWeightToDbl : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Categories", "WeightFrom", c => c.Double(nullable: false));
            AlterColumn("dbo.Categories", "WeightTo", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Categories", "WeightTo", c => c.Int(nullable: false));
            AlterColumn("dbo.Categories", "WeightFrom", c => c.Int(nullable: false));
        }
    }
}
