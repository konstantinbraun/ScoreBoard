using IdentitySample.Entities;

namespace IdentitySample.Repository
{
    public interface IParticipantRepository: IGenericRepository<Participant>
    {
        Participant GetById(int Id);
    }
}
