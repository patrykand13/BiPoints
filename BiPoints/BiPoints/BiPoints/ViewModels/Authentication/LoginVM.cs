using BiPoints.Helpers;
using BiPoints.Interfaces.Authenticate;
using BiPoints.Interfaces.Base;
using BiPoints.Interfaces.LocalDB;
using BiPoints.Models.Responses.Authentication;
using BiPoints.Views.Authentication;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace BiPoints.ViewModels.Authentication
{
    internal class LoginVM : AuthenticationVM
    {
        private readonly IAlertNotificationServices _notificationServices;
        private readonly ILoginServices _loginServices;
        private readonly ISearchLocalDBServices _searchLocalDBServices;
        public string Username { get; set; } = "";
        public string Password { get; set; } = "";
        public ICommand SignInCommand => new Command(async () => await SignInAsync());
        public ICommand GoSignUpCommand => new Command(async () => await GoSignUpAsync());
        internal LoginVM()
        {
            _notificationServices = DependencyService.Get<IAlertNotificationServices>();
            _loginServices = DependencyService.Get<ILoginServices>();
            _searchLocalDBServices = DependencyService.Get<ISearchLocalDBServices>();
        }
        internal async Task InitAsync()
        {
            if (!BaseInfoHelper.LogOut)
                await LoginSession();
        }
        private async Task SignInAsync()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            // Check if the user has provided a username and password.
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
            {
                await _notificationServices.AlertNotificationPopup("Wrong username or password");
                IsBusy = false;
                return;
            }

            var response = await _loginServices.LoginAsync(Username, Password);

            // Check if the response is not an error.
            if (CheckIfTheDataIsIncorrect(response))
            {
                IsBusy = false;
                return;
            }

            var authenticateModel = JsonConvert.DeserializeObject<AuthenticateResponse>(response);

            // Update user token in the local database.
            await _editProfileLocalDBServices.EditProfileAsync(authenticateModel.UserId, authenticateModel.Token);

            // Start the main user interface.
            await StartAsync(authenticateModel.UserId, authenticateModel.Token);
            IsBusy = false;
        }
        private async Task LoginSession()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            // Search for the local user profile.
            var localProfile = _searchLocalDBServices.SearchLocalProfile();

            // If the local profile doesn't exist, exit the session login process.
            if (localProfile == null)
            {
                IsBusy = false;
                return;
            }

            // Start the main user interface.
            await StartAsync(localProfile.UserId, localProfile.Token);
            IsBusy = false;
        }
        private async Task GoSignUpAsync()
        {
            if (IsBusy)
                return;
            await PushPageAsync(new RegisterPage());
        }
    }
}
