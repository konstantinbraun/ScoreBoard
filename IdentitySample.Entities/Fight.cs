using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentitySample.Resx;

namespace IdentitySample.Entities
{
    public class Fight : Entity<int>
    {
        [Required]
        [Display(Name="Round", ResourceType= typeof(AppResource))]
        public int LevelId { get; set; }
        public virtual GeneralUnit Level { get; set; }

        public int FightInLevel { get; set; }

        [Display(Name="Category", ResourceType=typeof(AppResource) )]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; } 

        public virtual ICollection<Fighter> Fighters { get; set; }

        //[InverseProperty("FightParent")]
        public virtual ICollection<FightRelation> FightRelationsParentFights { get; set; }

        //[InverseProperty("FightChild")]
        public virtual ICollection<FightRelation> FightRelationsChildFights { get; set; }

        [Display(Name = "Participant", ResourceType = typeof(AppResource))]
        public Fighter FirstFighter
        {
            get
            {
                if (Fighters.Count > 0)
                    return Fighters.ElementAt(0);
                else
                    return null;
            }
        }

        [Display(Name = "Participant", ResourceType = typeof(AppResource))]
        public Fighter SecondFighter
        {
            get
            {
                if (Fighters.Count > 1)
                    return Fighters.ElementAt(1);
                else
                    return null;
            }
        }
    }
}
