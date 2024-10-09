using BiPoints.Helpers;
using BiPoints.Interfaces.LocalDB;
using BiPoints.Interfaces.User;
using BiPoints.Models.Responses.User;
using BiPoints.Resx;
using Newtonsoft.Json;
using System;
using System.Globalization;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BiPoints.ViewModels.Authentication
{
    internal abstract class AuthenticationVM : BaseVM
    {
        private protected readonly IEditProfileLocalDBServices _editProfileLocalDBServices;
        private readonly IUserServices _userServices;
        private protected AuthenticationVM()
        {
            _editProfileLocalDBServices = DependencyService.Get<IEditProfileLocalDBServices>();
            _userServices = DependencyService.Get<IUserServices>();
        }
        private protected async Task StartAsync(Guid userId, string token)
        {
            // Set the user's token based on the local profile.
            ProfileHelper.Token = token;
            ProfileHelper.UserId = userId;

            // Retrieve user information.
            if (!await GetUserDataAsync())
                return;
            // Set the default language to Polish (pl).
            CultureInfo ci = new CultureInfo("pl");

            if (!string.IsNullOrWhiteSpace(BaseInfoHelper.Language) && BaseInfoHelper.Language.Equals("StringPolish"))
            {
                ci = new CultureInfo("pl");
            }
            else if (!string.IsNullOrWhiteSpace(BaseInfoHelper.Language) && BaseInfoHelper.Language.Equals("StringEnglish"))
            {
                ci = new CultureInfo("en-US");
            }

            // Set the application's culture to the selected language.
            AppResources.Culture = ci;
            // Set the default culture for the current thread.
            CultureInfo.DefaultThreadCurrentCulture = ci;

            // Set the main page of the application to AppShell.
            Application.Current.MainPage = new AppShell();
        }
        private protected async Task<bool> GetUserDataAsync()
        {
            // Retrieve user information.
            var response = await _userServices.GetUserDataAsync(ProfileHelper.UserId);

            // Check if user data is not an error.
            if (CheckIfTheDataIsIncorrect(response))
            {
                IsBusy = false;
                return false;
            }

            var userModel = JsonConvert.DeserializeObject<PersonalUserResponse>(response);

            // Update user information in ProfileHelper.
            ProfileHelper.Name = userModel.Name;
            ProfileHelper.Lastname = userModel.Lastname;
            ProfileHelper.City = userModel.City;
            ProfileHelper.Address = userModel.Address;
            ProfileHelper.PhoneNumber = userModel.PhoneNumber;
            BaseInfoHelper.Language = userModel.Language;
            return true;
        }
    }
}
