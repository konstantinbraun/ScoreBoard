using IdentitySample.Entities;

namespace IdentitySample.Repository
{
    public interface IRefereeRepository: IGenericRepository<Referee>
    {
        Referee GetById(int Id);
    }
}
