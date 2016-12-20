using IdentitySample.Entities;
using IdentitySample.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentitySample.Service
{
    public class ReportService: EntityService<Report>, IReportService 
    {
        IReportRepository _reportRepository;

        public ReportService(IUnitOfWork unitOfWork, IReportRepository reportRepository)
            : base(unitOfWork, reportRepository)
        {
            _reportRepository = reportRepository;
        }

        public Report GetById(int Id)
        {
            return _reportRepository.GetById(Id);
        }
    }
}
