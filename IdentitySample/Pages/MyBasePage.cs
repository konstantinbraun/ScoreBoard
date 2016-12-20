using IdentitySample.Controllers;
using IdentitySample.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IdentitySample.Pages
{
    public class MyBasePage : WebViewPage<PageHeader>
    {
        public INavigationService NavigationService { get; set; }

        public override void Execute()
        {
            throw new NotImplementedException();
        }
    }
}