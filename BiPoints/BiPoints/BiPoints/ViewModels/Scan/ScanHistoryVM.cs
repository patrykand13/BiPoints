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
        private readonly IGetScanHistoryService _scanServices;
        private ObservableRangeCollection<ScanHistoryItemResponse> _scanHistoryList { get; set; } = new ObservableRangeCollection<ScanHistoryItemResponse>();
        public ObservableRangeCollection<ScanHistoryItemResponse> ScanHistoryList { get { return _scanHistoryList; } set { _scanHistoryList = value; OnPropertyChanged(); } }
        private bool _isRefreshing { get; set; }
        public bool IsRefreshing { get { return _isRefreshing; } set { _isRefreshing = value; OnPropertyChanged(); } }
        private bool _emptyListVisible { get; set; }
        public bool EmptyListVisible { get { return _emptyListVisible; } set { _emptyListVisible = value; OnPropertyChanged(); } }
        private int SkipRecords { get; set; }
        public ICommand LoadListCommand => new Command(async () => await LoadListAsync());
        public ICommand RefreshCommand => new Command(async () => await RefreshAsync());
        internal ScanHistoryVM()
        {
            _scanServices = DependencyService.Get<IGetScanHistoryService>();
        }
        private async Task RefreshAsync()
        {
            if (IsBusy)
                return;

            await InitAsync();
            IsRefreshing = false;
        }

        private async Task LoadListAsync()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            ScanHistoryList.AddRange(await GetScanHistoryListAsync());

            IsBusy = false;
        }

        internal async Task InitAsync()
        {
            IsBusy = true;

            SkipRecords = 0;
            ScanHistoryList.ReplaceRange(await GetScanHistoryListAsync());
            EmptyListVisible = ScanHistoryList.IsEmptyOrNull();

            IsBusy = false;
        }

        private async Task<ObservableRangeCollection<ScanHistoryItemResponse>> GetScanHistoryListAsync()
        {
            var helpList = new ObservableRangeCollection<ScanHistoryItemResponse>();
            var scanStatusData = await _scanServices.GetScanHistoryListAsync(ProfileHelper.UserId, SkipRecords);

            // Check if the response is not an error.
            if (CheckIfTheDataIsIncorrect(scanStatusData))
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
