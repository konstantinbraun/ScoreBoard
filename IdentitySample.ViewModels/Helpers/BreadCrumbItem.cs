using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentitySample.ViewModels.Helpers
{
    public class BreadCrumbItem
    {
        public BreadCrumbItem()
        {
            Routes = new Dictionary<int, string>();
        }
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public Dictionary<int, string> Routes { get; set; }
    }
}
