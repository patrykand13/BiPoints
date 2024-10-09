using BiPoints.ViewModels.Main;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BiPoints.Views.Main
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        HomeVM _viewModel;
        public HomePage()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (_viewModel != null)
            {
                if (loader != null) loader.IsVisible = true;
                await _viewModel.InitAsync();
                if (loader != null) loader.IsVisible = false;
            }
            else
            {
                _viewModel = new HomeVM();
                await _viewModel.InitAsync();
                BindingContext = _viewModel;
                if (loader != null) loader.IsVisible = false;
            }
        }
    }
}