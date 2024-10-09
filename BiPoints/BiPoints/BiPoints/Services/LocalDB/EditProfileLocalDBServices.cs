using BiPoints.Helpers.LocalDB;
using BiPoints.Interfaces.LocalDB;
using System;
using System.Threading.Tasks;

namespace BiPoints.Services.LocalDB
{
    class EditProfileLocalDBServices : SearchLocalDBServices, IEditProfileLocalDBServices
    {
        public async Task EditProfileAsync(Guid userId, string token)
        {
            var mainLocalProfile = SearchLocalProfile();
            if (mainLocalProfile == null)
            {
                var profile = new LocalProfileClass
                {
                    UserId = userId,
                    Token = token
                };
                await DatabaseLocator.LocalDb.MainLocalProfile.AddAsync(profile);
            }
            else
            {
                mainLocalProfile.UserId = userId;
                mainLocalProfile.Token = token;
            }

            await DatabaseLocator.LocalDb.SaveChangesAsync();
        }
    }
}
