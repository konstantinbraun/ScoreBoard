using IdentitySample.Entities;
using System.Data.Entity;
using System.Linq;

namespace IdentitySample.Repository
{
    public class RefereeRepository: GenericRepository<Referee>, IRefereeRepository
    {
        public RefereeRepository(DbContext context)
            : base(context)
        {

        }

        public Referee GetById(int Id)
        {
            return FindBy(x=>x.Id == Id).SingleOrDefault();
        }
    }
}
