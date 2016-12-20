using IdentitySample.Entities;

namespace IdentitySample.Repository
{
    public interface ITimelineRepository: IGenericRepository<Timeline>
    {
        Timeline GetById(int Id);
    }
}
