using BiPoints.DAL.Entities;

namespace BiPoints.DAL.Interfaces.Point
{
    public interface ICreatePointRepositories
    {
        Task<bool> CreatePointInformationAsync(PointEntity point);
    }
}
