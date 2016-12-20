using IdentitySample.Entities;
using System.Data.Entity;
using System.Linq;

namespace IdentitySample.Repository
{
    public class TournamentRepository : GenericRepository<Tournament>, ITournamentRepository
    {
        public TournamentRepository(DbContext context) 
            : base(context)
        {

        }

        public Tournament GetById(int id)
        {
            return FindBy(x => x.Id == id).SingleOrDefault();
        }
    }
}
