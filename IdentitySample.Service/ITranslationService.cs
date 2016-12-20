using IdentitySample.Entities;
using System.Collections.Generic;

namespace IdentitySample.Service
{
    public interface ITranslationService : IEntityService<Translation>
    {
        Translation GetById(int Id);
        IEnumerable<Translation> GetByUnit(int UnitId);
    }
}
