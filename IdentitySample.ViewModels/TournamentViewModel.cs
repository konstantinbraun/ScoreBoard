using IdentitySample.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentitySample.Entities;
using IdentitySample.Service;
using IdentitySample.ViewModels.Helpers;
using IdentitySample.Resx;
using PagedList;
using IdentitySample.ViewModels.TabItems;

namespace IdentitySample.ViewModels
{
    public class TournamentViewModel1 : ViewModelBase,  ITournamentViewModel
    {
        ITournamentService tournamentService;
        Tournament tournament;

        public TournamentViewModel1(ITournamentService _tournamentService)
        {
            tournamentService = _tournamentService;
        }

        public string Title
        {
            get
            {
                if (tournament != null)
                    return tournament.Name;
                else
                    return Resx.AppResource.Tournament;
            }
        }

        public string Subtitle
        {
            get
            {
                return "Subtitle";
            }
        }


        public void Initialize(int? id)
        {
            if (id != null)
                tournament = tournamentService.GetById((int)id);
            else
                tournament = null;
        }

        public override  IList<BreadCrumbItem> GetBreadCrumbs()
        {
            IList<BreadCrumbItem> breadCrumbs=  base.GetBreadCrumbs();
            breadCrumbs.Add(new BreadCrumbItem() { Controller = "Tournaments", Name = AppResource.Tournaments, Action = "Index" });

            if (tournament != null)
                breadCrumbs.Add(new BreadCrumbItem() { Controller = "Tournaments", Id = tournament.Id, Name = tournament.Name, Action = "Details" });

            return breadCrumbs;
        }

        public IList<Tournament> GetData()
        {
            throw new NotImplementedException();
        }

        public IList<Button> GetButtons()
        {
            List<Button> buttons = new List<Button>();

            //      if (team.Tournament.Status == TournamentStatus.OpenedForRegistration && team.Coach == User.Identity.Name)
            //    {
            buttons.Add(new Button() { Target = "_self", Id = tournament.Id, Caption = AppResource.AddParticipant, Action = "Create", Controller = "Participants" });
            buttons.Add(new Button() { Target = "_self", Id = tournament.Id, Caption = AppResource.AddParticipant, Action = "Create", Controller = "Participants" });
            //}
            return buttons; throw new NotImplementedException();
        }

        public IList<ITabItem> GetTabs()
        {
            List<ITabItem> tabs = new List<ITabItem>();

            //tabs.Add(new TabItem() { LinkText = "Teams", IsActive = true, ActionName = "_Teams2", GroupName ="Test", delData = GetData1 });
            tabs.Add(new TeamTabItem());

            return tabs;
        }

        private IPagedList<BaseEntity> GetData1(int id)
        {
            List <BaseEntity> lst = new List<BaseEntity>();

            //var team = tournamentService.GetById((int)id);
            //HttpCookie pageSizeCookie =  Request.Cookies["_pageSize"];
            //int pageSize = 5;// Convert.ToInt16(pageSizeCookie.Value);

            //var tab = new TableViewModel<Participant>()
            //{
            //    TableData = team.Participants.OrderBy(x => x.FirstName) //.ToPagedList((int)page, pageSize),
            //    Active = active,
            //    Controller = "Teams",
            //    ChildController = "Participants",
            //    TournamentOrganizer = team.Tournament.Organizer,
            //    TournamentStatus = team.Tournament.Status
            //};

           // lst.AddRange(tournament.Teams.OrderBy(x => x.Name ));
//            tournament.Teams.OrderBy(x => x.Name).ToPagedList<Tournament>(1, 2);

            return tournament.Teams.OrderBy(x => x.Name).ToPagedList<BaseEntity>(1, 5);
        }

        public IList<string> GetDescription()
        {
            return new List<string>() { "First line", "Second line" };
        }
    }
}
