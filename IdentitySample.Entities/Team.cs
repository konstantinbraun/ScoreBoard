using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentitySample.Resx;
namespace IdentitySample.Entities
{
    //public class MaxWordsAttribute: ValidationAttribute
    //{
    //    private int _maxWords;

    //    public MaxWordsAttribute(int maxWords): 
    //        base("{0} has to many words")
    //    {
    //        _maxWords = maxWords;
    //    }
    //    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    //    {
    //        if (value != null)
    //        {
    //            var valueAsString = value.ToString();
    //            if (valueAsString.Split(' ').Length > _maxWords)
    //            {
    //                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
    //            }
    //        }
    //        return ValidationResult.Success;  
    //    }
    //}

    //public enum TeamStatus 
    //{
    //    RequestToParticipate,
    //    PaticipationConfirmed,
    //    ParticipationReject
    //}

    public class Team: Entity<int> 
    {
        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(AppResource))]
        [StringLength (50)]
        [Display(Name="Name", ResourceType=typeof(AppResource))]
        public string Name { get; set; }

        [Display(Name = "Coach", ResourceType = typeof(AppResource))]
        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(AppResource))]
        public string Coach { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(AppResource))]
        [Display(Name = "City", ResourceType = typeof(AppResource))]
        public string City { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(AppResource))]
        [Display(Name = "Country", ResourceType = typeof(AppResource))]
        public int CountryId { get; set; }
        public virtual GeneralUnit Country { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(AppResource))]
        [Display(Name = "Ranking", ResourceType = typeof(AppResource))]
        public int CoachRankingId { get; set; }
        public virtual SportUnit CoachRanking { get; set; }

        public bool ParticipationConfirmed { get; set; }

        [Required]
        public int TournamentId { get; set; }
        public virtual Tournament Tournament { get; set; }

        public virtual ICollection<Participant> Participants { get; set; }

        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{, IValidatableObject
        //    var prop = new[] { "City" };
        //    if (City.ToLower().StartsWith("xxx"))
        //    {
        //        yield return new ValidationResult("you cannot enter xxx as a city", prop);
        //    }
        //}
    }
}
