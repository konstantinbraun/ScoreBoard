using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentitySample.ViewModels.TabItems
{
    public class CategoryTabItem : TabItemBase
    {
        public override string ActionName
        {
            get
            {
                return "_Categories";
            }
        }

        public override string GetCaption()
        {
            throw new NotImplementedException();
        }

        public override bool IsActive()
        {
            throw new NotImplementedException();
        }

    }
}
