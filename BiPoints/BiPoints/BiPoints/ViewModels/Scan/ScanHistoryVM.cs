using BiPoints.Helpers;
using BiPoints.Interfaces.Scan;
using BiPoints.Models.Responses.ScanHistory;
using MvvmHelpers;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace BiPoints.ViewModels.Scan
{
    internal class ScanHistoryVM : BaseVM
    {
        IGetScanHistoryService _scanServices;
        private ObservableRangeCollection<ScanHistoryItemResponse> _scanHistoryList { get; set; } = new ObservableRangeCollection<ScanHistoryItemResponse>();
        public ObservableRangeCollection<ScanHistoryItemResponse> ScanHistoryList { get { return _scanHistoryList; } set { _scanHistoryList = value; OnPropertyChanged(); } }
        private bool _isRefreshing { get; set; } = false;
        public bool IsRefreshing { get { return _isRefreshing; } set { _isRefreshing = value; OnPropertyChanged(); } }
        public int SkipRecords { get; set; }
        public ICommand LoadListCommand => new Command(LoadList);
        public ICommand RefreshCommand => new Command(Refresh);
        internal ScanHistoryVM()
        {
            _scanServices = DependencyService.Get<IGetScanHistoryService>();
        }
        private async void Refresh()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            IsRefreshing = true;

            SkipRecords = 0;
            ScanHistoryList.ReplaceRange(await GetScanHistoryList());

            IsRefreshing = false;
            IsBusy = false;
        }

        private async void LoadList()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            ScanHistoryList.AddRange(await GetScanHistoryList());

            IsBusy = false;
        }

        public async Task Init()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            ScanHistoryList.ReplaceRange(await GetScanHistoryList());

            IsBusy = false;
        }

        private async Task<ObservableRangeCollection<ScanHistoryItemResponse>> GetScanHistoryList()
        {
            var helpList = new ObservableRangeCollection<ScanHistoryItemResponse>();
            var scanStatusData = await _scanServices.GetScanHistoryList(ProfileHelper.UserId, SkipRecords);

            // Check if the response is not an error.
            if (scanStatusData.Equals(Error))
            {
                return helpList;
            }

            // Deserialize the data and update the skipped records count.
            var scanStatusList = JsonConvert.DeserializeObject<ObservableRangeCollection<ScanHistoryItemResponse>>(scanStatusData);
            SkipRecords += scanStatusList.Count;
            helpList = scanStatusList;

            return helpList;
        }

    }
}
