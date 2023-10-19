namespace BiPoints.DAL.Interfaces.Point
{
    public interface ICreatePointRepositories
    {
        Task<bool> CreatePointByUserId(Guid userId);
    }
}
