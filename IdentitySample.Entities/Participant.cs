using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;
using IdentitySample.Resx;
using DataAnnotationsExtensions;

namespace IdentitySample.Entities
{
    public class Participant:Entity<int>
    {
        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(AppResource))]
        [Display( Name ="FirstName", ResourceType = typeof(IdentitySample.Resx.AppResource))]
        public string FirstName { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(AppResource))]
        [Display(Name = "LastName", ResourceType = typeof(IdentitySample.Resx.AppResource))]
        public string LastName { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(AppResource))]
        [Display(Name = "BirthDate", ResourceType = typeof(AppResource))]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(AppResource))]
        [Display(Name = "Gender", ResourceType = typeof(AppResource))]
        public Gender Gender { get; set; }

        [Display(Name = "Weight", ResourceType = typeof(IdentitySample.Resx.AppResource))]
        [Min(15, ErrorMessageResourceName = "GreaterThen", ErrorMessageResourceType = typeof(AppResource))]
        [Max(150, ErrorMessageResourceName = "LessThen", ErrorMessageResourceType = typeof(AppResource))]
        [Numeric]
        public double Weight { get; set; }

        [Display(Name = "Team", ResourceType = typeof(IdentitySample.Resx.AppResource))]
        public int TeamId { get; set; }
        public virtual Team Team { get; set; }

        public ICollection<Fighter> Fighters { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(AppResource))]
        [Display(Name = "Ranking", ResourceType = typeof(IdentitySample.Resx.AppResource))]
        public int RankingId { get; set; }
        public virtual SportUnit Ranking { get; set; }

        [Display(Name="Participant", ResourceType=typeof(IdentitySample.Resx.AppResource))]
        public string FullName
        {
            get
            {
                return string.Format("{0} {1}", FirstName, LastName);
            }
        }

        [Display(Name = "Weight", ResourceType = typeof(IdentitySample.Resx.AppResource))]
        public string WeightMeasure
        {
            get 
            { 
                return String.Format("{0:n2} {1}", Weight, AppResource.kg);  
            }
        }

        [Display(Name = "Age", ResourceType = typeof(IdentitySample.Resx.AppResource))]
        public string Age
        {
            get
            {
                    return string.Format("{0} {1}", new DateTime(this.Team.Tournament.DateFrom.Subtract(BirthDate).Ticks).Year - 1, IdentitySample.Resx.AppResource.Years );
            }
        }
    }
}
