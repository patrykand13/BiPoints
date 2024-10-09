using BiPoints.ViewModels.Scan;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BiPoints.Views.Scan
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScanHistoryPage : ContentPage
    {
        ScanHistoryVM _viewModel;
        public ScanHistoryPage()
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
                _viewModel = new ScanHistoryVM();
                await _viewModel.InitAsync();
                BindingContext = _viewModel;
                if (loader != null) loader.IsVisible = false;
            }
        }
    }
}