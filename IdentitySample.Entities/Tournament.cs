using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using IdentitySample.Resx;
using DataAnnotationsExtensions;

namespace IdentitySample.Entities
{
    public enum TournamentStatus
    {
        OpenedForRegistration,
        CheckingData,
        IsRunning,
        IsClosed
    }

    public class Tournament: Entity<int>
    {
        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(AppResource))]
        [StringLength (255)]
        [Display(Name="Name", ResourceType=typeof(AppResource))]
        public string Name { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(AppResource))]
        [Display(Name = "StartDate", ResourceType = typeof(AppResource))]
        [DataType(DataType.Date)]
        public DateTime DateFrom { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(AppResource))]
        [Display(Name = "EndDate", ResourceType = typeof(AppResource))]
        [DataType(DataType.Date)]
        public DateTime DateTo { get; set; }

        [Required]
        [Display(Name = "Organizer", ResourceType = typeof(AppResource))]
        public string Organizer { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(AppResource))]
        [StringLength(50)]
        [Display(Name = "City", ResourceType = typeof(AppResource))]
        public string City { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(AppResource))]
        [Display(Name = "RegistrationEnd", ResourceType = typeof(AppResource))]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        public DateTime RegistrationEnd { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(AppResource))]
        [Display(Name = "Country", ResourceType = typeof(AppResource))]
        public int CountryId { get; set; }
        public virtual GeneralUnit  Country { get; set; }

        public virtual ICollection<Team> Teams { get; set; }

        public virtual ICollection<Category> Categories { get; set; }

        public virtual ICollection<Referee> Referees { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(AppResource))]
        [Display(Name = "Sport", ResourceType = typeof(AppResource))]
        public int GroupId { get; set; }
        public virtual Group Group { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(AppResource))] 
        [Display(Name = "Address", ResourceType = typeof(AppResource))]
        public string Address { get; set; }

        [Display(Name = "City", ResourceType = typeof(AppResource))]
        public string Location 
        {
            get 
            { 
                return string.Format("{0}, {1}", City, Country.Name); 
            }
        }

        public string FullAddress
        {
            get
            {
                return string.Format("{0} {1}, {2}", Address, City, Country.Name);
            }
        }

        [Display(Name = "Duration", ResourceType = typeof(AppResource))]
        public string Duration
        {
            get
            {
                return string.Format("{0:d} - {1:d}", DateFrom, DateTo);
            }
        }

        public TournamentStatus Status 
        {
            get 
            {
                if ( DateTime.Now<= this.RegistrationEnd )
                    return  TournamentStatus.OpenedForRegistration;
                else
                    return TournamentStatus.CheckingData;
            }
        }

    }
}
