using System;
using System.Threading.Tasks;

namespace BiPoints.Interfaces.User
{
    interface IUserServices
    {
        Task<string> GetUserDataAsync(Guid userId);
    }
}
