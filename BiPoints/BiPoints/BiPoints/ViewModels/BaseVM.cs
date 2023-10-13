using MvvmHelpers;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BiPoints.ViewModels
{
    public class BaseVM : BaseViewModel
    {
        public string Error = "ERROR";
        public async Task PopAsync()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }
        public async Task PushAsync(Page page)
        {
            await Application.Current.MainPage.Navigation.PushAsync(page);
        }
    }
}
