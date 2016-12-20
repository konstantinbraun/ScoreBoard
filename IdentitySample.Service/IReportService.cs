using IdentitySample.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentitySample.Service
{
    public interface IReportService : IEntityService<Report>
    {
        Report GetById(int Id);
    }
}
