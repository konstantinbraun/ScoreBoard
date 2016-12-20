using IdentitySample.Entities;
using IdentitySample.Repository;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace IdentitySample.Service
{
    class ParticipantService: EntityService<Participant>, IParticipantService
    {
        IParticipantRepository _participantRepository;

        public ParticipantService(IUnitOfWork unitOfWork, IParticipantRepository participantRepository)
            : base(unitOfWork, participantRepository)
        {
            _participantRepository = participantRepository;
        }

        public Participant GetById(int Id)
        {
            return _participantRepository.GetById(Id);
        }

        public IEnumerable<Participant> GetWithFilter(Expression<System.Func<Participant, bool>> filter)
        {
            return _participantRepository.FindBy(filter);

        }
    }
}
