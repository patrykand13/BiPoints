using BiPoints.API;
using BiPoints.Common.Exceptions;
using BiPoints.DAL.Entities;
using BiPoints.DAL.Interfaces.Item;
using Microsoft.EntityFrameworkCore;

namespace BiPoints.DAL.Repositories.Item
{
    public class ItemRepositories : IItemRepositories
    {
        private readonly AppDbContext _context;
        public ItemRepositories(AppDbContext context)
        {
            _context = context;
        }
        public async Task<ItemEntity> GetItemByCodeQrAsync(string codeQr)
        {
            try
            {
                return await _context.Items.FirstOrDefaultAsync(x => x.CodeQr == codeQr);
            }
            catch (DbUpdateException ex)
            {
                throw new DataAccessException("Error occurred while checking the database.", "ItemRepositories/GetItemByCodeQrAsync", ex);
            }
            catch (Exception ex)
            {
                throw new DataAccessException("An unexpected error occurred while getting item.", "ItemRepositories/GetItemByCodeQrAsync", ex);
            }
        }
    }
}
