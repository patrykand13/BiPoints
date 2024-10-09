using BiPoints.ViewModels.Authentication;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BiPoints.Views.Authentication
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        LoginVM _viewModel;
        public LoginPage()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (_viewModel == null)
            {
                _viewModel = new LoginVM();
                await _viewModel.InitAsync();
                BindingContext = _viewModel;
                await Task.Delay(500);
                if (loader != null) loader.IsVisible = false;
            }
        }
    }
}