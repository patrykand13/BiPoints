using BiPoints.DAL.Entities;

namespace BiPoints.DAL.Interfaces.Item
{
    public interface IItemRepositories
    {
        ItemEntity GetItemByCodeQr(string codeQr);
    }
}
