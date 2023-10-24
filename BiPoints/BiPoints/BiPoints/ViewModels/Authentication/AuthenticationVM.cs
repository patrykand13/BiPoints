using BiPoints.Helpers;
using BiPoints.Interfaces.LocalDB;
using BiPoints.Resx;
using System.Globalization;
using Xamarin.Forms;

namespace BiPoints.ViewModels.Authentication
{
    internal class AuthenticationVM : BaseVM
    {
        internal IEditProfileLocalDBServices _editProfileLocalDBServices;
        internal AuthenticationVM()
        {
            _editProfileLocalDBServices = DependencyService.Get<IEditProfileLocalDBServices>();
        }
        protected void Start()
        {
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

    }
}
