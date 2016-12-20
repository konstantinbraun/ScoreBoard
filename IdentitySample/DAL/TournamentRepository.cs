using IdentitySample.Entities;
using IdentitySample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IdentitySample.DAL
{
    public class TournamentRepository : GenericRepository<Tournament>
    {
        public TournamentRepository(ScoreSystemDbContext context) 
            : base(context)
        {

        }


    }
}