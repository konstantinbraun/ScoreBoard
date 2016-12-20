using IdentitySample.Entities;
using IdentitySample.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentitySample.Service
{
    public class UnitService : EntityService<Unit>, IUnitService 
    {
        IUnitRepository _unitRepository;
        
        public UnitService(IUnitOfWork unitOfWork, IUnitRepository unitRepository)
            : base(unitOfWork, unitRepository)
        {
            _unitRepository = unitRepository;
        }
        public Unit GetById(int Id)
        {
           return _unitRepository.GetById(Id);
        }
    }
}
