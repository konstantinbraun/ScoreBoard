using IdentitySample.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentitySample.Repository
{
    public class FightRepository: GenericRepository<Fight>, IFightRepository
    {
        public FightRepository(DbContext context):
            base(context)
        {

        }

        public Fight GetById(int Id)
        {
            return FindBy(x=>x.Id==Id).SingleOrDefault();
        }
    }
}
