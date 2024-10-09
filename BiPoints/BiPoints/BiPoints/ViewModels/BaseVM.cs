using MvvmHelpers;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BiPoints.ViewModels
{
    internal abstract class BaseVM : BaseViewModel
    {
        private protected async Task PopPageAsync()
        {
            IsBusy = true;
            await Application.Current.MainPage.Navigation.PopAsync();
            IsBusy = false;
        }
        private protected async Task PushPageAsync(Page page)
        {
            IsBusy = true;
            await Application.Current.MainPage.Navigation.PushAsync(page);
            IsBusy = false;
        }
        private protected async Task GoToAsync(string page)
        {
            IsBusy = true;
            await Shell.Current.GoToAsync(page);
            IsBusy = false;
        }
        private protected bool CheckIfTheDataIsIncorrect(string data)
        {
            if (string.IsNullOrWhiteSpace(data))
                return true;
            return false;
        }
    }
}
