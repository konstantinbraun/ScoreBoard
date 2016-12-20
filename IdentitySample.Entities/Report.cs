using IdentitySample.Resx;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentitySample.Entities
{
    public class Report : Entity<int>
    {
        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(AppResource))]
        [StringLength(50)]
        [Display(Name = "Name", ResourceType = typeof(AppResource))]
        public string Name { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(AppResource))]
        [Display(Name = "Description", ResourceType = typeof(AppResource))]
        public string Description { get; set; }

        public string FileName { get; set; } 

        public bool IsAdminReport { get; set; }
    }
}
