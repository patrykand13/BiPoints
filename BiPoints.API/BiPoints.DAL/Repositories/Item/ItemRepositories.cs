using BiPoints.API;
using BiPoints.DAL.Entities;
using BiPoints.DAL.Interfaces.Item;

namespace BiPoints.DAL.Repositories.Item
{
    public class ItemRepositories : IItemRepositories
    {
        private readonly AppDbContext _context;
        public ItemRepositories(AppDbContext context)
        {
            _context = context;
        }

        public ItemEntity GetItemByCodeQr(string codeQr)
        {
            return _context.Items.Where(x => x.CodeQr == codeQr).FirstOrDefault();
        }
    }
}
