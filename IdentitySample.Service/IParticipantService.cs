using IdentitySample.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace IdentitySample.Service
{
    public interface IParticipantService: IEntityService<Participant>
    {
        Participant GetById(int Id);
        IEnumerable<Participant> GetWithFilter(Expression<Func<Participant, bool>> filter);     

    }
}
