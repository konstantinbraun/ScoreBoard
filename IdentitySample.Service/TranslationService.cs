using IdentitySample.Entities;
using IdentitySample.Repository;
using System.Collections.Generic;

namespace IdentitySample.Service
{
    public class TranslationService : EntityService<Translation>, ITranslationService
    {
        ITranslationRepository _translationRepository;

        public TranslationService(IUnitOfWork unitOfWork, ITranslationRepository translationRepository)
            : base(unitOfWork, translationRepository)
        {
            _translationRepository = translationRepository;
        }

        public Translation GetById(int Id)
        {
            return _translationRepository.GetById(Id);
        }


        public IEnumerable<Translation> GetByUnit(int UnitId)
        {
            return _translationRepository.FindBy(x => x.UnitId == UnitId);
        }
    }
}
