using IdentitySample.Entities;
using IdentitySample.Repository;

namespace IdentitySample.Service
{
    public class TimelineService : EntityService<Timeline>, ITimelineService
    {
        ITimelineRepository _timelineRepository;

        public TimelineService(IUnitOfWork unitOfWork, ITimelineRepository timelineRepository)
            :base(unitOfWork, timelineRepository)
        {
            _timelineRepository = timelineRepository;
        }

        public Timeline GetById(int Id)
        {
            return _timelineRepository.GetById(Id);
        }


        public System.Collections.Generic.IEnumerable<Timeline> GetWithFilter(System.Linq.Expressions.Expression<System.Func<Timeline, bool>> filter)
        {
            return _timelineRepository.FindBy(filter);
        }
    }
}
