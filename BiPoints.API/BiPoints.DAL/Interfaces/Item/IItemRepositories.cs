using BiPoints.DAL.Entities;

namespace BiPoints.DAL.Interfaces.Item
{
    public interface IItemRepositories
    {
        Task<ItemEntity> GetItemByCodeQrAsync(string codeQr);
    }
}
