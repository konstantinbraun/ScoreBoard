using IdentitySample.Entities;
using IdentitySample.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentitySample.Service
{
    public class FighterService : EntityService<Fighter>, IFighterService
    {
        IFighterRepository _fighterRepository;
        ISportUnitService _sportUnitService;
        IUnitOfWork _unitOfWork;

        public FighterService(IUnitOfWork unitOfWork, IFighterRepository fighterRepository, ISportUnitService sportUnitService)
            : base(unitOfWork, fighterRepository)
        {
            _fighterRepository = fighterRepository;
            _sportUnitService = sportUnitService;
            _unitOfWork = unitOfWork;
        }

        public Fighter GetById(int Id)
        {
            return _fighterRepository.GetById(Id);
        }

        public int Replace(int firstId, int secondId)
        {
            Fighter firstFighter = _fighterRepository.GetById(firstId);
            Fighter secondFighter = _fighterRepository.GetById(secondId);

            int firstParticipantId = firstFighter.ParticipantId;
            int secondParticipantId = secondFighter.ParticipantId;
            
            firstFighter.ParticipantId = secondParticipantId;
            this.Update(firstFighter);

            secondFighter.ParticipantId = firstParticipantId;
            this.Update(secondFighter);

            return secondFighter.Fight.CategoryId;
        }

        public IEnumerable<Fighter> GetFightersToReplace(Fighter fighter)
        {
            return _fighterRepository.FindBy(x => x.Fight.CategoryId == fighter.Fight.CategoryId && x.Id != fighter.Id);
        }

        public void SetWinner(int Id)
        {
            Fighter winner = _fighterRepository.GetById(Id);

            winner.IsWinner = true;

            SportUnit[] distinctions = _sportUnitService.GetByTypeForGroup(winner.Fight.Category.Tournament.GroupId, SportUnitType.Distinction).ToArray();

            var nextWinnerFight = winner.Fight.FightRelationsChildFights.Where(x => x.IsWinner).SingleOrDefault();
            if (nextWinnerFight != null)
            {
                int fighterCount = nextWinnerFight.FightParent.Fighters.Count();
                Fighter fighter = new Fighter() { FightId = (int)nextWinnerFight.FightParentId, ParticipantId = winner.ParticipantId, DistinctionId = distinctions[fighterCount].Id };
                this.Create(fighter);
            }

            var nexLoserFight = winner.Fight.FightRelationsChildFights.Where(x => !x.IsWinner).SingleOrDefault();
            if (nexLoserFight != null)
            {
                var loser = winner.Fight.Fighters.Where(x => x.Id != Id).Single();
                int fighterCount = nexLoserFight.FightParent.Fighters.Count();
                Fighter fighter = new Fighter() { FightId = (int)nexLoserFight.FightParentId, ParticipantId = loser.ParticipantId, DistinctionId = distinctions[fighterCount].Id };
                this.Create(fighter);
            }
        }
    }
}
