namespace IdentitySample.Migration.ScoreSystem
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ThirdPlaceFeature : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FightRelations", "IsWinner", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.FightRelations", "IsWinner");
        }
    }
}
