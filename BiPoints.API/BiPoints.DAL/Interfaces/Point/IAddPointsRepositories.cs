namespace BiPoints.DAL.Interfaces.Point
{
    public interface IAddPointsRepositories
    {
        Task<bool> AddPointsAsync(Guid userId, int points);
    }
}
