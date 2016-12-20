using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentitySample.Resx;

namespace IdentitySample.Entities
{
    public class Referee : Entity<int>
    {
        [Required]
        public int TournamentId { get; set; }
        public virtual Tournament Tournament { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(AppResource))]
        [Display(Name = "Referee", ResourceType = typeof(AppResource))]
        [DataType(DataType.EmailAddress)]
        public string RefereeEmail { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(AppResource))]
        [Display(Name="Position", ResourceType = typeof(AppResource))]
        public int PositionId { get; set; }
        public virtual SportUnit Position { get; set; }

        public virtual ICollection<Timeline> Timelines { get; set; }
    }
}
