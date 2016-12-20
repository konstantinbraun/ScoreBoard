using IdentitySample.Entities;

namespace IdentitySample.Service
{
    public interface ITeamService: IEntityService<Team>
    {
        Team GetById(int Id);
    }
}
