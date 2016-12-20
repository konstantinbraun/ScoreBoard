using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentitySample.ViewModels.Helpers
{
    public class Button
    {
        public int Id { get; set; }
        public string Caption { get; set; }
        public string Action { get; set; }
        public string Controller { get; set; }
        public string Target { get; set; }
        public string Type { get; set; }
    }
}
