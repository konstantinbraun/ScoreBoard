using IdentitySample.Entities;

namespace IdentitySample.Repository
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Category GetById(int Id);
    }
}
