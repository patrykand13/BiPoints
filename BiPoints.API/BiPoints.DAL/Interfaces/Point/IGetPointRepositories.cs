using BiPoints.DAL.Entities;

namespace BiPoints.DAL.Interfaces.Point
{
    public interface IGetPointRepositories
    {
        PointEntity GetPointsByUserId(Guid userId);
    }
}
