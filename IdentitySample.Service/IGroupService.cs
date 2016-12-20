using IdentitySample.Entities;
using System.Collections.Generic;

namespace IdentitySample.Service
{
    public interface IGroupService: IEntityService<Group>
    {
        Group GetById(int Id);
        IEnumerable<Group> GetSports();
    }
}
