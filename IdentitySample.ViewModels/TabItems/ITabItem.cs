using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentitySample.ViewModels.TabItems
{
    public interface ITabItem
    {
        string GetCaption();
        bool IsActive();
        int GetPageNumber();
        string ActionName { get; }

    }
}
