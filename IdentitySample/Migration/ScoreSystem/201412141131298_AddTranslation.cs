namespace IdentitySample.Migration.ScoreSystem
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTranslation : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Categories", "SportUnit_Id", "dbo.Units");
            DropForeignKey("dbo.Categories", "SportUnit_Id1", "dbo.Units");
            DropForeignKey("dbo.Categories", "PositionId", "dbo.Units");
            DropForeignKey("dbo.Participants", "TeamId", "dbo.Teams");
            DropIndex("dbo.Categories", new[] { "PositionId" });
            DropIndex("dbo.Categories", new[] { "SportUnit_Id" });
            DropIndex("dbo.Categories", new[] { "SportUnit_Id1" });
            DropIndex("dbo.Referees", new[] { "PositionId" });
            CreateTable(
                "dbo.Timelines",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RefereeId = c.Int(nullable: false),
                        CategoryId = c.Int(nullable: false),
                        LevelId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId)
                .ForeignKey("dbo.Units", t => t.LevelId)
                .ForeignKey("dbo.Referees", t => t.RefereeId)
                .Index(t => t.RefereeId)
                .Index(t => t.CategoryId)
                .Index(t => t.LevelId);
            
            CreateTable(
                "dbo.Translations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UnitId = c.Int(nullable: false),
                        Language = c.Int(nullable: false),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Units", t => t.UnitId)
                .Index(t => t.UnitId);
            
            AddColumn("dbo.Referees", "RefereeEmail", c => c.String());
            AlterColumn("dbo.Referees", "PositionId", c => c.Int(nullable: false));
            CreateIndex("dbo.Referees", "PositionId");
            AddForeignKey("dbo.Participants", "TeamId", "dbo.Teams", "Id", cascadeDelete: true);
            DropColumn("dbo.Categories", "PositionId");
            DropColumn("dbo.Categories", "SequenceNumber");
            DropColumn("dbo.Categories", "SportUnit_Id");
            DropColumn("dbo.Categories", "SportUnit_Id1");
            DropColumn("dbo.Referees", "UserName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Referees", "UserName", c => c.String());
            AddColumn("dbo.Categories", "SportUnit_Id1", c => c.Int());
            AddColumn("dbo.Categories", "SportUnit_Id", c => c.Int());
            AddColumn("dbo.Categories", "SequenceNumber", c => c.Int(nullable: false));
            AddColumn("dbo.Categories", "PositionId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Participants", "TeamId", "dbo.Teams");
            DropForeignKey("dbo.Translations", "UnitId", "dbo.Units");
            DropForeignKey("dbo.Timelines", "RefereeId", "dbo.Referees");
            DropForeignKey("dbo.Timelines", "LevelId", "dbo.Units");
            DropForeignKey("dbo.Timelines", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Translations", new[] { "UnitId" });
            DropIndex("dbo.Timelines", new[] { "LevelId" });
            DropIndex("dbo.Timelines", new[] { "CategoryId" });
            DropIndex("dbo.Timelines", new[] { "RefereeId" });
            DropIndex("dbo.Referees", new[] { "PositionId" });
            AlterColumn("dbo.Referees", "PositionId", c => c.Int());
            DropColumn("dbo.Referees", "RefereeEmail");
            DropTable("dbo.Translations");
            DropTable("dbo.Timelines");
            CreateIndex("dbo.Referees", "PositionId");
            CreateIndex("dbo.Categories", "SportUnit_Id1");
            CreateIndex("dbo.Categories", "SportUnit_Id");
            CreateIndex("dbo.Categories", "PositionId");
            AddForeignKey("dbo.Participants", "TeamId", "dbo.Teams", "Id");
            AddForeignKey("dbo.Categories", "PositionId", "dbo.Units", "Id");
            AddForeignKey("dbo.Categories", "SportUnit_Id1", "dbo.Units", "Id");
            AddForeignKey("dbo.Categories", "SportUnit_Id", "dbo.Units", "Id");
        }
    }
}
