using IdentitySample.Controllers;
using IdentitySample.Entities;
using PagedList;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IdentitySample.Models
{
    public class ScoreSystemDbContext : DbContext
    {
#if DEBUG
        public ScoreSystemDbContext()
            : base("ScoreSystemConnection")
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<ScoreSystemDbContext>());
        }
#else
        public ScoreSystemDbContext()
            : base("YuliConnection")
        {
            // this.Configuration.LazyLoadingEnabled = false;
            // this.Configuration.ProxyCreationEnabled = false;
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<ScoreSystemDbContext>());
            // Database.SetInitializer(new MigrateDatabaseToLatestVersion<ScoreSystemDbContext, ScoreSystemDbMigrationConfiguration>());
        }
#endif
        public DbSet<Tournament> Tournaments { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Fight> Fights { get; set; }
        public DbSet<Fighter> Fighters { get; set; }
        public DbSet<FightRelation> FightRelations { get; set; }
        public DbSet<Participant> Participants { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<Referee> Referees { get; set; }
        public DbSet<Timeline> Timelines { get; set; }
        public DbSet<Translation> Translations { get; set; }
        public DbSet<Report> Reports { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Fighter>().HasRequired(f => f.Distinction).WithMany(s => s.Fighters).WillCascadeOnDelete(false);
         
            modelBuilder.Entity<Participant>().HasRequired(f => f.Ranking).WithMany(s => s.Participants).WillCascadeOnDelete(false);
            
            modelBuilder.Entity<Team>().HasRequired(f => f.CoachRanking).WithMany(s => s.Teams).WillCascadeOnDelete(false);
            modelBuilder.Entity<Team>().HasRequired(f => f.Country).WithMany(s => s.Teams).WillCascadeOnDelete(false);

            modelBuilder.Entity<Tournament>().HasRequired(f => f.Country).WithMany(s => s.Tournaments).WillCascadeOnDelete(false);
            modelBuilder.Entity<Tournament>().HasRequired(f => f.Group).WithMany(s => s.Tournaments).WillCascadeOnDelete(false);

            modelBuilder.Entity<Team>().HasRequired(f => f.Tournament).WithMany(s => s.Teams).WillCascadeOnDelete(false);

            modelBuilder.Entity<GeneralUnit>().HasRequired(f => f.GeneralGroup).WithMany(s => s.GeneralUnits).WillCascadeOnDelete(false);
            modelBuilder.Entity<SportUnit>().HasRequired(f => f.SportGroup).WithMany(s => s.SportUnits).WillCascadeOnDelete(false);

            modelBuilder.Entity<Timeline>().HasRequired(f => f.Level).WithMany(s => s.Timelines).WillCascadeOnDelete(false);

            modelBuilder.Entity<Referee>().HasRequired(f => f.Position).WithMany(s => s.Referees).WillCascadeOnDelete(false);
            modelBuilder.Entity<Timeline>().HasRequired(f => f.Referee).WithMany(s => s.Timelines).WillCascadeOnDelete(false);

            modelBuilder.Entity<FightRelation>().HasRequired(f => f.FightParent).WithMany(s => s.FightRelationsParentFights).WillCascadeOnDelete(false);
            modelBuilder.Entity<FightRelation>().HasRequired(f => f.FightChild).WithMany(s => s.FightRelationsChildFights).WillCascadeOnDelete(false);
        }
    }
}