using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentitySample.Resx;

namespace IdentitySample.Entities
{
    public class Timeline : Entity<int>
    {
        [Required]
        public int RefereeId { get; set; }
        public virtual Referee Referee { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(AppResource))]
        [Display(Name = "Category", ResourceType = typeof(AppResource))]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(AppResource))]
        [Display(Name = "Round", ResourceType = typeof(AppResource))]
        public int LevelId { get; set; }
        public virtual GeneralUnit Level { get; set; } 
    }
}
