using IdentitySample.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentitySample.Repository
{
    public interface IReportRepository: IGenericRepository<Report>
    {
        Report GetById(int Id);
    }
}
