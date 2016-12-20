using IdentitySample.Entities;
using IdentitySample.ViewModels.Helpers;
using IdentitySample.ViewModels.TabItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentitySample.ViewModels.Common
{
    public interface IViewModel
    {
        void Initialize(int? id);

        string Title { get; }

        string Subtitle { get; }

        IList<string> GetDescription();

        IList<BreadCrumbItem> GetBreadCrumbs();

        IList<Button> GetButtons();

        IList<ITabItem> GetTabs();

        //ITabItem GetCurrentTab();
    }
}
