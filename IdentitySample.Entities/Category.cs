using IdentitySample.Resx;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DataAnnotationsExtensions;

namespace IdentitySample.Entities
{
    public enum Gender
    {
        [Display(Name="Male", ResourceType=typeof(AppResource))]
        Male,
        [Display(Name = "Female", ResourceType = typeof(AppResource))]
        Female
    }

    public class Category : Entity<int>
    {
        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(AppResource))]
        [Display(Name="Gender",ResourceType=typeof(Resx.AppResource))]
        public Gender Gender { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(AppResource))]
        [Integer]
        [Min(3, ErrorMessageResourceName = "GreaterThen", ErrorMessageResourceType = typeof(AppResource))]
        [Max(100, ErrorMessageResourceName = "LessThen", ErrorMessageResourceType = typeof(AppResource))]
        [Display(Name = "AgeFrom", ResourceType = typeof(AppResource))]
        public int AgeFrom { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(AppResource))]
        [Integer]
        [Min(3, ErrorMessageResourceName = "GreaterThen", ErrorMessageResourceType = typeof(AppResource))]
        [Max(100, ErrorMessageResourceName = "LessThen", ErrorMessageResourceType = typeof(AppResource))]
        [Display(Name = "AgeTo", ResourceType = typeof(AppResource))]
        public int AgeTo { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(AppResource))]
        [Numeric]
        [Min(15, ErrorMessageResourceName = "GreaterThen", ErrorMessageResourceType = typeof(AppResource))]
        [Max(150, ErrorMessageResourceName = "LessThen", ErrorMessageResourceType = typeof(AppResource))]
        [Display(Name = "WeightFrom", ResourceType = typeof(AppResource))]
        public double WeightFrom { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(AppResource))]
        [Numeric]
        [Min(15, ErrorMessageResourceName = "GreaterThen", ErrorMessageResourceType = typeof(AppResource))]
        [Max(150, ErrorMessageResourceName = "LessThen", ErrorMessageResourceType = typeof(AppResource))]
        [Display(Name = "WeightTo", ResourceType = typeof(AppResource))]
        public double WeightTo { get; set; }

        [Display(Name = "OneThirdPlace", ResourceType = typeof(AppResource))]
        public bool OneThirdPlace { get; set; }

        public int TournamentId { get; set; }
        public virtual Tournament Tournament { get; set; }

        public virtual ICollection<Fight> Fights { get; set; }

        public virtual ICollection<Timeline> Timelines { get; set; }

        [Display(Name = "Age", ResourceType = typeof(AppResource))]
        public string Age
        {
            get
            {
                return string.Format("{0}-{1} {2}", AgeFrom, AgeTo, AppResource.Years);
            }
        }

        [Display(Name = "Weight", ResourceType = typeof(AppResource))]
        public string Weight
        {
            get
            {
                return string.Format("{0:n}-{1:n} {2}", WeightFrom, WeightTo, AppResource.kg);
            }
        }

    }
}
