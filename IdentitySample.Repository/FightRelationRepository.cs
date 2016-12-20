using IdentitySample.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentitySample.Repository
{
    public class FightRelationRepository: GenericRepository<FightRelation>, IFightRelationRepository
    {
        public FightRelationRepository(DbContext context)
            : base(context)
        {

        }

        public FightRelation GetById(int Id)
        {
            return FindBy(x => x.Id == Id).SingleOrDefault();
        }
    }
}
