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
            CultureInfo ci = new CultureInfo("pl");
            if (!string.IsNullOrWhiteSpace(BaseInfoHelper.Language) && BaseInfoHelper.Language.Equals("StringPolish"))
            {
                ci = new CultureInfo("pl");
            }
            else if (!string.IsNullOrWhiteSpace(BaseInfoHelper.Language) && BaseInfoHelper.Language.Equals("StringEnglish"))
            {
                ci = new CultureInfo("en-US");
            }
            AppResources.Culture = ci;
            CultureInfo.DefaultThreadCurrentCulture = ci;

            Application.Current.MainPage = new AppShell();
        }
    }
}
