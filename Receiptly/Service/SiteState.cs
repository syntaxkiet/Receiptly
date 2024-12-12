using Microsoft.AspNetCore.Components;
using Shared.Enum.Components.Home;

namespace Receiptly.Service
{
    public class SiteState
    {
        public EventCallback<NavigationAction> NavEntryChanged { get; set; }
        public NavigationAction NavEntry { get; set; }        
    }
}
