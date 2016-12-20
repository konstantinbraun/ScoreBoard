using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using IdentitySample.Resx;

namespace IdentitySample.Models
{
    public class RoleViewModel
    {
        public string Id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(AppResource))]
        [Display(Name = "Name",ResourceType= typeof(AppResource) )]
        public string Name { get; set; }
    }

    public class EditUserViewModel
    {
        public string Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(AppResource))]
        [Display(Name = "Email", ResourceType=typeof(AppResource) )]
        [EmailAddress]
        public string Email { get; set; }


        public IEnumerable<SelectListItem> RolesList { get; set; }
    }
}