using IdentitySample.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IdentitySample.Service
{
    public interface IFightService : IEntityService<Fight>
    {
        Fight GetById(int id);
        IEnumerable<Fight> GetWithFilter(Expression<Func<Fight, bool>> filter);     
    }
}
