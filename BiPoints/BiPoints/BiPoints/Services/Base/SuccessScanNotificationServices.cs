using BiPoints.Interfaces.Base;
using BiPoints.Popups.Base;
using Rg.Plugins.Popup.Services;
using System.Threading.Tasks;

namespace BiPoints.Services.Base
{
    class SuccessScanNotificationServices : ISuccessScanServices
    {
        public async Task SuccessScanPopup(string name, string image)
        {
            var dialog = new SuccessScanPopup(name, image);
            await PopupNavigation.Instance.PushAsync(dialog);
        }
    }
}
