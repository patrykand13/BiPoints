using System;
using System.Threading.Tasks;

namespace BiPoints.Interfaces.LocalDB
{
    interface IEditProfileLocalDBServices
    {
        Task EditProfileAsync(Guid userId, string token);
    }
}
