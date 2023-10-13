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
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
            {
                await _notificationServices.AlertNotificationPopup("ErrorWrongUsernameOrPassword");
                IsBusy = false;
                return;
            }
            var response = await _loginServices.Login(Username, Password);
            if (!response.Equals(Error))
            {
                var userModel = JsonConvert.DeserializeObject<UserResponse>(response);
                await _editProfileLocalDBServices.EditProfile(userModel.Id, userModel.Token);
                ProfileHelper.UserId = userModel.Id;
                ProfileHelper.Token = userModel.Token;
                ProfileHelper.Name = userModel.Name;
                ProfileHelper.Lastname = userModel.Lastname;
                ProfileHelper.City = userModel.City;
                ProfileHelper.Address = userModel.Address;
                ProfileHelper.PhoneNumber = userModel.PhoneNumber;
                BaseInfoHelper.Language = userModel.Language;
                Start();
            }
            else IsBusy = false;
        }
        async Task LoginSession()
        {
            if (IsBusy)
                return;
            IsBusy = true;
            var localProfile = _searchLocalDBServices.SearchLocalProfile();
            if (localProfile == null)
            {
                IsBusy = false;
                return;
            }
            ProfileHelper.Token = localProfile.Token;
            var userData = await _userServices.GetInformations(localProfile.UserId);
            if (userData != "ERROR")
            {
                var userModel = JsonConvert.DeserializeObject<PersonalUserResponse>(userData);
                ProfileHelper.UserId = userModel.Id;
                ProfileHelper.Name = userModel.Name;
                ProfileHelper.Lastname = userModel.Lastname;
                ProfileHelper.City = userModel.City;
                ProfileHelper.Address = userModel.Address;
                ProfileHelper.PhoneNumber = userModel.PhoneNumber;
                BaseInfoHelper.Language = userModel.Language;
                Start();
            }
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
