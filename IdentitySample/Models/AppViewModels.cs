using IdentitySample.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IdentitySample.Models
{
    public class TournamentViewModel
    {
        public Tournament Tournament { get; set; }
        public IEnumerable<SelectListItem> CountryList { get; set; }
        public IEnumerable<SelectListItem> SportGroupList { get; set; }
    }

    public class TeamViewModel
    {
        public Team Team { get; set; }
        public IEnumerable<SelectListItem> CountryList { get; set; }
        public IEnumerable<SelectListItem> RankingList { get; set; }
    }

    public class ParticipantViewModel
    {
        public Participant Participant { get; set; }
        public IEnumerable<SelectListItem> RankingList { get; set; }
    }

    public class CategoryEditViewModel
    {
        public Category Category { get; set; }
        public IEnumerable<SelectListItem> PositionList { get; set; }
    }

    public class TimelineViewModel
    {
        public Timeline TimelineItem { get; set; }
        public IEnumerable<SelectListItem> LevelList { get; set; }
        public IEnumerable<SelectListItem> CategoryList { get; set; }
    }

    public class RefereeViewModel
    {
        public IEnumerable<SelectListItem> PositionList { get; set; }
        public Referee Referee { get; set; }
    }

    public class DisplayViewModel
    {
        public Fight CurrentFight { get; set; }
        public Fight NextFight { get; set; }
    }

    public class FighterViewModel
    {
        public int RefereeId { get; set; }
        public Fighter Fighter { get; set; }
        public List<Fighter> FightersToReplace { get; set; }
        public bool ShowWinnerItem { get; set; }
    }


    public class ReportViewModel
    {
        public Report Report { get; set; }
        public IEnumerable<SelectListItem> FileList { get; set; }
    }

    public class UploadViewModel : Entity<int>
    {
        [Required, FileExtensions(Extensions = "jpg", ErrorMessage = "Specify a jpg file. (Comma-separated values)")]
        public HttpPostedFileBase File { get; set; }
        public string UnitName { get; set; }
        public bool ToInsert { get; set; }
    }

    public class SampleDataViewModel
    {
        public int TournamentId { get; set; }

        [Display(Name = "Teams in tournament")]
        [Required]
        [Range(2, 10)]
        public int TeamCount { get; set; }

        [Display(Name = "Max participants in team")]
        [Required]
        [Range(5, 20)]
        public int MaxParticipantsInTeam { get; set; }
    }

    public class TableViewModel<T>
    {
        public PagedList.IPagedList<T> TableData { get; set; }
        public TournamentStatus? TournamentStatus { get; set; }
        public string TournamentOrganizer { get; set; }
        public string TeamCoach { get; set; }
        public string Active { get; set; }
        public string Controller { get; set; }
        public string ChildController { get; set; }
    }
}