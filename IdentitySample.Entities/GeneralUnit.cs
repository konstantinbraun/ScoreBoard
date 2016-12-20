using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentitySample.Entities
{
    public enum GeneralUnitType
    {
        [Display(Name = "Country", ResourceType = typeof(IdentitySample.Resx.AppResource))]
        Country,
        [Display(Name = "Round", ResourceType = typeof(IdentitySample.Resx.AppResource))]
        Level,
        [Display(Name = "Report", ResourceType = typeof(IdentitySample.Resx.AppResource))]
        Report,
    }

    public class GeneralUnit : Unit 
    {
        public GeneralUnitType GeneralType { get; set; }

        [ForeignKey("GeneralGroup")]
        public int GeneralGroup_Id { get; set; }

        [InverseProperty("GeneralUnits")]
        public virtual Group GeneralGroup { get; set; }

        public virtual ICollection<Tournament> Tournaments { get; set; }
        public virtual ICollection<Team> Teams { get; set; }
        public virtual ICollection<Fight> Fights { get; set; }
        public virtual ICollection<Timeline> Timelines { get; set; }
    }
}
