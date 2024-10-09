using BiPoints.DAL.Entities;

namespace BiPoints.DAL.Interfaces.Scan
{
    public interface IScanRepositories
    {
        Task<bool> AddScanAsync(ScanHistoryEntity scanHistory);
        Task<bool> CheckIfTheCodeWasScannedAsync(string codeQr);
    }
}
