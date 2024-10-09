using BiPoints.Common.DTO.Model.Scan;

namespace BiPoints.DAL.Interfaces.Scan
{
    public interface IScanHistoryRepositories
    {
        Task<List<ScanHistoryDTO>> GetScanHistoryAsync(Guid userId, int skipRecords);
        Task<int> GetTodaysPointsAsync(Guid userId);
        Task<int> GetWeekPointsAsync(Guid userId);
    }
}
