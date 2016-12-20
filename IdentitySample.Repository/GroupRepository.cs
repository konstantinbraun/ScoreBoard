using IdentitySample.Entities;
using System.Data.Entity;
using System.Linq;

namespace IdentitySample.Repository
{
    public class GroupRepository: GenericRepository<Group>, IGroupRepository
    {
        public GroupRepository(DbContext context)
            : base(context)
        {

        }
        public Group GetById(int Id)
        {
            return FindBy(x=>x.Id==Id).SingleOrDefault() ;
        }
    }
}
