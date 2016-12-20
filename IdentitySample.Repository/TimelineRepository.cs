using IdentitySample.Entities;
using System.Data.Entity;
using System.Linq;

namespace IdentitySample.Repository
{
    class TimelineRepository : GenericRepository<Timeline>, ITimelineRepository
    {
        public TimelineRepository(DbContext context)
            :base(context)
        {

        }

        public Timeline GetById(int Id)
        {
            return FindBy(x=>x.Id == Id).SingleOrDefault();
        }
    }
}
