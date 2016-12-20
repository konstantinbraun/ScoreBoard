using IdentitySample.Entities;
using IdentitySample.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IdentitySample.Service
{
    public class FightService: EntityService<Fight>, IFightService
    {
        IFightRepository _fightRepository;

        public FightService(IUnitOfWork unityOfWork, IFightRepository fightRepository)
            : base(unityOfWork, fightRepository)
        {
            _fightRepository = fightRepository;
        }

        public Fight GetById(int id)
        {
            return _fightRepository.GetById(id);
        }

        public IEnumerable<Fight> GetWithFilter(Expression<Func<Fight, bool>> filter)
        {
            return _fightRepository.FindBy(filter);
        }

        public IEnumerable<Fight> GetFightsForReferee(int refereeId, bool isComplete)
        {
            return GetWithFilter(x => x.Category.Timelines.Any(s => s.LevelId == x.LevelId && s.RefereeId == refereeId));
        }
    }
}
