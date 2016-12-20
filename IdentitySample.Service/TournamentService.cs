using IdentitySample.Entities;
using IdentitySample.Repository;

namespace IdentitySample.Service
{
    public class TournamentService: EntityService<Tournament>, ITournamentService
    {
        ITournamentRepository _tournamentRepository;

        public TournamentService(IUnitOfWork unitOfWork, ITournamentRepository tournamentRepository)
            : base(unitOfWork, tournamentRepository)
        {
            _tournamentRepository = tournamentRepository;
        }

        public Tournament GetById(int Id)
        {
            return _tournamentRepository.GetById(Id);
        }
    }
}
