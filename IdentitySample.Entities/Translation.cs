using IdentitySample.Resx;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentitySample.Entities
{
    public enum Language
    {
        [Display(Name = "German", ResourceType = typeof(IdentitySample.Resx.AppResource))]
        de,
        [Display(Name = "Russian", ResourceType = typeof(IdentitySample.Resx.AppResource))]
        ru,
        [Display(Name = "Japanese", ResourceType = typeof(IdentitySample.Resx.AppResource))]
        ja,
        [Display(Name = "English", ResourceType = typeof(IdentitySample.Resx.AppResource))]
        en
    }

    public class Translation : Entity<int>
    {
        public int UnitId { get; set; }
        public virtual Unit Unit { get; set; }

        [Display(Name = "Language", ResourceType = typeof(AppResource))]
        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(AppResource))]
        public Language Language { get; set; }

        [Display(Name = "Name", ResourceType = typeof(AppResource))]
        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(AppResource))]
        public string Name { get; set; }
    }
}
