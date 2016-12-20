using IdentitySample.Entities;
using IdentitySample.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace IdentitySample.Service
{
    public class CategoryService: EntityService<Category>, ICategoryService
    {
        ICategoryRepository _categoryRepository;
        IParticipantRepository  _participantRepository;

        public CategoryService(IUnitOfWork unitOfwork, ICategoryRepository categoryRepository, IParticipantRepository participantRepository)
            : base(unitOfwork, categoryRepository)
        {
            _categoryRepository = categoryRepository;
            _participantRepository = participantRepository;
        }

        public Category GetById(int Id)
        {
            return _categoryRepository.GetById(Id);
        }

        public IEnumerable<Participant> GetParticipants(int Id, bool confirmedOnly)
        {
            Expression<Func<Participant, bool>> filter;
            Category cat = _categoryRepository.GetById(Id);
            DateTime minDate = cat.Tournament.DateFrom.AddYears(-1 * cat.AgeTo);
            DateTime maxDate = cat.Tournament.DateFrom.AddYears(-1 * cat.AgeFrom);

            if (confirmedOnly)
            {
                filter = x => x.Team.TournamentId == cat.TournamentId &&
                               x.Gender == cat.Gender &&
                               x.BirthDate >= minDate &&
                               x.BirthDate < maxDate &&
                               x.Team.ParticipationConfirmed &&
                               x.Weight >= cat.WeightFrom &&
                               x.Weight < cat.WeightTo;
            }
            else
            {
                filter = x => x.Team.TournamentId == cat.TournamentId &&
                               x.Gender == cat.Gender &&
                               x.BirthDate >= minDate &&
                               x.BirthDate < maxDate &&
                               x.Weight >= cat.WeightFrom &&
                               x.Weight < cat.WeightTo;
            }
            return _participantRepository.FindBy(filter);
        }
    }
}
