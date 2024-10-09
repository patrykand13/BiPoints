using BiPoints.Interfaces.Base;
using BiPoints.Interfaces.Scan;
using BiPoints.Models.Responses.Scan;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace BiPoints.ViewModels.Scan
{
    internal class ScanVM : BaseVM
    {
        private readonly IAlertNotificationServices _alertNotificationServices;
        private readonly ISuccessScanServices _successScanServices;
        private readonly IScanServices _scanServices;
        private string _scanButtonColor { get; set; } = "#131C74";
        public string ScanButtonColor { get { return _scanButtonColor; } set { _scanButtonColor = value; OnPropertyChanged(); } }
        private bool _startScan { get; set; }
        public bool StartScan { get { return _startScan; } set { _startScan = value; OnPropertyChanged(); } }
        public ICommand ScanCommand => new Command(Scan);
        internal ScanVM()
        {
            _alertNotificationServices = DependencyService.Get<IAlertNotificationServices>();
            _successScanServices = DependencyService.Get<ISuccessScanServices>();
            _scanServices = DependencyService.Get<IScanServices>();
        }
        // The Scan() method initializes the scanning process by changing the button color and setting the scan start flag.
        private void Scan()
        {
            ScanButtonColor = "#1A1A1A";
            StartScan = true;
        }
        internal async Task ScanResultAsync(string result)
        {
            if (IsBusy)
                return;

            IsBusy = true;
            StartScan = false;
            ScanButtonColor = "#131C74";

            // Check if the scan result is not empty and has sufficient length.
            if (result == null || result.Length <= 3)
            {
                await _alertNotificationServices.AlertNotificationPopup("Scan failed");
            }

            var scanData = await _scanServices.ScanAsync(result);
            if (CheckIfTheDataIsIncorrect(scanData))
            {
                IsBusy = false;
                return;
            }
            // Deserialize the result data and trigger a success scanning popup.
            var scanItemModel = JsonConvert.DeserializeObject<ItemResponse>(scanData);
            await _successScanServices.SuccessScanPopup(scanItemModel.Name, scanItemModel.Image);

            IsBusy = false;
        }

    }
}
