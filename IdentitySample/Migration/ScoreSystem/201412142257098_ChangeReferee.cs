namespace IdentitySample.Migration.ScoreSystem
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeReferee : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Referees", "RefereeEmail", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Referees", "RefereeEmail", c => c.String());
        }
    }
}
