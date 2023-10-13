using BiPoints.Interfaces.Base;
using BiPoints.Popups.Base;
using Rg.Plugins.Popup.Services;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BiPoints.Services.Base
{
    class AlertNotificationServices : IAlertNotificationServices
    {
        private readonly ITextServices _textServices;
        public AlertNotificationServices()
        {
            _textServices = DependencyService.Get<ITextServices>();
        }
        public async Task AlertNotificationPopup(string message)
        {
            var dialog = new NotificationAlertPopup(_textServices.GetString(message));
            await PopupNavigation.Instance.PushAsync(dialog);
        }
    }
}
