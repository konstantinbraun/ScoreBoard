using IdentitySample.Entities;
using System.Collections.Generic;

namespace IdentitySample.Service
{
    public interface IGeneralUnitService: IEntityService<GeneralUnit>
    {
        GeneralUnit GetById(int Id);
        IEnumerable<GeneralUnit> GetByType(GeneralUnitType Type);
    }
}
