using IdentitySample.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentitySample.Service
{
    public interface IFighterService : IEntityService<Fighter>
    {
        Fighter GetById(int Id);
        /// <summary>
        /// Replace 2 participants in the category
        /// </summary>
        /// <param name="firstId">First participant Id</param>
        /// <param name="secondId">Second participant Id</param>
        /// <returns>Category Id</returns>
        int Replace(int firstId, int secondId);
        
        IEnumerable<Fighter> GetFightersToReplace(Fighter fighter);

        void SetWinner(int Id);
    }
}
