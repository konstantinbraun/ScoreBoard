namespace IdentitySample.Migration.ScoreSystem
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ParticipationConfirmedFlag : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Teams", "ParticipationConfirmed", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Teams", "ParticipationConfirmed");
        }
    }
}
