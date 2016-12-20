using IdentitySample.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentitySample.Service
{
    public interface IUnitService: IEntityService<Unit>
    {
        Unit GetById(int id);
    }
}
