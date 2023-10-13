using BiPoints.Helpers.LocalDB;
using BiPoints.Interfaces.LocalDB;
using System.Linq;

namespace BiPoints.Services.LocalDB
{
    class SearchLocalDBServices : ISearchLocalDBServices
    {
        public LocalProfileClass SearchLocalProfile()
        {
            var localProfile = DatabaseLocator.LocalDb.MainLocalProfile.FirstOrDefault();

            return localProfile;
        }
    }
}
