using IdentitySample.Entities;
using System.Data.Entity;
using System.Linq;

namespace IdentitySample.Repository
{
    public class CategoryRepository: GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(DbContext context)
            : base(context)
        {

        }

        public Category GetById(int Id)
        {
            return FindBy(x=>x.Id == Id).SingleOrDefault();
        }
    }
}
