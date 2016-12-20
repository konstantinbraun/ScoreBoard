using IdentitySample.Entities;
using IdentitySample.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentitySample.Service
{
    public class FightRelationService: EntityService<FightRelation>, IFightRelationService
    {
        IFightRelationRepository _fightRelationReopsitory;

        public FightRelationService(IUnitOfWork unitOfWork, IFightRelationRepository fightRelationRepository)
            : base(unitOfWork, fightRelationRepository)
        {
            _fightRelationReopsitory = fightRelationRepository;
        }

        public FightRelation GetById(int Id)
        {
            return _fightRelationReopsitory.GetById(Id);
        }


        public IEnumerable<FightRelation> GetWithFilter(System.Linq.Expressions.Expression<Func<FightRelation, bool>> filter)
        {
            return _fightRelationReopsitory.FindBy(filter);
        }
    }
}
