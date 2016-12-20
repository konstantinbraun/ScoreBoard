using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentitySample.ViewModels.TabItems
{
    public abstract class TabItemBase : ITabItem
    {
        protected ITabItem TabItems;

        public abstract string ActionName { get;}

        public abstract string GetCaption();

        public virtual int GetPageNumber()
        {
            throw new NotImplementedException();
        }

        public abstract bool IsActive();



    }
}
