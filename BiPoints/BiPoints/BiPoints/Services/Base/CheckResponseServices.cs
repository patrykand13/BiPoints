using BiPoints.Interfaces.Base;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BiPoints.Services.Base
{
    class CheckResponseServices : ICheckResponseServices
    {
        private string Error = "ERROR";
        private readonly IAlertNotificationServices _notificationServices;
        public CheckResponseServices()
        {
            _notificationServices = DependencyService.Get<IAlertNotificationServices>();
        }
        public async Task<string> CheckApiResponse(HttpResponseMessage response, bool sendNotification)
        {
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                if (sendNotification) await _notificationServices.AlertNotificationPopup(await response.Content.ReadAsStringAsync());
                return Error;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                if (sendNotification) await _notificationServices.AlertNotificationPopup("ErrorUnauthorizedAccess");
                return Error;
            }
            else
            {
                if (sendNotification) await _notificationServices.AlertNotificationPopup("ErrorNoConnectionToTheServer");
                return Error;
            }
        }
    }
}
