using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Classes
{
    public class Utility
    {
        [Inject]
        public NavigationManager _navigationManager { get; set; }

        public Utility(NavigationManager nav)
        {
            _navigationManager = nav;
        }

        public string getCurrentPage()
        {
            int baseUriLength = _navigationManager.BaseUri.Length - 1;
            return _navigationManager.Uri.Remove(0, baseUriLength);
        }
    }
}
