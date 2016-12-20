using IdentitySample.Entities;
using System.Data.Entity;
using System.Linq;

namespace IdentitySample.Repository
{
    public class TranslationRepository: GenericRepository<Translation>, ITranslationRepository
    {
        public TranslationRepository(DbContext context) 
            : base(context)
        {

        }

        public Translation GetById(int Id)
        {
            return FindBy(x => x.Id == Id).SingleOrDefault();
        }
    }
}
