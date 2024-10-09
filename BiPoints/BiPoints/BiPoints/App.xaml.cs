using BiPoints.Helpers.LocalDB;
using BiPoints.Interfaces.Authenticate;
using BiPoints.Interfaces.Base;
using BiPoints.Interfaces.LocalDB;
using BiPoints.Interfaces.Scan;
using BiPoints.Interfaces.User;
using BiPoints.Services.Authenticate;
using BiPoints.Services.Base;
using BiPoints.Services.LocalDB;
using BiPoints.Services.Scan;
using BiPoints.Services.User;
using BiPoints.Views.Authentication;
using Xamarin.Forms;

namespace BiPoints
{
    public partial class App : Application
    {

        public App()
        {
            //Initialize AppResources
            Resources["DefaultStringResources"] = new Resx.AppResources();
            InitializeComponent();

            //Initialize main DI
            DependencyService.Register<ITextServices, TextServices>();
            DependencyService.Register<IAlertNotificationServices, AlertNotificationServices>();
            DependencyService.Register<ISuccessScanServices, SuccessScanNotificationServices>();

            //Initialize DI
            DependencyService.Register<ILoginServices, LoginServices>();
            DependencyService.Register<IRegisterServices, RegisterServices>();
            DependencyService.Register<IEditProfileLocalDBServices, EditProfileLocalDBServices>();
            DependencyService.Register<IDeleteProfileLocalDBServices, DeleteProfileLocalDBServices>();
            DependencyService.Register<ISearchLocalDBServices, SearchLocalDBServices>();
            DependencyService.Register<IUserServices, UserServices>();
            DependencyService.Register<IScanServices, ScanServices>();
            DependencyService.Register<IGetScanHistoryService, ScanHistoryService>();
            DependencyService.Register<IScanPointsServices, ScanPointsServices>();

            //Initialize Local Database
            var localDb = new LocalDb();
            localDb.Database.EnsureCreated();
            DatabaseLocator.LocalDb = localDb;

            MainPage = new NavigationPage(new LoginPage());
        }
    }
}
