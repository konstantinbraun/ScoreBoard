using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IdentitySample.Service
{
    public class NavigationItem
    {
        public string Name { get; set; }
        public string NavigationPath { get; set; }
        public string ResourceName { get; set; }
        public string Action { get; set; }
        public bool IsActive { get; set; }
        public string Group { get; set; }
    }
}