using BiPoints.API;
using BiPoints.DAL.Interfaces;

namespace BiPoints.DAL.Repositories
{
    public class SaveRepositories : ISaveRepositories
    {
        private readonly AppDbContext _context;
        public SaveRepositories(AppDbContext context)
        {
            _context = context;
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
