using IdentitySample.Entities;
using IdentitySample.Repository;

namespace IdentitySample.Service
{
    public class GeneralUnitService : EntityService<GeneralUnit>, IGeneralUnitService
    {
        IGeneralUnitRepository _generalUnitRepository;

        public GeneralUnitService(IUnitOfWork unitOfWork, IGeneralUnitRepository generalUnitRepository)
            : base(unitOfWork, generalUnitRepository)
        {
            _generalUnitRepository = generalUnitRepository;
        }

        public GeneralUnit GetById(int Id)
        {
            return _generalUnitRepository.GetById(Id);
        }


        public System.Collections.Generic.IEnumerable<GeneralUnit> GetByType(GeneralUnitType Type)
        {
            return _generalUnitRepository.FindBy(x => x.GeneralType == Type);
        }
    }
}
