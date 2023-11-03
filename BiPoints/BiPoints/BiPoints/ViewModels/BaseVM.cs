using MvvmHelpers;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BiPoints.ViewModels
{
    public class BaseVM : BaseViewModel
    {
        string Error = "ERROR";
        public async Task PopAsync()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }
        public async Task PushAsync(Page page)
        {
            await Application.Current.MainPage.Navigation.PushAsync(page);
        }
        public bool CheckIfTheDataIsIncorrect(string data)
        {
            if (string.IsNullOrWhiteSpace(data) || data.Equals(Error))
                return true;
            return false;
        }
    }
}
