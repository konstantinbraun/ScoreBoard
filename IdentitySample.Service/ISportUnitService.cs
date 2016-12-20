using IdentitySample.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentitySample.Service
{
    public interface ISportUnitService: IEntityService<SportUnit>
    {
        SportUnit GetById(int Id);
        IEnumerable<SportUnit> GetByTypeForGroup(int GroupId, SportUnitType type);
    }
}
