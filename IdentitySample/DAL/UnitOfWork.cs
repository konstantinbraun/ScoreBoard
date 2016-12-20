using System;
using IdentitySample.Models;
using IdentitySample.Entities;

namespace IdentitySample.DAL
{
    public class UnitOfWork1 : IDisposable
    {
        private ScoreSystemDbContext context = new ScoreSystemDbContext();
        private TournamentRepository tournamentRepository;
        private GenericRepository<Team> teamRepository;
        private GenericRepository<Participant> participantRepository;
        private CategoryRepository categoryRepository;
        private GenericRepository<Group> groupRepository;
        private GenericRepository<SportUnit> sportUnitRepository;
        private GenericRepository<GeneralUnit> generalUnitRepository;
        private GenericRepository<Fight> fightRepository;
        private GenericRepository<FightRelation> fightRelationRepository;
        private GenericRepository<Fighter> fighterRepository;
        private GenericRepository<Referee> refereeRepository;
        private GenericRepository<Translation> translationRepository;
        private GenericRepository<Timeline> timelineRepository;
        private GenericRepository<Report> reportRepository;

        public GenericRepository<Group> GroupRepository
        {
            get
            {
                if (this.groupRepository == null)
                {
                    this.groupRepository = new GenericRepository<Group>(context);
                }
                return groupRepository;
            }
        }
        
        public GenericRepository<Timeline> TimelineRepository
        {
            get
            {
                if (this.timelineRepository == null)
                {
                    this.timelineRepository = new GenericRepository<Timeline>(context);
                }
                return timelineRepository;
            }
        }
        
        public GenericRepository<Translation> TranslationRepository
        {
            get
            {
                if (this.translationRepository == null)
                {
                    this.translationRepository = new GenericRepository<Translation>(context);
                }
                return translationRepository;
            }
        }
        
        public TournamentRepository TournamentRepository
        {
            get
            {
                if (this.tournamentRepository == null)
                {
                    this.tournamentRepository = new TournamentRepository(context);
                }
                return tournamentRepository;
            }
        }
        
        public GenericRepository<Team> TeamRepository
        {
            get
            {
                if (this.teamRepository == null)
                {
                    this.teamRepository = new GenericRepository<Team>(context);
                }
                return teamRepository;
            }
        }

        public GenericRepository<Participant> ParticipantRepository
        {
            get
            {

                if (this.participantRepository == null)
                {
                    this.participantRepository = new GenericRepository<Participant>(context);
                }
                return participantRepository;
            }
        }

        public CategoryRepository CategoryRepository
        {
            get
            {
                if (this.categoryRepository == null)
                {
                    this.categoryRepository = new CategoryRepository(context);
                }
                return categoryRepository;
            }
        }

        public GenericRepository<SportUnit> SportUnitRepository
        {
            get
            {

                if (this.sportUnitRepository == null)
                {
                    this.sportUnitRepository = new GenericRepository<SportUnit>(context);
                }
                return sportUnitRepository;
            }
        }

        public GenericRepository<GeneralUnit> GeneralUnitRepository
        {
            get
            {

                if (this.generalUnitRepository == null)
                {
                    this.generalUnitRepository = new GenericRepository<GeneralUnit>(context);
                }
                return generalUnitRepository;
            }
        }

        public GenericRepository<Fighter> FighterRepository
        {
            get
            {

                if (this.fighterRepository == null)
                {
                    this.fighterRepository = new GenericRepository<Fighter>(context);
                }
                return fighterRepository;
            }
        }

        public GenericRepository<Fight> FightRepository
        {
            get
            {

                if (this.fightRepository == null)
                {
                    this.fightRepository = new GenericRepository<Fight>(context);
                }
                return fightRepository;
            }
        }

        public GenericRepository<FightRelation> FightRelationRepository
        {
            get
            {

                if (this.fightRelationRepository == null)
                {
                    this.fightRelationRepository = new GenericRepository<FightRelation>(context);
                }
                return fightRelationRepository;
            }
        }

        public GenericRepository<Referee> RefereeRepository
        {
            get
            {

                if (this.refereeRepository == null)
                {
                    this.refereeRepository = new GenericRepository<Referee>(context);
                }
                return refereeRepository;
            }
        }

        public GenericRepository<Report> ReportRepository
        {
            get
            {

                if (this.reportRepository == null)
                {
                    this.reportRepository = new GenericRepository<Report>(context);
                }
                return reportRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }


}
