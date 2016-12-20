using IdentitySample.Entities;
using System.Data.Entity;
using System.Linq;

namespace IdentitySample.Repository
{
    public class SportUnitRepository : GenericRepository<SportUnit>, ISportUnitRepository
    {
        public SportUnitRepository(DbContext context) 
            : base(context)
        {

        }

        public SportUnit GetById(int Id)
        {
            return FindBy(x => x.Id == Id).SingleOrDefault();
        }
    }
}
