using IdentitySample.Entities;

namespace IdentitySample.Repository
{
    public interface ITeamRepository : IGenericRepository<Team>
    {
        Team GetById(int id);
    }
}
