namespace IdentitySample.Migration.ScoreSystem
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Change_ParentChildFightRelation : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Fights", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Timelines", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Categories", "TournamentId", "dbo.Tournaments");
            DropForeignKey("dbo.Fighters", "FightId", "dbo.Fights");
            DropForeignKey("dbo.Fights", "LevelId", "dbo.Units");
            DropForeignKey("dbo.Fighters", "ParticipantId", "dbo.Participants");
            DropForeignKey("dbo.Translations", "UnitId", "dbo.Units");
            DropForeignKey("dbo.Participants", "TeamId", "dbo.Teams");
            DropForeignKey("dbo.Referees", "TournamentId", "dbo.Tournaments");
            AddForeignKey("dbo.Fights", "CategoryId", "dbo.Categories", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Timelines", "CategoryId", "dbo.Categories", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Categories", "TournamentId", "dbo.Tournaments", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Fighters", "FightId", "dbo.Fights", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Fights", "LevelId", "dbo.Units", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Fighters", "ParticipantId", "dbo.Participants", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Translations", "UnitId", "dbo.Units", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Participants", "TeamId", "dbo.Teams", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Referees", "TournamentId", "dbo.Tournaments", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Referees", "TournamentId", "dbo.Tournaments");
            DropForeignKey("dbo.Participants", "TeamId", "dbo.Teams");
            DropForeignKey("dbo.Translations", "UnitId", "dbo.Units");
            DropForeignKey("dbo.Fighters", "ParticipantId", "dbo.Participants");
            DropForeignKey("dbo.Fights", "LevelId", "dbo.Units");
            DropForeignKey("dbo.Fighters", "FightId", "dbo.Fights");
            DropForeignKey("dbo.Categories", "TournamentId", "dbo.Tournaments");
            DropForeignKey("dbo.Timelines", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Fights", "CategoryId", "dbo.Categories");
            AddForeignKey("dbo.Referees", "TournamentId", "dbo.Tournaments", "Id");
            AddForeignKey("dbo.Participants", "TeamId", "dbo.Teams", "Id");
            AddForeignKey("dbo.Translations", "UnitId", "dbo.Units", "Id");
            AddForeignKey("dbo.Fighters", "ParticipantId", "dbo.Participants", "Id");
            AddForeignKey("dbo.Fights", "LevelId", "dbo.Units", "Id");
            AddForeignKey("dbo.Fighters", "FightId", "dbo.Fights", "Id");
            AddForeignKey("dbo.Categories", "TournamentId", "dbo.Tournaments", "Id");
            AddForeignKey("dbo.Timelines", "CategoryId", "dbo.Categories", "Id");
            AddForeignKey("dbo.Fights", "CategoryId", "dbo.Categories", "Id");
        }
    }
}
