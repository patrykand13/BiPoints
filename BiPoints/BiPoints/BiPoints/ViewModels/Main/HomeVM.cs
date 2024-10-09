using BiPoints.Helpers;
using BiPoints.Interfaces.Scan;
using BiPoints.Models.Responses.Scan;
using BiPoints.Views.Authentication;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace BiPoints.ViewModels.Main
{
    internal class HomeVM : BaseVM
    {
        private readonly IScanPointsServices _scanPointsServices;
        private string _name { get; set; } = ProfileHelper.Name + " " + ProfileHelper.Lastname;
        public string Name { get { return _name; } set { _name = value; OnPropertyChanged(); } }
        private string _dayAchievementColor { get; set; }
        public string DayAchievementColor { get { return _dayAchievementColor; } set { _dayAchievementColor = value; OnPropertyChanged(); } }
        private string _weekAchievementColor { get; set; }
        public string WeekAchievementColor { get { return _weekAchievementColor; } set { _weekAchievementColor = value; OnPropertyChanged(); } }
        private int _biPoints { get; set; }
        public int BiPoints { get { return _biPoints; } set { _biPoints = value; OnPropertyChanged(); } }
        private int _biPointsForUse { get; set; }
        public int BiPointsForUse { get { return _biPointsForUse; } set { _biPointsForUse = value; OnPropertyChanged(); } }
        private int _biPointsUsed { get; set; }
        public int BiPointsUsed { get { return _biPointsUsed; } set { _biPointsUsed = value; OnPropertyChanged(); } }
        private int _dayAchievementValue { get; set; }
        public int DayAchievementValue { get { return _dayAchievementValue; } set { _dayAchievementValue = value; OnPropertyChanged(); } }
        private int _weekAchievementValue { get; set; }
        public int WeekAchievementValue { get { return _weekAchievementValue; } set { _weekAchievementValue = value; OnPropertyChanged(); } }
        private int _dayAchievementToGet { get; set; }
        public int DayAchievementToGet { get { return _dayAchievementToGet; } set { _dayAchievementToGet = value; OnPropertyChanged(); } }
        private int _weekAchievementToGet { get; set; }
        public int WeekAchievementToGet { get { return _weekAchievementToGet; } set { _weekAchievementToGet = value; OnPropertyChanged(); } }
        private double _dayAchievementProgress { get; set; }
        public double DayAchievementProgress { get { return _dayAchievementProgress; } set { _dayAchievementProgress = value; OnPropertyChanged(); } }
        private double _weekAchievementProgress { get; set; }
        public double WeekAchievementProgress { get { return _weekAchievementProgress; } set { _weekAchievementProgress = value; OnPropertyChanged(); } }
        public ICommand SignOutCommand => new Command(SignOut);
        public ICommand GoScanCommand => new Command(async () => await GoScanAsync());
        internal HomeVM()
        {
            _scanPointsServices = DependencyService.Get<IScanPointsServices>();
        }
        internal async Task InitAsync()
        {
            IsBusy = true;
            var informationData = await _scanPointsServices.GetPointsInformationsAsync(ProfileHelper.UserId);
            if (CheckIfTheDataIsIncorrect(informationData))
            {
                IsBusy = false;
                return;
            }
            var informationModel = JsonConvert.DeserializeObject<PointsInformationResponse>(informationData);
            DayAchievementColor = informationModel.DayAchievementColor;
            WeekAchievementColor = informationModel.WeekAchievementColor;
            DayAchievementProgress = informationModel.DayAchievementProgress;
            WeekAchievementProgress = informationModel.WeekAchievementProgress;
            DayAchievementToGet = informationModel.DayAchievementToGet;
            WeekAchievementToGet = informationModel.WeekAchievementToGet;
            DayAchievementValue = informationModel.DayAchievementValue;
            WeekAchievementValue = informationModel.WeekAchievementValue;
            BiPoints = informationModel.BiPoints;
            BiPointsForUse = informationModel.BiPointsForUse;
            BiPointsUsed = informationModel.BiPointsUsed;

            IsBusy = false;
        }
        private void SignOut()
        {
            if (IsBusy)
                return;
            IsBusy = true;
            ProfileHelper.UserId = Guid.Empty;
            ProfileHelper.Token = null;
            ProfileHelper.Name = null;
            ProfileHelper.Lastname = null;
            ProfileHelper.City = null;
            ProfileHelper.Address = null;
            ProfileHelper.PhoneNumber = 0;
            BaseInfoHelper.LogOut = true;

            Application.Current.MainPage = new NavigationPage(new LoginPage());
            IsBusy = false;
        }
        private async Task GoScanAsync()
        {
            if (IsBusy)
                return;
            await GoToAsync($"//ScanPage");
        }
    }
}
