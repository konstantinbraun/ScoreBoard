using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentitySample.ViewModels.TabItems
{
    public class TeamTabItem : TabItemBase
    {
        public override string ActionName
        {
            get
            {
                return "_Teams2";
            }
        }

        public override string GetCaption()
        {
            return "Caption";
        }

        public override bool IsActive()
        {
            return true;
        }

    }
}
