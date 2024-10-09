using BiPoints.DAL.Entities;

namespace BiPoints.DAL.Interfaces.Point
{
    public interface IGetPointRepositories
    {
        Task<PointEntity> GetPointsByUserIdAsync(Guid userId);
    }
}
