using System.Threading.Tasks;

namespace BiPoints.Interfaces.Base
{
    interface ISuccessScanServices
    {
        Task SuccessScanPopup(string name, string image);
    }
}
