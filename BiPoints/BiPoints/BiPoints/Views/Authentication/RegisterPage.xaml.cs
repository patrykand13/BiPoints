using BiPoints.ViewModels.Authentication;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BiPoints.Views.Authentication
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        private RegisterVM _viewModel;
        public RegisterPage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (_viewModel == null)
            {
                _viewModel = new RegisterVM();
                BindingContext = _viewModel;
            }
        }
    }
}