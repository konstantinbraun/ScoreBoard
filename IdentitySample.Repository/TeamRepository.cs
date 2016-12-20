using IdentitySample.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace IdentitySample.Repository
{
    public class TeamRepository :  GenericRepository<Team>, ITeamRepository 
    {
        public TeamRepository(DbContext context)
            : base(context)
        {

        }

        public override IEnumerable<Team> GetAll()
        {
            return _entities.Set<Team>().Include(x => x.Country).AsEnumerable(); 
        }

        public Team GetById(int id)
        {
            return _dbset.Include(x=>x.Participants).Where(x=>x.Id == id).SingleOrDefault();
        }
    }
}
