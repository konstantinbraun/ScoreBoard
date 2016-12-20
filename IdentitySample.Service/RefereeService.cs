using IdentitySample.Entities;
using IdentitySample.Repository;
using System.Collections.Generic;
using System.Linq;

namespace IdentitySample.Service
{
    public class RefereeService: EntityService<Referee>, IRefereeService
    {
        IRefereeRepository _refereeRepository;

        public RefereeService(IUnitOfWork unitOfWork, IRefereeRepository refereeRepository)
            : base(unitOfWork, refereeRepository)
        {
            _refereeRepository = refereeRepository;
        }

        public Referee GetById(int Id)
        {
            return _refereeRepository.GetById(Id);
        }
    }
}
