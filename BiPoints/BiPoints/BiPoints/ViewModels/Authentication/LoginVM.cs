using BiPoints.Helpers;
using BiPoints.Interfaces.Authenticate;
using BiPoints.Interfaces.Base;
using BiPoints.Interfaces.LocalDB;
using BiPoints.Interfaces.User;
using BiPoints.Models.Responses.User;
using BiPoints.Views.Authentication;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace BiPoints.ViewModels.Authentication
{
    class LoginVM : AuthenticationVM
    {
        private readonly IAlertNotificationServices _notificationServices;
        private readonly ILoginServices _loginServices;
        private readonly ISearchLocalDBServices _searchLocalDBServices;
        private readonly IUserServices _userServices;
        public string Username { get; set; } = "";
        public string Password { get; set; } = "";
        public ICommand SignInCommand => new Command(async () => await SignIn());
        public ICommand GoSignUpCommand => new Command(async () => await GoSignUp());
        internal LoginVM()
        {
            _notificationServices = DependencyService.Get<IAlertNotificationServices>();
            _loginServices = DependencyService.Get<ILoginServices>();
            _searchLocalDBServices = DependencyService.Get<ISearchLocalDBServices>();
            _userServices = DependencyService.Get<IUserServices>();
            if (!BaseInfoHelper.LogOut) LoginSession();
        }
        async Task SignIn()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            // Check if the user has provided a username and password.
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
            {
                await _notificationServices.AlertNotificationPopup("ErrorWrongUsernameOrPassword");
                IsBusy = false;
                return;
            }

            var response = await _loginServices.Login(Username, Password);

            // Check if the response is not an error.
            if (response.Equals(Error))
            {
                IsBusy = false;
                return;
            }

            var userModel = JsonConvert.DeserializeObject<UserResponse>(response);

            // Update user token in the local database.
            await _editProfileLocalDBServices.EditProfile(userModel.Id, userModel.Token);

            // Update user information in ProfileHelper.
            ProfileHelper.UserId = userModel.Id;
            ProfileHelper.Token = userModel.Token;
            ProfileHelper.Name = userModel.Name;
            ProfileHelper.Lastname = userModel.Lastname;
            ProfileHelper.City = userModel.City;
            ProfileHelper.Address = userModel.Address;
            ProfileHelper.PhoneNumber = userModel.PhoneNumber;
            BaseInfoHelper.Language = userModel.Language;

            // Start the main user interface.
            Start();
            IsBusy = false;
        }
        async Task LoginSession()
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

            // Set the user's token based on the local profile.
            ProfileHelper.Token = localProfile.Token;

            // Retrieve user information.
            var userData = await _userServices.GetInformations(localProfile.UserId);

            // Check if user data is not an error.
            if (userData.Equals(Error))
            {
                IsBusy = false;
                return;
            }

            var userModel = JsonConvert.DeserializeObject<PersonalUserResponse>(userData);

            // Update user information in ProfileHelper.
            ProfileHelper.UserId = userModel.Id;
            ProfileHelper.Name = userModel.Name;
            ProfileHelper.Lastname = userModel.Lastname;
            ProfileHelper.City = userModel.City;
            ProfileHelper.Address = userModel.Address;
            ProfileHelper.PhoneNumber = userModel.PhoneNumber;
            BaseInfoHelper.Language = userModel.Language;

            // Start the main user interface.
            Start();
            IsBusy = false;
        }

        async Task GoSignUp()
        {
            if (IsBusy)
                return;
            IsBusy = true;
            await PushAsync(new RegisterPage());
            IsBusy = false;
        }
    }
}
