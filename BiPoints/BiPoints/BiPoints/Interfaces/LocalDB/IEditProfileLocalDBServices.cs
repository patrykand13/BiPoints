using System;
using System.Threading.Tasks;

namespace BiPoints.Interfaces.LocalDB
{
    interface IEditProfileLocalDBServices
    {
        Task EditProfile(Guid userId, string token);
    }
}
