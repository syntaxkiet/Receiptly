using Shared.Models;

namespace Shared.Cache
{
    public static class Local
    {
        public static List<string> NotificationHistory = new List<string>();
        
        public static List<Receipt> LocalRecipeList = new List<Receipt>();
        
    }
}