using IdentitySample.Entities;
using IdentitySample.Repository;

namespace IdentitySample.Service
{
    public class TeamService : EntityService<Team>, ITeamService
    {
        IUnitOfWork _unitOfWork;
        ITeamRepository _teamRepository;

        public TeamService(IUnitOfWork unitOfWork, ITeamRepository teamRepository)
            : base(unitOfWork, teamRepository)
        {
            _unitOfWork = unitOfWork;
            _teamRepository = teamRepository;
        }

        public Team GetById(int Id)
        {
            return _teamRepository.GetById(Id);
        }
    }
}
