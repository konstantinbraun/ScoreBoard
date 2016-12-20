namespace IdentitySample.Migration.ScoreSystem
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeLevelInFight : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Fights", name: "PositionId", newName: "LevelId");
            RenameIndex(table: "dbo.Fights", name: "IX_PositionId", newName: "IX_LevelId");
            DropColumn("dbo.Fights", "Level");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Fights", "Level", c => c.Int(nullable: false));
            RenameIndex(table: "dbo.Fights", name: "IX_LevelId", newName: "IX_PositionId");
            RenameColumn(table: "dbo.Fights", name: "LevelId", newName: "PositionId");
        }
    }
}
