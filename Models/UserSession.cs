using LocalFantasyLeague.Models.DbSets;

namespace LocalFantasyLeague.Models
{
    public class UserSession
    {
        public event Action OnChange = null!;

        private User? _currentUser;

        public User? CurrentUser
        {
            get => _currentUser;
            set
            {
                _currentUser = value;
                NotifyStateChanged();
            }
        }

        private void NotifyStateChanged()
        {
            if (OnChange != null)
            {
                Task.Run(() => Microsoft.AspNetCore.Components.Dispatcher.CreateDefault().InvokeAsync(OnChange));
            }
        }
    }
}
