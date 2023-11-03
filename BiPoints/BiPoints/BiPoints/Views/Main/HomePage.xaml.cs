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
            Init();
            InitializeComponent();
        }
        async void Init()
        {
            _viewModel = new HomeVM();
            await _viewModel.Init();
            BindingContext = _viewModel;
        }
        protected override async void OnAppearing()
        {
            if (_viewModel != null) await _viewModel.Init();
            base.OnAppearing();
        }
    }
}