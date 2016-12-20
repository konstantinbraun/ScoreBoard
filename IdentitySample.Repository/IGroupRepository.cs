using IdentitySample.Entities;

namespace IdentitySample.Repository
{
    public interface IGroupRepository : IGenericRepository<Group>
    {
        Group GetById(int Id);
    }
}
