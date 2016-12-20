using IdentitySample.Entities;

namespace IdentitySample.Service
{
    public interface ITournamentService: IEntityService<Tournament>
    {
        Tournament GetById(int Id);
    }
}
