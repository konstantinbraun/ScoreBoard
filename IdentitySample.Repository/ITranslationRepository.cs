using IdentitySample.Entities;

namespace IdentitySample.Repository
{
    public interface ITranslationRepository: IGenericRepository<Translation>
    {
        Translation GetById(int Id);
    }
}
