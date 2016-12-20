using IdentitySample.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentitySample.Repository
{
    public class ReportRepository : GenericRepository<Report>, IReportRepository 
    {
        public ReportRepository(DbContext context)
            : base(context)
        {

        }

        public Report GetById(int Id)
        {
            return FindBy(x=>x.Id == Id).SingleOrDefault();
        }
    }
}
