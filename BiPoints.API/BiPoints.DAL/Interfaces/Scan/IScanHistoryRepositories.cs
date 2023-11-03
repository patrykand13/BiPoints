using BiPoints.DAL.DAO.Scan;

namespace BiPoints.DAL.Interfaces.Scan
{
    public interface IScanHistoryRepositories
    {
        List<ScanHistoryDao> GetScanHistory(Guid userId, int skipRecords);
        int GetTodaysPoints(Guid userId);
        int GetWeekPoints(Guid userId);
    }
}
