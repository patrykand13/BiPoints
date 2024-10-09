using BiPoints.ViewModels.Scan;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BiPoints.Views.Scan
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScanPage : ContentPage
    {
        ScanVM _viewModel;
        public ScanPage()
        {
            InitializeComponent();
        }
        private void ZXingScannerView_OnScanResult(ZXing.Result result)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                if (_viewModel != null && _viewModel.StartScan)
                    await _viewModel.ScanResultAsync(result.Text);
            });
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (_viewModel == null)
            {
                _viewModel = new ScanVM();
                BindingContext = _viewModel;
                await Task.Delay(500);
                if (loader != null) loader.IsVisible = false;
            }
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            if (_viewModel != null)
            {
                _viewModel.StartScan = false;
                _viewModel.ScanButtonColor = "#131C74";
            }
        }
    }
}