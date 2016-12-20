using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentitySample.Entities
{
    public class Fighter:Entity<int>
    {
        public int FightId { get; set; }
        public virtual Fight Fight {get; set;}

        public int ParticipantId { get; set; }
        public virtual Participant Participant { get; set; }

        public int DistinctionId { get; set; }
        public virtual SportUnit Distinction { get; set; }

        public bool IsWinner { get; set; }
    }
}
