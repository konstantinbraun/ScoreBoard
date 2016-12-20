using IdentitySample.Entities;

namespace IdentitySample.Repository
{
    public interface ITournamentRepository : IGenericRepository<Tournament>
    {
        Tournament GetById(int id);
    }
}
