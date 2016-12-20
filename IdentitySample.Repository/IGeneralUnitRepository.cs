using IdentitySample.Entities;

namespace IdentitySample.Repository
{
    public interface IGeneralUnitRepository : IGenericRepository<GeneralUnit>
    {
        GeneralUnit GetById(int Id);
    }
}
