using IdentitySample.Entities;
using System.Data.Entity;
using System.Linq;

namespace IdentitySample.Repository
{
    public class ParticipantRepository: GenericRepository<Participant>, IParticipantRepository
    {
        public ParticipantRepository(DbContext context)
            : base(context)
        {

        }

        public Participant GetById(int Id)
        {
            return FindBy(x => x.Id == Id).SingleOrDefault();
        }
    }
}
