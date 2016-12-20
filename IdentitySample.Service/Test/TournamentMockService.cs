using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentitySample.Service.Test
{
    class TournamentMockService1: ITournamentService
    {
        public Entities.Tournament GetById(int Id)
        {
            throw new NotImplementedException();
        }

        public Entities.Tournament Create(Entities.Tournament entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Entities.Tournament entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Entities.Tournament> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(Entities.Tournament entity)
        {
            throw new NotImplementedException();
        }
    }
}
