using IdentitySample.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IdentitySample.Service
{
    public interface IFightRelationService : IEntityService<FightRelation>
    {
        FightRelation GetById(int Id);
        IEnumerable<FightRelation> GetWithFilter(Expression<Func<FightRelation, bool>> filter);
    }
}
