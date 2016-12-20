using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentitySample.Entities
{
    public enum SportUnitType
    {
        [Display(Name = "Ranking", ResourceType = typeof(IdentitySample.Resx.AppResource))]
        Ranking,
        [Display(Name = "Position", ResourceType = typeof(IdentitySample.Resx.AppResource))]
        Position,
        [Display(Name = "Distinction", ResourceType = typeof(IdentitySample.Resx.AppResource))]
        Distinction,
    }

    public class SportUnit :Unit
    {
        public SportUnitType SportType { get; set; }

        [ForeignKey("SportGroup")]
        public int SportGroup_Id { get; set; }

        [InverseProperty("SportUnits")]
        public virtual Group SportGroup { get; set; }

        public virtual ICollection<Team> Teams { get; set; }
        public virtual ICollection<Participant> Participants { get; set; }
        public virtual ICollection<Fighter> Fighters { get; set; }
        public virtual ICollection<Referee> Referees { get; set; } 
    }
}
