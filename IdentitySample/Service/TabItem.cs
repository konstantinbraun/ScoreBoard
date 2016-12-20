using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace IdentitySample.Service
{
    public class TabItem
    {
        /// <summary>
        /// The link's display name 
        /// </summary>
        public string LinkText { get; set; }
        public RouteValueDictionary RouteValues { get; set; }
        
        /// <summary>
        /// Action method in the controller to get child table data for the object
        /// </summary>
        public string ActionName { get; set; }

        /// <summary>
        /// The current tab is active (yes,no)
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Additional routing property
        /// </summary>
        public string GroupName { get; set; }
    }
}