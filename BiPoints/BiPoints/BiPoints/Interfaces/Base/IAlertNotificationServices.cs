using System.Threading.Tasks;

namespace BiPoints.Interfaces.Base
{
    interface IAlertNotificationServices
    {
        Task AlertNotificationPopup(string message);
    }
}
