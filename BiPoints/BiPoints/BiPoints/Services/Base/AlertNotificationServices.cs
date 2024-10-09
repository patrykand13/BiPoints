using BiPoints.Interfaces.Base;
using BiPoints.Popups.Base;
using Rg.Plugins.Popup.Services;
using System.Threading.Tasks;

namespace BiPoints.Services.Base
{
    class AlertNotificationServices : IAlertNotificationServices
    {
        public async Task AlertNotificationPopup(string message)
        {
            var dialog = new NotificationAlertPopup(message);
            await PopupNavigation.Instance.PushAsync(dialog);
        }
    }
}
