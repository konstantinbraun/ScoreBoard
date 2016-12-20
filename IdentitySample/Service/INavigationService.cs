using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;

namespace IdentitySample.Service
{
    public interface INavigationService
    {
        IEnumerable<TabItem> GetTabs(RouteData routeData, string active);
        IEnumerable<TabItem> GetTabs(RouteData routeData, string active, string groupName);
    }
}
