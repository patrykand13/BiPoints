using BiPoints.ViewModels.Scan;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BiPoints.Views.Scan
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScanHistoryPage : ContentPage
    {
        public ScanHistoryPage()
        {
            Init();
            InitializeComponent();
        }
        async void Init()
        {
            var _viewModel = new ScanHistoryVM();
            await _viewModel.Init();
            BindingContext = _viewModel;
        }
    }
}