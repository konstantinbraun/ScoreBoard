using IdentitySample.Entities;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace IdentitySample.Service
{
    public interface ICategoryService : IEntityService<Category>
    {
        Category GetById(int Id);
        
        /// <summary>
        /// Get participants list from category
        /// </summary>
        /// <param name="Id">Category Id</param>
        /// <param name="confirmedOnly">If yes, only confirmed participants will get</param>
        /// <returns></returns>
        IEnumerable<Participant> GetParticipants(int Id, bool confirmedOnly);
    }
}
