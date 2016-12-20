using IdentitySample.Entities;
using IdentitySample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace IdentitySample.DAL
{
    public class CategoryRepository : GenericRepository<Category>
    {
        public CategoryRepository(ScoreSystemDbContext context) 
            : base(context)
        {

        }

        public Expression<Func<Participant, bool>> GetParticipantsInCategoryFilter(int id, bool getConfirmed = false)
        {
            Expression<Func<Participant, bool>> filter;

            Category cat = base.GetByID(id);
            DateTime minDate = cat.Tournament.DateFrom.AddYears(-1 * cat.AgeTo);
            DateTime maxDate = cat.Tournament.DateFrom.AddYears(-1 * cat.AgeFrom);

            if (getConfirmed)
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
            return filter;
        }
    }
} 