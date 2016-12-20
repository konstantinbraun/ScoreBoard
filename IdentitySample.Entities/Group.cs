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
    public class Group : Entity<int>
    {
        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(AppResource))]
        [Display(Name = "Name", ResourceType = typeof(AppResource))]
        public string Name { get; set; }

        public bool IsPublicGroup { get; set; }

        public virtual ICollection<Tournament> Tournaments { get; set; }

        [InverseProperty("GeneralGroup")]
        public virtual ICollection<GeneralUnit> GeneralUnits { get; set; }

        [InverseProperty("SportGroup")]
        public virtual ICollection<SportUnit> SportUnits { get; set; }
    }
}
