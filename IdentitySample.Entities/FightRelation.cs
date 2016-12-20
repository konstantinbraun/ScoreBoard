using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentitySample.Entities
{
    public class FightRelation: Entity<int>
    {
        [ForeignKey("FightParent")]
        public int FightParentId { get; set; }
        [ForeignKey("FightChild")]
        public int FightChildId { get; set; }


        [InverseProperty("FightRelationsParentFights")]
        public virtual Fight FightParent { get; set; }
        [InverseProperty("FightRelationsChildFights")]
        public virtual Fight FightChild { get; set; }

        
        public bool IsWinner { get; set; }
    }
}
