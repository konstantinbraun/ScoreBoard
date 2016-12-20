using IdentitySample.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentitySample.Repository
{
    class FighterRepository : GenericRepository<Fighter>, IFighterRepository
    {
        public FighterRepository(DbContext context) 
            : base(context)
        {

        }

        public Fighter GetById(int Id)
        {
            return FindBy(x=>x.Id == Id).SingleOrDefault();
        }
    }
}
