using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentitySample.ViewModels.Helpers;
using IdentitySample.Resx;
using IdentitySample.Entities;

namespace IdentitySample.ViewModels.Common
{
    public abstract class ViewModelBase
    {
        protected int? id;

        public virtual IList<BreadCrumbItem> GetBreadCrumbs()
        {
            List<BreadCrumbItem> breadCrumbs = new List<BreadCrumbItem>();
            breadCrumbs.Add(new BreadCrumbItem() { Controller = "Home", Name = AppResource.StartPage, Action = "Index" });
            return breadCrumbs;
        }

    }
}
