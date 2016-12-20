using IdentitySample.Entities;
using IdentitySample.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentitySample.Service
{
    public class SportUnitService: EntityService<SportUnit>, ISportUnitService
    {
        ISportUnitRepository _sportUnitRepository;

        public SportUnitService(IUnitOfWork unitOfWork, ISportUnitRepository sportUnitRepository)
            :base(unitOfWork, sportUnitRepository)
        {
            _sportUnitRepository = sportUnitRepository;
        }

        public SportUnit GetById(int Id)
        {
            return _sportUnitRepository.GetById(Id);
        }

        public IEnumerable<SportUnit> GetByTypeForGroup(int GroupId, SportUnitType type)
        {
            return _sportUnitRepository.FindBy(x => x.SportGroup_Id == GroupId && x.SportType == type);
        }
    }
}
