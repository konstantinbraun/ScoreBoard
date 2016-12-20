using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;

namespace IdentitySample.Models
{
    public class ScoreSystemDbMigrationConfiguration
        : DbMigrationsConfiguration<ScoreSystemDbContext>
    {
        public ScoreSystemDbMigrationConfiguration()
        {
       //     this.AutomaticMigrationDataLossAllowed = true;
       //     this.AutomaticMigrationsEnabled = true;
        }
        protected override void Seed(ScoreSystemDbContext context)
        {
            base.Seed(context);

#if DEBUG
            int i = 1;
            //context.SaveChanges();
#endif
        }
    }
}
