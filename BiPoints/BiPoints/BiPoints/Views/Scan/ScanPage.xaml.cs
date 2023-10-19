using BiPoints.ViewModels.Scan;

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
            _viewModel = new ScanVM();
            BindingContext = _viewModel;
            InitializeComponent();
        }

        private void ZXingScannerView_OnScanResult(ZXing.Result result)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                if (_viewModel != null && _viewModel.StartScan)
                {
                    _viewModel.ScanResult(result.Text);
                }
            });
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