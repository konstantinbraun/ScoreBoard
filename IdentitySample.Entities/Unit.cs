using IdentitySample.Resx;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IdentitySample.Entities
{
    public abstract class Unit: Entity<int>
    {
        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(AppResource))]
        [Display(Name = "Name", ResourceType = typeof(AppResource))]
        public string Name { get; set; }

        [Display(Name = "Description", ResourceType = typeof(AppResource))]
        public string Description { get; set; }

        public byte[] Image { get; set; }

        public virtual ICollection<Translation> Translations { get; set; }
    }
}
