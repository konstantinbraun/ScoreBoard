using IdentitySample.Entities;
using System.Data.Entity;
using System.Linq;

namespace IdentitySample.Repository
{
    public class GeneralUnitRepository : GenericRepository<GeneralUnit>, IGeneralUnitRepository
    {
        public GeneralUnitRepository(DbContext context)
            : base(context)
        {

        }

        public GeneralUnit GetById(int Id)
        {
            return FindBy(x=>x.Id == Id).SingleOrDefault();
        }
    }
}
