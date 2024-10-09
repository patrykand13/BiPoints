using BiPoints.Helpers.LocalDB;
using BiPoints.Interfaces.LocalDB;
using System.Threading.Tasks;

namespace BiPoints.Services.LocalDB
{
    class DeleteProfileLocalDBServices : SearchLocalDBServices, IDeleteProfileLocalDBServices
    {
        public async Task DeleteProfile()
        {
            var mainLocalProfile = SearchLocalProfile();
            if (mainLocalProfile != null) DatabaseLocator.LocalDb.MainLocalProfile.Remove(mainLocalProfile);

            await DatabaseLocator.LocalDb.SaveChangesAsync();
        }
    }
}
