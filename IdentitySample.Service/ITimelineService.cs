using IdentitySample.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace IdentitySample.Service
{
    public interface ITimelineService : IEntityService<Timeline>
    {
        Timeline GetById(int Id);
        IEnumerable<Timeline> GetWithFilter(Expression<Func<Timeline, bool>> filter);     
    }
}
