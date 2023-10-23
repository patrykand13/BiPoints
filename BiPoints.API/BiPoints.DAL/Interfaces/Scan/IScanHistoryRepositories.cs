using BiPoints.DAL.DAO;

namespace BiPoints.DAL.Interfaces.Scan
{
    public interface IScanHistoryRepositories
    {
        List<ScanHistoryDao> GetScanHistory(Guid userId, int skipRecords);
    }
}
