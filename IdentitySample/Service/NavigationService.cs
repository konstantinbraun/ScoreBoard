using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using IdentitySample.Resx;
using System.Resources;  

namespace IdentitySample.Service
{
    public class NavigationService: INavigationService
    {
        List<NavigationItem> NavItems = new List<NavigationItem>()
        {
            new NavigationItem() { NavigationPath = "Tournaments/Index", ResourceName = "Tournaments",  Action = "GetTournaments", Name ="Tournaments", IsActive = true},
            new NavigationItem() { NavigationPath = "Tournaments/Details", ResourceName = "Teams",  Action = "GetTeams", Name ="Teams", IsActive = true},
            new NavigationItem() { NavigationPath = "Tournaments/Details", ResourceName = "Categories",  Action = "GetCategories", Name ="Categories", IsActive = false},
            new NavigationItem() { NavigationPath = "Tournaments/Details", ResourceName = "Referees",  Action = "GetReferees", Name ="Referees", IsActive = false},
            new NavigationItem() { NavigationPath = "Tournaments/Details", ResourceName = "Map",  Action = "GetMap", Name ="Map", IsActive = false},
            new NavigationItem() { NavigationPath = "Teams/Details", ResourceName = "Participants",  Action = "GetParticipants", Name ="Participants", IsActive = true},
            new NavigationItem() { NavigationPath = "Categories/Details", ResourceName = "Participants",  Action = "GetParticipantsForCategory", Name ="Participants", IsActive = true},
            new NavigationItem() { NavigationPath = "Categories/Details", ResourceName = "Fights",  Action = "GetFightsForCategory", Name ="Competitions", IsActive = false },
            new NavigationItem() { NavigationPath = "Referees/Details", ResourceName = "UpcomingFights",  Action = "GetUpcomingFights", Name ="UpcomingFights", IsActive = true },
            new NavigationItem() { NavigationPath = "Referees/Details", ResourceName = "FinishedFights",  Action = "GetFinishedFights", Name ="FinishedFights", IsActive = false },
            new NavigationItem() { NavigationPath = "Referees/Details", ResourceName = "Timeline",  Action = "GetTimeline", Name ="Timeline", IsActive = false },
            new NavigationItem() { NavigationPath = "Administration/Index", ResourceName = "Groups",  Action = "GetGroups", Name ="Groups", IsActive = true },
            new NavigationItem() { NavigationPath = "Administration/Index", ResourceName = "Users",  Action = "GetUsers", Name ="Users", IsActive = false },
            new NavigationItem() { NavigationPath = "Groups/Details", ResourceName = "Ranking",  Action = "GetSportUnits", Name ="Ranking", IsActive = true,  Group="Sport"},
            new NavigationItem() { NavigationPath = "Groups/Details", ResourceName = "Distinction",  Action = "GetSportUnits", Name ="Distinction", IsActive = false,  Group="Sport"},
            new NavigationItem() { NavigationPath = "Groups/Details", ResourceName = "Country",  Action = "GetGeneralUnits", Name ="Country", IsActive = true,  Group="General"},
            new NavigationItem() { NavigationPath = "Groups/Details", ResourceName = "Round",  Action = "GetGeneralUnits", Name ="Level", IsActive = false,  Group="General"},
            new NavigationItem() { NavigationPath = "Groups/Details", ResourceName = "Position",  Action = "GetSportUnits", Name ="Position", IsActive = false,  Group="Sport"},
            new NavigationItem() { NavigationPath = "Units/Details", ResourceName = "Translation",  Action = "GetTranslations", Name ="Translation", IsActive = true}        
        };

        public NavigationService()
        {

        }

        public IEnumerable<TabItem> GetTabs(RouteData routeData, string active)
        {
            string controller = routeData.Values["controller"].ToString();
            string action = routeData.Values["action"].ToString();
            List<TabItem> tabs = new List<TabItem>();
            IEnumerable<NavigationItem> navItemsRoute = NavItems.Where(x => x.NavigationPath == string.Format("{0}/{1}", controller, action));

            foreach (NavigationItem item in navItemsRoute)
            {
                bool IsActive = string.IsNullOrEmpty(active) ? item.IsActive : (item.Name == active);

                RouteValueDictionary routes = new RouteValueDictionary(routeData.Values);
                routes.Add("active", item.Name);

                string translation;
                ResourceManager rm = new ResourceManager(typeof(AppResource));
                try
                {
                    translation = rm.GetString(item.ResourceName);
                }
                catch (Exception)
                {
                    translation = item.ResourceName;
                }

                tabs.Add(new TabItem() { LinkText = translation, IsActive = IsActive, RouteValues = routes, ActionName = item.Action });
                item.IsActive = (item.Name == active);
            }
            return tabs;
        }

        public IEnumerable<TabItem> GetTabs(RouteData routeData, string active, string groupName)
        {
            string controller = routeData.Values["controller"].ToString();
            string action = routeData.Values["action"].ToString();
            List<TabItem> tabs = new List<TabItem>();
            IEnumerable<NavigationItem> navItemsRoute = NavItems.Where(x => x.NavigationPath == string.Format("{0}/{1}", controller, action) && x.Group == groupName);

            foreach (NavigationItem item in navItemsRoute)
            {
                bool IsActive = string.IsNullOrEmpty(active) ? item.IsActive : (item.Name == active);

                RouteValueDictionary routes = new RouteValueDictionary(routeData.Values);
                routes.Add("active", item.Name);

                string translation;
                ResourceManager rm = new ResourceManager(typeof(AppResource));
                try
                {
                    translation = rm.GetString(item.ResourceName);
                }
                catch (Exception)
                {
                    translation = item.ResourceName;
                }

                tabs.Add(new TabItem() { LinkText = translation, IsActive = IsActive, RouteValues = routes, ActionName = item.Action, GroupName = item.Name });
                item.IsActive = (item.Name == active);
            }
            return tabs;
        }
    }



}