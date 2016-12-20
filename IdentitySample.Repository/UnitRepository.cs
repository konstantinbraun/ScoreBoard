using IdentitySample.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentitySample.Repository
{
    public class UnitRepository:  GenericRepository<Unit>, IUnitRepository
    {
        public UnitRepository(DbContext context)
            : base(context)
        {

        }

        public Unit GetById(int Id)
        {
            return FindBy(x=>x.Id == Id).SingleOrDefault();
        }
    }
}
