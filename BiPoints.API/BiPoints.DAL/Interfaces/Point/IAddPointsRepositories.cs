namespace BiPoints.DAL.Interfaces.Point
{
    public interface IAddPointsRepositories
    {
        bool AddPoints(Guid userId, int points);
    }
}
