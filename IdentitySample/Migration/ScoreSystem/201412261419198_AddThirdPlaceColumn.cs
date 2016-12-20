namespace IdentitySample.Migration.ScoreSystem
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddThirdPlaceColumn : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Participants", "TeamId", "dbo.Teams");
            AddColumn("dbo.Categories", "OneThirdPlace", c => c.Boolean(nullable: false));
            AddForeignKey("dbo.Participants", "TeamId", "dbo.Teams", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Participants", "TeamId", "dbo.Teams");
            DropColumn("dbo.Categories", "OneThirdPlace");
            AddForeignKey("dbo.Participants", "TeamId", "dbo.Teams", "Id", cascadeDelete: true);
        }
    }
}
