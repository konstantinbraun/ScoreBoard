namespace IdentitySample.Migration.ScoreSystem
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Gender = c.Int(nullable: false),
                        AgeFrom = c.Int(nullable: false),
                        AgeTo = c.Int(nullable: false),
                        WeightFrom = c.Int(nullable: false),
                        WeightTo = c.Int(nullable: false),
                        PositionId = c.Int(nullable: false),
                        SequenceNumber = c.Int(nullable: false),
                        TournamentId = c.Int(nullable: false),
                        SportUnit_Id = c.Int(),
                        SportUnit_Id1 = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Units", t => t.SportUnit_Id)
                .ForeignKey("dbo.Units", t => t.SportUnit_Id1)
                .ForeignKey("dbo.Tournaments", t => t.TournamentId)
                .ForeignKey("dbo.Units", t => t.PositionId)
                .Index(t => t.PositionId)
                .Index(t => t.TournamentId)
                .Index(t => t.SportUnit_Id)
                .Index(t => t.SportUnit_Id1);
            
            CreateTable(
                "dbo.Fights",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Level = c.Int(nullable: false),
                        FightInLevel = c.Int(nullable: false),
                        CategoryId = c.Int(nullable: false),
                        PositionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId)
                .ForeignKey("dbo.Units", t => t.PositionId)
                .Index(t => t.CategoryId)
                .Index(t => t.PositionId);
            
            CreateTable(
                "dbo.Fighters",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FightId = c.Int(nullable: false),
                        ParticipantId = c.Int(nullable: false),
                        DistinctionId = c.Int(nullable: false),
                        IsWinner = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Units", t => t.DistinctionId)
                .ForeignKey("dbo.Participants", t => t.ParticipantId)
                .ForeignKey("dbo.Fights", t => t.FightId)
                .Index(t => t.FightId)
                .Index(t => t.ParticipantId)
                .Index(t => t.DistinctionId);
            
            CreateTable(
                "dbo.Units",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        SportType = c.Int(),
                        SportGroup_Id = c.Int(),
                        GeneralType = c.Int(),
                        GeneralGroup_Id = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Groups", t => t.GeneralGroup_Id)
                .ForeignKey("dbo.Groups", t => t.SportGroup_Id)
                .Index(t => t.SportGroup_Id)
                .Index(t => t.GeneralGroup_Id);
            
            CreateTable(
                "dbo.Participants",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(),
                        BirthDate = c.DateTime(nullable: false),
                        Gender = c.Int(nullable: false),
                        Weight = c.Double(nullable: false),
                        TeamId = c.Int(nullable: false),
                        RankingId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Units", t => t.RankingId)
                .ForeignKey("dbo.Teams", t => t.TeamId)
                .Index(t => t.TeamId)
                .Index(t => t.RankingId);
            
            CreateTable(
                "dbo.Teams",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Coach = c.String(nullable: false),
                        City = c.String(nullable: false),
                        CountryId = c.Int(nullable: false),
                        CoachRankingId = c.Int(nullable: false),
                        TournamentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Units", t => t.CoachRankingId)
                .ForeignKey("dbo.Tournaments", t => t.TournamentId)
                .ForeignKey("dbo.Units", t => t.CountryId)
                .Index(t => t.CountryId)
                .Index(t => t.CoachRankingId)
                .Index(t => t.TournamentId);
            
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        IsPublicGroup = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Tournaments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        DateFrom = c.DateTime(nullable: false),
                        DateTo = c.DateTime(nullable: false),
                        Organizer = c.String(nullable: false),
                        City = c.String(nullable: false, maxLength: 50),
                        RegistrationEnd = c.DateTime(nullable: false),
                        CountryId = c.Int(nullable: false),
                        GroupId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Units", t => t.CountryId)
                .ForeignKey("dbo.Groups", t => t.GroupId)
                .Index(t => t.CountryId)
                .Index(t => t.GroupId);
            
            CreateTable(
                "dbo.Referees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TournamentId = c.Int(nullable: false),
                        UserName = c.String(),
                        PositionId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Units", t => t.PositionId)
                .ForeignKey("dbo.Tournaments", t => t.TournamentId)
                .Index(t => t.TournamentId)
                .Index(t => t.PositionId);
            
            CreateTable(
                "dbo.FightRelations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FightParentId = c.Int(nullable: false),
                        FightChildId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Fights", t => t.FightChildId)
                .ForeignKey("dbo.Fights", t => t.FightParentId)
                .Index(t => t.FightParentId)
                .Index(t => t.FightChildId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Categories", "PositionId", "dbo.Units");
            DropForeignKey("dbo.Fights", "PositionId", "dbo.Units");
            DropForeignKey("dbo.FightRelations", "FightParentId", "dbo.Fights");
            DropForeignKey("dbo.FightRelations", "FightChildId", "dbo.Fights");
            DropForeignKey("dbo.Fighters", "FightId", "dbo.Fights");
            DropForeignKey("dbo.Participants", "TeamId", "dbo.Teams");
            DropForeignKey("dbo.Teams", "CountryId", "dbo.Units");
            DropForeignKey("dbo.Teams", "TournamentId", "dbo.Tournaments");
            DropForeignKey("dbo.Referees", "TournamentId", "dbo.Tournaments");
            DropForeignKey("dbo.Referees", "PositionId", "dbo.Units");
            DropForeignKey("dbo.Tournaments", "GroupId", "dbo.Groups");
            DropForeignKey("dbo.Tournaments", "CountryId", "dbo.Units");
            DropForeignKey("dbo.Categories", "TournamentId", "dbo.Tournaments");
            DropForeignKey("dbo.Units", "SportGroup_Id", "dbo.Groups");
            DropForeignKey("dbo.Units", "GeneralGroup_Id", "dbo.Groups");
            DropForeignKey("dbo.Teams", "CoachRankingId", "dbo.Units");
            DropForeignKey("dbo.Participants", "RankingId", "dbo.Units");
            DropForeignKey("dbo.Fighters", "ParticipantId", "dbo.Participants");
            DropForeignKey("dbo.Categories", "SportUnit_Id1", "dbo.Units");
            DropForeignKey("dbo.Fighters", "DistinctionId", "dbo.Units");
            DropForeignKey("dbo.Categories", "SportUnit_Id", "dbo.Units");
            DropForeignKey("dbo.Fights", "CategoryId", "dbo.Categories");
            DropIndex("dbo.FightRelations", new[] { "FightChildId" });
            DropIndex("dbo.FightRelations", new[] { "FightParentId" });
            DropIndex("dbo.Referees", new[] { "PositionId" });
            DropIndex("dbo.Referees", new[] { "TournamentId" });
            DropIndex("dbo.Tournaments", new[] { "GroupId" });
            DropIndex("dbo.Tournaments", new[] { "CountryId" });
            DropIndex("dbo.Teams", new[] { "TournamentId" });
            DropIndex("dbo.Teams", new[] { "CoachRankingId" });
            DropIndex("dbo.Teams", new[] { "CountryId" });
            DropIndex("dbo.Participants", new[] { "RankingId" });
            DropIndex("dbo.Participants", new[] { "TeamId" });
            DropIndex("dbo.Units", new[] { "GeneralGroup_Id" });
            DropIndex("dbo.Units", new[] { "SportGroup_Id" });
            DropIndex("dbo.Fighters", new[] { "DistinctionId" });
            DropIndex("dbo.Fighters", new[] { "ParticipantId" });
            DropIndex("dbo.Fighters", new[] { "FightId" });
            DropIndex("dbo.Fights", new[] { "PositionId" });
            DropIndex("dbo.Fights", new[] { "CategoryId" });
            DropIndex("dbo.Categories", new[] { "SportUnit_Id1" });
            DropIndex("dbo.Categories", new[] { "SportUnit_Id" });
            DropIndex("dbo.Categories", new[] { "TournamentId" });
            DropIndex("dbo.Categories", new[] { "PositionId" });
            DropTable("dbo.FightRelations");
            DropTable("dbo.Referees");
            DropTable("dbo.Tournaments");
            DropTable("dbo.Groups");
            DropTable("dbo.Teams");
            DropTable("dbo.Participants");
            DropTable("dbo.Units");
            DropTable("dbo.Fighters");
            DropTable("dbo.Fights");
            DropTable("dbo.Categories");
        }
    }
}
